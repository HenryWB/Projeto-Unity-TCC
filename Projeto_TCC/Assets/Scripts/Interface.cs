using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public GameObject inventario;
    private bool inv;

    private void Start()
    {
        inventario = FindObjectOfType<Inventario>().gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inv = !inv;
        }


        inventario.SetActive(inv);
    }

}
