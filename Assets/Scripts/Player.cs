using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const int MAX_PLAYERS = 4;
    Animator anim;

    public enum State
    {
        FullHappiness = 4,
        FourFifths = 3,
        Normal = 2,
        TwoFifths = 1,
        NoHappinessLeft = 0
    };

    private static int playerCounter = 1;

    [Range(1, 100)]
    public int Happiness;
    public AudioClip BlobCreatedAudioClip;
    public AudioClip[] BlobAbsorbedAudioClip;
    public AudioClip[] PlayerHitWall;

    private int _startingHappiness;
    private int _happiness;
    private int _maxHappiness;
    private AudioSource _audioSource;
    private int _playerID;
    private List<HappinessController> _happinessThrown;


    public void Init()
    {
        _happiness = 25;
        _startingHappiness = _happiness;
        _playerID = playerCounter++;
        _maxHappiness = _startingHappiness * 2;

        _happinessThrown = new List<HappinessController>();
        _audioSource = GetComponent<AudioSource>();
        Happiness = 50;

        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        var otherPlayer = collision2D.gameObject.GetComponent<Player>();
        if (collision2D.gameObject.name == "RoomWalls")
        {
            Debug.Log(string.Format("Player collided with {0}!", gameObject.name));
            _audioSource.PlayOneShot(PlayerHitWall[Random.Range(0, PlayerHitWall.Length)], 0.25f);
        }
        else if (otherPlayer != null)
        {
            switch (GetState())
            {
                

                case State.NoHappinessLeft:

                    Debug.Log(string.Format("Player has sacrified themselves for another player!"));
                    otherPlayer.ConsumeHappiness(this);

                    Vector2 lastPosition = transform.position;
                    Destroy(gameObject);
                    //Debug.Log("TODO: Handle win state");


                    GameObject stormRainCloud = new GameObject();
                    stormRainCloud.transform.position = lastPosition;
                    SpriteRenderer spriteRenderer = stormRainCloud.AddComponent<SpriteRenderer>();
                    spriteRenderer.sprite = Resources.Load<GameObject>("storm1").GetComponent<SpriteRenderer>().sprite;

                    //replace cloud walls with storm
                    Transform roomClouds = GameObject.Find("RoomWalls").transform;
                    foreach (Transform childCloud in roomClouds)
                    {
                        childCloud.GetComponent<SpriteRenderer>().sprite = Resources.Load<GameObject>("storm2").GetComponent<SpriteRenderer>().sprite;
                    }
                    break;

                default:
                    _audioSource.PlayOneShot(PlayerHitWall[Random.Range(0, PlayerHitWall.Length)], 5.0f);
                    break;
            }
        }
    }

    public void HappinessCreated(HappinessController happinessController)
    {
        State stateBefore = GetState();
        _happiness -= happinessController.Happiness;
        UpdateAnimationState(stateBefore, GetState());

        _happinessThrown.Add(happinessController);
        if (false)
        {
            _audioSource.PlayOneShot(BlobCreatedAudioClip, 1.0f);
        }
        Debug.Log(string.Format("Player happiness decreased to {0}", _happiness));
    }

    public void ConsumeHappiness(Player player)
    {
        State stateBefore = GetState();
        _happiness += player._happiness;
        UpdateAnimationState(stateBefore, GetState());

        Debug.Log(string.Format("Player happiness increased to {0}", _happiness));
    }

    public void ConsumeHappiness(HappinessController happinessController)
    {
        State stateBefore = GetState();
        _happiness += happinessController.Happiness;
        UpdateAnimationState(stateBefore, GetState());

        _audioSource.PlayOneShot(BlobAbsorbedAudioClip[(int)GetState()], 1.0f);

        foreach (var player in Resources.FindObjectsOfTypeAll<Player>())
        {
            if (player._happinessThrown.Remove(happinessController))
            {
                if (player == this)
                {
                    Debug.Log(string.Format("Player absorbed their own happiness!"));
                }
                break;
            }
        }

        Debug.Log(string.Format("Player happiness increased to {0}", _happiness));
    }

    public bool IsHappinessOwnedByme(HappinessController happinessController)
    {
        return _happinessThrown.Contains(happinessController);
    }

    public int GetCountOfHappinessOwnedByMe()
    {
        return _happinessThrown.Count;
    }

    public State GetState()
    {
        if (_happiness >= _maxHappiness)
        {
            return State.FullHappiness;
        }
        else if (_happiness >= _maxHappiness * 0.7f)
        {
            return State.FourFifths;
        }
        else if (_happiness >= _maxHappiness * 0.3f)
        {
            return State.Normal;
        }
        else if (_happiness > ThrowController.MAX_HAPPINESS_THROW)
        {
            return State.TwoFifths;
        }
        else
        {
            return State.NoHappinessLeft;
        }
    }

    public void UpdateAnimationState(State stateBefore, State stateAfter)
    {
        if (stateBefore == stateAfter) return;

        Debug.Log("Animation State change :D ");
        anim.SetBool(stateBefore.ToString(), false);
        anim.SetBool(stateAfter.ToString(), true);
    }

    public string GetInputName(string name)
    {
        return string.Format("{0}{1}", _playerID, name);
    }
}
