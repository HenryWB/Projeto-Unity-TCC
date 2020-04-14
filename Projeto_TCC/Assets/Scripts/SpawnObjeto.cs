using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjeto : MonoBehaviour
{
    public GameObject obj;
    public float espera;
    public float inicio;
    private float tempoSpawn;
    private float cdSpawn;

    void Start()
    {
        tempoSpawn = inicio;
    }

    // Update is called once per frame
    void Update()
    {

        if (cdSpawn >= tempoSpawn)
        {
            Instantiate(obj, gameObject.transform.position, Quaternion.identity);
            tempoSpawn += espera;
        }
        else
        {
            cdSpawn += Time.deltaTime;
        }

    }
}
