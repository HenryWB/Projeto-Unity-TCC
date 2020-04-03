using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegarItem : MonoBehaviour
{
    public int idItem;
    private Inventario inventario;

    private void Start()
    {
        inventario = FindObjectOfType<Inventario>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventario.AddItem(idItem);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventario.AddItem(idItem);
                Destroy(gameObject);
            }
        }
    }
}
