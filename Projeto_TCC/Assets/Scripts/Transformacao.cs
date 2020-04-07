using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformacao : MonoBehaviour
{
    private int formas;
    private string nomeFormaAnterior;
    private string nomeForma;
    // Start is called before the first frame update
    void Start()
    {
        nomeForma = "Espirito";
    }

    // Update is called once per frame
    void Update()
    {
        ManagerTransformation();
        AteraForma();
        //Debug.Log(nomeForma);
        //Debug.Log(formas);

    }

    public void ManagerTransformation()
    {
        switch (nomeForma)
        {
            case "Espirito":
                formas = 0;
                Espirito();
                break;

            case "Caranguejo":
                formas = 1;
                Carangueijo();
                break;
        }
        Player.formaPlayer = (FORMAS)formas;
    }

    public void AteraForma()
    {
        string guarda = "";
        if (Input.GetKeyDown(KeyCode.T))
        {
            guarda = nomeForma;
            nomeForma = nomeFormaAnterior;
            nomeFormaAnterior = guarda;
        }
    } 
    public void Espirito()
    {
        Vector3 scala = new Vector3(1f, 1f, 1);
        transform.localScale = scala;
    }

    public void Carangueijo()
    {
        Vector3 scala = new Vector3(1f, 0.5f, 1);
        transform.localScale = scala;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                nomeFormaAnterior = nomeForma;
                nomeForma = collision.gameObject.tag;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                nomeFormaAnterior = nomeForma;
                nomeForma = collision.gameObject.tag;
            }
        }
    }
}
