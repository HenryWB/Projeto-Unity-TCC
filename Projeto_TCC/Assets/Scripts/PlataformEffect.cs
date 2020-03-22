using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformEffect : MonoBehaviour
{
    private bool transpor;
    private PlatformEffector2D plant;

    // Start is called before the first frame update
    void Start()
    {
        plant = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transpor)
        {
            if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    plant.rotationalOffset = 180;
                }
            }
        }
        else
        {
            plant.rotationalOffset = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transpor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transpor = false;
        }
    }
}
