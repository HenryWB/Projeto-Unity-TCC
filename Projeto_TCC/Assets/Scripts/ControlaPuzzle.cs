using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaPuzzle : MonoBehaviour
{
    [SerializeField] private Material materialBlur;

    [SerializeField] private float blurAmountX;
    [SerializeField] private float blurAmountY;
    [SerializeField] private GameObject puzzle;
    [SerializeField] private Player player;

    public bool passou;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        passou = false;
        puzzle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        AjusteFoco();

        if(blurAmountX == 0 && blurAmountY == 0)
        {
            passou = true;
            player.canMove = true;
            puzzle.SetActive(false);
        }

    }

    private void AjusteFoco()
    {
        float blurSpeed = 10f;
        if (blurAmountX != 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                blurAmountX -= blurSpeed * Time.deltaTime;
            }

            else if (Input.GetKey(KeyCode.D))
            {
                blurAmountX += blurSpeed * Time.deltaTime;
            }
        }

        if (blurAmountY != 0)
        {
            if (Input.GetKey(KeyCode.S))
            {
                blurAmountY -= blurSpeed * Time.deltaTime;
            }

            else if (Input.GetKey(KeyCode.W))
            {
                blurAmountY += blurSpeed * Time.deltaTime;
            }
        }

        blurAmountX = Mathf.Clamp(blurAmountX, 0f, 10f);
        blurAmountY = Mathf.Clamp(blurAmountY, 0f, 10f);

        materialBlur.SetFloat("_BlurAmountX", blurAmountX);
        materialBlur.SetFloat("_BlurAmountY", blurAmountY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                puzzle.SetActive(true);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                puzzle.SetActive(true);
                player.canMove = false;
            }
        }
    }
}
