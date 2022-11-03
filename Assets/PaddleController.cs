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
        heading = GetHeading();
        rb.velocity = new Vector2(0, heading.y * movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CallComponent<WallController>(CollideWithWall, collision.gameObject);
    }

    private void CollideWithWall(WallController wallController)
    {
        Debug.Log($"Collided with {wallController.Position}");
        if (wallController.Position == WallPosition.Bottom || wallController.Position == WallPosition.Top)
        {
            rb.velocity = Vector2.zero;
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
