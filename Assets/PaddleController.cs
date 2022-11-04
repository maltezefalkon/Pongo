using Assets;
using Assets.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controller responsible for moving a paddle using <see cref="PaddleMovement"/>
/// </summary>
public class PaddleController : BaseController
{
    // settable in editor
    public float movementSpeed = 5f;
    public PlayerSide side = PlayerSide.None;
    public float Perturbance = 0.2f;
    public bool IsAI = false;

    // private fields
    private PaddleMovement paddleMovement;
    private Rigidbody2D rb;
    private Vector2 heading = Vector2.zero;
    private InputAction move;
    private bool stopped;

    void Awake()
    {
        paddleMovement = new PaddleMovement();
        move = side == PlayerSide.Left ? paddleMovement.Player.LeftPlayerMove : paddleMovement.Player.RightPlayerMove;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (!IsAI)
        {
            move.Enable();
        }
    }

    private void OnDisable()
    {
        if (!IsAI)
        {
            move.Disable();
        }
    }

    private void FixedUpdate()
    {
        heading = stopped ? Vector2.zero : GetHeading(); // returns a vector of magnitude 1
        rb.velocity = new Vector2(0, heading.y * movementSpeed);
        stopped = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            stopped = true;
        }
    }

    protected virtual Vector2 GetHeading()
    {
        if (!IsAI)
        {
            return move.ReadValue<Vector2>();
        }
        else
        {
            if (GameManager.Instance.Ball.gameObject.transform.position.y > gameObject.transform.position.y)
            {
                return new Vector2(0, 1);
            }
            else if (GameManager.Instance.Ball.gameObject.transform.position.y < gameObject.transform.position.y)
            {
                return new Vector2(0, -1);
            }
            else
            {
                return Vector2.zero;
            }
        }
    }

    public float GetPerturbance()
    {
        return UnityEngine.Random.Range(-Perturbance, Perturbance);
    }

}
