using UnityEngine;

public class BlobController : MonoBehaviour
{
    public Player OriginPlayer { private get; set; }

    private float _originPlayerTimer;

    // Use this for initialization
    void Start()
    {
        _originPlayerTimer = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (OriginPlayer != null)
        {
            _originPlayerTimer -= Time.deltaTime;
            if (_originPlayerTimer < 0.0f)
            {
                OriginPlayer = null;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        var player = collision2D.gameObject.GetComponent<Player>();
        if (player != null && player != OriginPlayer)
        {
            Debug.Log(string.Format("Player collided with {0}!", gameObject.name));
            player.ConsumeBlob(this);
            Destroy(gameObject);
        }
    }
}
