using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galho : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("head"))
        {
            collision.gameObject.GetComponentInParent<SpawnPlayer>().Spawn();
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0.25f;
            Destroy(gameObject, 1f);
        }
    }
}
