using UnityEngine;

public class Player : MonoBehaviour
{
    private static int playerCounter = 0;
    
    [Range(1, 100)]
    public int Happiness;
    public AudioClip BlobCreatedAudioClip;
    public AudioClip BlobAbsorbedAudioClip;
    public AudioClip PlayerHitWall;

    private AudioSource _audioSource;
    private int playerNumber_;

    public void Init()
    {
        _audioSource = GetComponent<AudioSource>();
        Happiness = 50;
        playerNumber_ = ++playerCounter;
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
        if (collision2D.gameObject.name == "Blocks_1")
        {
            Debug.Log(string.Format("Player collided with {0}!", gameObject.name));
            _audioSource.PlayOneShot(PlayerHitWall, 1.0f);
        }
    }

    public void BlobCreated(GameObject blog)
    {
        Happiness--;
        if (false)
        {
            _audioSource.PlayOneShot(BlobCreatedAudioClip, 1.0f);
        }
        Debug.Log(string.Format("Player happiness decreased to {0}", Happiness));
    }

    public void ConsumeBlob(BlobController blobController)
    {
        Happiness++;
        if (false)
        {
            _audioSource.PlayOneShot(BlobAbsorbedAudioClip, 1.0f);
        }
        Debug.Log(string.Format("Player happiness increased to {0}", Happiness));
    }
}
