using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector diretor;

    [SerializeField]
    private Animator animatorPlayer;

    private RuntimeAnimatorController controlador;
    private bool consertado;

    private void Awake()
    {
        if (animatorPlayer == null)
        {
            animatorPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        }
    }

    private void OnEnable()
    {
        controlador = animatorPlayer.runtimeAnimatorController;
        animatorPlayer.runtimeAnimatorController = null;
    }

    void Update()
    {
        if (animatorPlayer == null)
        {
            animatorPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        }


        if (diretor.state != PlayState.Playing && !consertado)
        {
            animatorPlayer.runtimeAnimatorController = controlador;
            consertado = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            diretor.Play();
        }
    }
}
