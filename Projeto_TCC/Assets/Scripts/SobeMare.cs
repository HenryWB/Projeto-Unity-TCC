using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SobeMare : MonoBehaviour
{
    public bool ativaMare;
    private GameMaster gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        if (gm.numeroCheck == 3)
        {
            transform.position = new Vector3(transform.position.x, -11.7f, 0);
            ativaMare = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ativaMare)
        {
            if (transform.position.y < -11.7f)
            {
                transform.Translate(0, 0.02f, 0);
            }
        }
    }

    public void AtivaMare()
    {
        ativaMare = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ativaMare)
        {
            collision.gameObject.GetComponentInParent<SpawnPlayer>().Spawn();
        }
    }

}
