using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClusteringSystem : MonoBehaviour
{
    public static int MAX_NUMBER_BLOB_PARTS = 10;

    private int numberBlobParts = MAX_NUMBER_BLOB_PARTS;
    private List<GameObject> blobs = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        //GameObject blobPart = Instantiate(Resources.Load("Gems_0"), typeof(GameObject)) as GameObject;
        GameObject blobPart = Instantiate(Resources.Load<GameObject>("Gems_0"));
        AttachCirclingArrayBlobs(blobPart);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AttachCirclingArrayBlobs(GameObject blobPart)
    {
        //get current game object's size
        BoxCollider2D boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        Vector2 center = boxCollider2D.bounds.center;
        float radius = RandomRadius(boxCollider2D);

        //get original part name until last delimeter
        //string[] tokens = blobPart.name.Split('_');
        //string substring = blobPart.name.Substring(0, endIndex + searchString.Length - startIndex);
        //tokens.Length

        for (int i = 0; i < numberBlobParts; i++)
        {
            Vector2 spawnPosition = RandomCircle(center, radius);
            GameObject blob = Instantiate(blobPart, spawnPosition, Quaternion.identity, transform);
            //name
            blob.name = blobPart.name + "[" + i + "]";
            blob.AddComponent<BlobSpinner>();
            blobs.Add(blob);
        }
    }

    private float RandomRadius(Collider2D collider2D)
    {
        float radius = (collider2D.bounds.min.x + collider2D.bounds.min.y + collider2D.bounds.max.x + collider2D.bounds.max.y) / 6 + (Random.value * 2);
        return radius;
    }

    private Vector2 RandomCircle(Vector2 center, float radius)
    {
        float ang = Random.value * 360;
        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}
