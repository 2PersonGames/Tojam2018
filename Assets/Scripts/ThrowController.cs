﻿using UnityEngine;

public class ThrowController : MonoBehaviour
{
    private const float FORCE = 500.0f;

    public GameObject ThrownObjectPrefab;  

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var direction = GetDirection();
        if (direction != Vector2.zero && Input.GetButtonDown("Fire1"))
        {
            var blob = Instantiate(ThrownObjectPrefab, gameObject.transform.position, Quaternion.identity);
            blob.tag = "Blob";
            blob.GetComponent<Rigidbody2D>().AddForce(
                (direction * Time.deltaTime * FORCE), 
                ForceMode2D.Impulse);
        }
    }

    private Vector2 GetDirection()
    {
        return new Vector2(Input.GetAxis("RightH"), Input.GetAxis("RightV"));
    }
}