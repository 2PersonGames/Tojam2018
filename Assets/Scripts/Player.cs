using UnityEngine;

public class Player : MonoBehaviour
{
    private static int playerCounter = 0;

    private int playerNumber_;
    private int _happiness;

    public void Init()
    {
        _happiness = 50;
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

    public void BlobCreated(GameObject blog)
    {
        _happiness--;
        Debug.Log(string.Format("Player happiness decreased to {0}", _happiness));
    }

    public void ConsumeBlob(BlobController blobController)
    {
        _happiness++;
        Debug.Log(string.Format("Player happiness increased to {0}", _happiness));
    }
}
