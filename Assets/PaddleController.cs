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

    [SerializeField, SerializeReference]
    public BasePlayerAgent PlayerInput;

    // private fields
    private Rigidbody2D rb;
    private Vector2 heading = Vector2.zero;
    private bool stopped;

    void Awake()
    {
        if (side == PlayerSide.None) throw new Exception("No player side defined for PaddleController");
        PlayerInput = PlayerInput ?? GetComponent<BasePlayerAgent>() ?? GameManager.Instance.GetPlayer(side).Agent ?? throw new Exception($"Failed to determine agent for {side} {nameof(PaddleController)}");
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        heading = stopped ? Vector2.zero : PlayerInput.GetHeading(gameObject); // returns a vector of magnitude 1
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
}
