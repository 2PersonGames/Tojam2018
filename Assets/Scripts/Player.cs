using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State
    {
        FullHappiness,
        FourFifths,
        Normal,
        TwoFifths,
        NoHappinessLeft
    };

    private static int playerCounter = 0;
    
    public AudioClip BlobCreatedAudioClip;
    public AudioClip BlobAbsorbedAudioClip;
    public AudioClip PlayerHitWall;

    private int _startingHappiness;
    private int _happiness;
    private AudioSource _audioSource;
    private int _playerNumber;
    private List<HappinessController> _happinessThrown;

    public void Init()
    {
        _happiness = 25;
        _startingHappiness = _happiness;
        _playerNumber = ++playerCounter;

        _happinessThrown = new List<HappinessController>();
        _audioSource = GetComponent<AudioSource>();
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
        _happiness -= happinessController.Happiness;
        _happinessThrown.Add(happinessController);
        if (false)
        {
            _audioSource.PlayOneShot(BlobCreatedAudioClip, 1.0f);
        }
        Debug.Log(string.Format("Player happiness decreased to {0}", _happiness));
    }

    public void ConsumeHappiness(Player player)
    {
        _happiness += player._happiness;

        Debug.Log(string.Format("Player happiness increased to {0}", _happiness));
    }

    public void ConsumeHappiness(HappinessController happinessController)
    {
        _happiness += happinessController.Happiness;
        if (false)
        {
            _audioSource.PlayOneShot(BlobAbsorbedAudioClip, 1.0f);
        }

        foreach (var player in Resources.FindObjectsOfTypeAll<Player>())
        {
            if (player._happinessThrown.Remove(happinessController) && player == this)
            {
                Debug.Log(string.Format("Player absorbed their own happiness!"));
            }
            break;
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
        var maxHappiness = _startingHappiness * 2;
        if (_happiness >= maxHappiness)
        {
            return State.FullHappiness;
        }
        else if (_happiness >= maxHappiness * 0.7f)
        {
            return State.FourFifths;
        }
        else if (_happiness >= maxHappiness * 0.3f)
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
}
