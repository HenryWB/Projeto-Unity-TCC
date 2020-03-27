using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Atributos")]
    private Rigidbody2D Rb;

    private bool isOnGruond;
    private bool canMove = true;
    private bool JumpPressedDown;
    private bool JumpPressed;
    private bool JumpUp;
    private bool isJump;
    private bool isDashing;


    [Header("Movement")]
    public float speed;
    private float speedOrign;
    private float horizontal;
    private float vertical;

    [Header("Jump")]
    public LayerMask groundLayer;
    public Transform feet;
    public int multiJump;
    private int totalPulos;
    public float feetOffSet;
    public float groundDistance;
    public float jumpForce;
    public float radiusFeet;
    public float timeJump;
    private float jumpTimeCounter;

    [Header("Wall")]
    public LayerMask wallLayer;
    public float wallOffSet;
    public float verticalJumpForce;
    public float horizontalJumpForce;
    public float wallRadius;
    public float maxFallSpeed;
    public float wallJumpDuration;
    public float jumpFinish;
    private bool onWall;
    private bool jumpFromWall;

    [Header("Dash")]
    public float dashTime;
    public float dashSpeed;
    public float distanceGhost;
    public float dashCoolDown;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100;

    [Header("Flip")]
    public bool facingRight = true;
    private int direcao = 1;

    [Header("Swim")]
    public float seaOffSet;
    public float seaRadius;
    public float speedInWater;
    public float folego;
    public LayerMask seaLayer;
    private float maxFolego;
    private bool isWater;
    private bool isSwimming;

    [Header("Sine Wave")]
    public float frequencia;
    public float magnitude;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(feet.position, radiusFeet);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(wallOffSet, 0f), wallRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + new Vector3(-wallOffSet, 0f), wallRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0f, seaOffSet), seaRadius);
    }

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        totalPulos = multiJump;
        speedOrign = speed;
        maxFolego = folego;
    }

    private void FixedUpdate()
    {
        TestarContato();
        Movimento();
        MovimentoAgua();
    }

    private void Update()
    {
        InputCheck();
        Pular();
        WallJump();
        CheckDash();
        Flip();
    }

    public void Movimento()
    {
        if (!canMove)
        {
            return;
        }

        if (isWater)
        {
            speed = speedOrign / 2;
        }
        else if (isOnGruond)
        {
            speed = speedOrign;
        }

        Vector2 move = Rb.velocity;
        move.x = horizontal * speed * 100 * Time.deltaTime;

        Rb.velocity = move;

        if (!onWall)
        {
            if (horizontal > 0)
            {
                facingRight = true;
            }

            if (horizontal < 0)
            {
                facingRight = false;
            }
        }
    }

    public void MovimentoAgua()
    {
        if (isWater && !isSwimming)
        {
            Rb.gravityScale = 4f;
        } else
        {
            Rb.gravityScale = 5f;
        }

        if (isSwimming)
        {
            Rb.gravityScale = 0;
            Vector2 move = Rb.velocity;
            move.x = horizontal * speedInWater * 100 * Time.deltaTime;
            move.y = vertical * speedInWater * 100 * Time.deltaTime;

            if (horizontal == 0 && vertical == 0)
            {
                //move.x = speedInWater * 100 * Mathf.Cos(Time.time * frequencia) * magnitude;
                move.y = speedInWater * 100 * Mathf.Sin(Time.time * frequencia) * magnitude;
            }


            Rb.velocity = move;

            
        }
    }

    public void Pular()
    {
        if (JumpPressedDown && isOnGruond)
        {
            isJump = true;
            jumpTimeCounter = timeJump;
            Rb.velocity = Vector2.up * jumpForce * 100 * Time.deltaTime;
        }

        if (multiJump > 0 && !isOnGruond && JumpPressedDown)
        {
            Rb.velocity = Vector2.up * jumpForce * 190 * Time.deltaTime;
            multiJump--;
        }

        if (JumpPressed && isJump)
        {
            if (jumpTimeCounter > 0)
            {
                Rb.velocity = Vector2.up * jumpForce * 100 * Time.deltaTime;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJump = false;
            }
        }

        if (JumpUp)
        {
            isJump = false;
        }

        if (isOnGruond || onWall || isWater)
        {
            multiJump = totalPulos;
        }
    }

    public void WallJump()
    {
        if(onWall && !isOnGruond && JumpPressedDown)
        {
            canMove = false;
            jumpFinish = Time.time + wallJumpDuration;
            JumpPressedDown = false;
            jumpFromWall = true;
            facingRight = !facingRight;
            Rb.velocity = Vector2.up * verticalJumpForce * 100 * Time.deltaTime;
            Rb.velocity += Vector2.right * direcao * horizontalJumpForce * 100 * Time.deltaTime;
        }
    }

    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                Rb.velocity = new Vector2(dashSpeed * -direcao, Rb.velocity.y);
                dashTimeLeft -= Time.deltaTime;


                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceGhost)
                {
                    PlayerGhostPool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }

            if(dashTime <= 0 || onWall)
            {
                isDashing = false;
                canMove = true;
            }
        }
    }

    public void TestarContato()
    {
        isOnGruond = false;
        onWall = false;
        isWater = false;

        bool contato = Physics2D.OverlapCircle(feet.position, radiusFeet, groundLayer);
        isOnGruond = contato;

        bool water = Physics2D.OverlapCircle(transform.position + new Vector3(0f, seaOffSet), seaRadius, seaLayer);
        isWater = water;

        bool rigthWall = Physics2D.OverlapCircle(transform.position + new Vector3(wallOffSet, 0f), wallRadius, wallLayer);
        bool leftWall = Physics2D.OverlapCircle(transform.position + new Vector3(-wallOffSet, 0f), wallRadius, wallLayer);

        if (leftWall || rigthWall)
        {
            onWall = true;
        }

        if (onWall && direcao != 0)
        {
            if(Rb.velocity.y < maxFallSpeed)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, maxFallSpeed);
            }
        }


        
    }

    public void InputCheck()
    {
        JumpPressedDown = Input.GetButtonDown("Jump") && !Input.GetKey(KeyCode.S);
        JumpPressed = Input.GetKey(KeyCode.Space);
        JumpUp = Input.GetKeyUp(KeyCode.Space);

        if (canMove)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        if (jumpFromWall)
        {
            if(Time.time > jumpFinish)
            {
                jumpFromWall = false;
            }
        }

        if (!jumpFromWall && !canMove) 
        { 
            if(Input.GetAxis("Horizontal") != 0 || isOnGruond)
            {
                canMove = true;
            }
        }

        if (Input.GetButtonDown("Dash"))
        {
            if(Time.time >= (lastDash + dashCoolDown))
            {
                AttemptToDash();
            }
        }

        if (isWater && !isSwimming)
        {
            if (Input.GetKey(KeyCode.S))
            {
                isSwimming = true;
            }
        }
        else if (!isWater)
        {
            isSwimming = false;
        }
    }

    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerGhostPool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    private void Flip()
    {
        Quaternion rotacao = new Quaternion();
        if (facingRight)
        {
            rotacao.y = 0f; 
            direcao = -1;            
        }

        if (!facingRight)
        {
            rotacao.y = 180f;
            direcao = 1;
        }

        transform.rotation = rotacao;
    }

    public void Folego()
    {
        if (isSwimming)
        {
            if(folego > 0)
            {
                folego -= Time.deltaTime;
            }
            else if(folego <= 0)
            {
                //Morre;
            }
        }
        else
        {
            if (folego < maxFolego) {
                folego += Time.deltaTime;
            } 

            if(folego > maxFolego)
            {
                folego = maxFolego;
            }
        }
    }
}
