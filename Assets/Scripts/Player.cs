using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const int MAX_PLAYERS = 4;
    Animator anim;

    public enum State
    {
        FullHappiness,
        FourFifths,
        Normal,
        TwoFifths,
        NoHappinessLeft
    };

    private static int playerCounter = 1;

    [Range(1, 100)]
    public int Happiness;
    public AudioClip BlobCreatedAudioClip;
    public AudioClip BlobAbsorbedAudioClip;
    public AudioClip PlayerHitWall;

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
        if (false && collision2D.gameObject.name.StartsWith("Blocks_1"))
        {
            Debug.Log(string.Format("Player collided with {0}!", gameObject.name));
            _audioSource.PlayOneShot(PlayerHitWall, 1.0f);
        }
        else if (GetState() == State.NoHappinessLeft)
        {
            var otherPlayer = collision2D.gameObject.GetComponent<Player>();
            if (otherPlayer != null)
            {
                Debug.Log(string.Format("Player has sacrified themselves for another player!"));
                otherPlayer.ConsumeHappiness(this);
                Destroy(gameObject);               
                Debug.Log("TODO: Handle win state");
            }
        }
    }

    public void HappinessCreated(HappinessController happinessController)
    {
        //State stateBefore = GetState();
        _happiness -= happinessController.Happiness;
        //UpdateAnimationState(stateBefore, GetState());

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
        //State stateBefore = GetState();
        _happiness += happinessController.Happiness;
        //UpdateAnimationState(stateBefore, GetState());

        if (false)
        {
            _audioSource.PlayOneShot(BlobAbsorbedAudioClip, 1.0f);
        }

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
        Debug.Log("Animation  :D ");

        if (stateBefore == stateAfter) return;

        anim.SetBool(stateBefore.ToString(), false);
        anim.SetBool(stateAfter.ToString(), true);
    }

    public string GetInputName(string name)
    {
        return string.Format("{0}{1}", _playerID, name);
    }
}
