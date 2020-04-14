using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector diretor;
    
    [SerializeField]
    private Animator animatorPlayer;

    private RuntimeAnimatorController controlador;
    private bool consertado;
    private bool ativado;

    // Start is called before the first frame update
    private void Awake()
    {
        if (diretor == null)
        {
            diretor = GameObject.FindGameObjectWithTag("Diretor").GetComponent<PlayableDirector>();
        }

        if(animatorPlayer == null)
        {
            animatorPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        }

        Debug.Log(ativado);
    }
    void Start()
    {
        diretor.Play();

        Debug.Log(ativado);
    }

    private void OnEnable()
    {
        controlador = animatorPlayer.runtimeAnimatorController;
        animatorPlayer.runtimeAnimatorController = null;
        Debug.Log(ativado);
    }

    void Update()
    {
        if (diretor == null)
        {
            diretor = GameObject.FindGameObjectWithTag("Diretor").GetComponent<PlayableDirector>();
        }

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

    public void Stop()
    {
        diretor.Stop();
    }

    public void Play()
    {
        diretor.Play();
    }
}
