using UnityEngine;

public class BlobSpinner : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var angles = transform.rotation.eulerAngles;
        angles.z += Time.deltaTime * 50;
        transform.rotation = Quaternion.Euler(angles);
    }
}
