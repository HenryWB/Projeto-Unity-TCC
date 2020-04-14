using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaHUD : MonoBehaviour
{
    public GameObject texto;
    public GameObject pular;
    // Start is called before the first frame update
    void Start()
    {
        texto.SetActive(false);
        pular.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (texto != null)
        {
            if (texto.activeSelf && Input.GetAxis("Horizontal") != 0)
            {
                texto.SetActive(false);
            }
        }

        if (pular != null)
        {
            if (pular.activeSelf && Input.GetButtonDown("Jump"))
            {
                pular.SetActive(false);
            }
        }
        else
        {
            pular = GameObject.Find("Pular");
        }
    }
}
