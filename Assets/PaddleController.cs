using Assets;
using Assets.Enums;
using Assets.Scriptables;
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
    public float MovementSpeed = 5f;
    public PlayerSide Side = PlayerSide.None;
    public ScriptableAgentType AgentType;

    public HumanPlayerAgent LeftHumanAgent;
    public HumanPlayerAgent RightHumanAgent;
    public AIPlayerAgent DefaultAIAgent;
    public AIPlayerAgent StaringAIAgent;

    // private fields
    private Rigidbody2D rb;
    private Vector2 heading = Vector2.zero;
    private bool stopped;

    void Awake()
    {
        if (Side == PlayerSide.None) throw new Exception("No player side defined for PaddleController");
        rb = GetComponent<Rigidbody2D>();
    }

    public BasePlayerAgent PlayerAgent
    {
        get
        {
            switch (AgentType.RuntimeValue)
            {
                case Assets.Enums.AgentType.AI_Staring: return StaringAIAgent;
                case Assets.Enums.AgentType.AI_Default: return DefaultAIAgent;
                case Assets.Enums.AgentType.Human:
                    if (Side == PlayerSide.Left) return LeftHumanAgent;
                    if (Side == PlayerSide.Right) return RightHumanAgent;
                    break;
            }
            throw new ArgumentOutOfRangeException(nameof(Assets.Enums.AgentType));
        }
    }

    private void OnEnable()
    {
        stopped = false;
        rb.velocity = Vector2.zero;
    }

    private void OnDisable()
    {
        stopped = true;
        rb.velocity = Vector2.zero;
    }

    private void Update()
    {
        heading = stopped ? Vector2.zero : PlayerAgent.GetHeading(gameObject); // returns a vector of magnitude 1
        rb.velocity = new Vector2(0, heading.y * MovementSpeed);
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
