using UnityEngine;

public class Player : MonoBehaviour
{
    private static int playerCounter = 0;
    
    [Range(1, 100)]
    public int Happiness;

    private int playerNumber_;

    public void Init()
    {
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

    public void BlobCreated(GameObject blog)
    {
        Happiness--;
        Debug.Log(string.Format("Player happiness decreased to {0}", Happiness));
    }

    public void ConsumeBlob(BlobController blobController)
    {
        Happiness++;
        Debug.Log(string.Format("Player happiness increased to {0}", Happiness));
    }
}
