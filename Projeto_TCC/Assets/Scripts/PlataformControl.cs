using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformControl : MonoBehaviour
{

    public float speedX;
    public int direcaoX = 1;
    public float speedY;
    public int direcaoY = 1;

    public Transform[] point;
    private int i;
    private bool back;



    void Start()
    {
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
    }

    public void Move()
    {
        Vector3 move = new Vector3();
        move.x = speedX * direcaoX  * Time.deltaTime;
        move.y = speedY * direcaoY  * Time.deltaTime;
        transform.Translate(move);

        int distance = (int)Vector2.Distance(transform.position, point[i].position);
        if(distance <= 0) {
            if (!back)
            {
                i++;
                direcaoX = 1;
                direcaoY = 1;
            }

            if (back)
            {
                i--;
                direcaoX = -1;
                direcaoY = -1;
            }
        }
        
        if(i > point.Length - 1)
        {
            i--;
            back = true;
        }

        if(i < 0)
        {
            i++;
            back = false;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform); 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
