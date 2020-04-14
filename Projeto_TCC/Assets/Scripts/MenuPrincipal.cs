using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject menuInicial;
    public GameObject opcoes;

    // Start is called before the first frame update
    void Start()
    {
        menuInicial.SetActive(true);
        opcoes.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Opcoes()
    {
        menuInicial.SetActive(false);
        opcoes.SetActive(true);
    }
}
