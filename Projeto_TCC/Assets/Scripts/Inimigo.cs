using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    private Rigidbody2D m_rb;
    public float velocidade;

    public float direcao;
    public float tempoDirecao;
    public float tempoDirecaoInicial;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

    }
}
