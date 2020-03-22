using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;

    [SerializeField]
    private float alphaSet = 0.8f;
    private float alphaMutiplier = 0.85f;

    private Transform player;

    private SpriteRenderer SR;
    private SpriteRenderer playerSR;

    private Color color;


    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActivated = Time.time;
    }

    private void Update()
    {
        alpha *= alphaMutiplier;
        color = new Color(1f,1f,1f,alpha);
        SR.color = color;


        if(Time.time >= (timeActivated + activeTime))
        {
            PlayerGhostPool.Instance.AddToPool(gameObject);
        }
    }
}
