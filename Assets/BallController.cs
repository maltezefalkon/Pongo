using Assets;
using Assets.Enums;
using Assets.Scriptables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class BallController : BaseController
{
    public ScriptableBallSpeedRank Speed;
    public ScriptablePlayerSideEvent PointScoredEvent;
    public GameParameters GameParameters;

    public float VelocityMultiplier = 0.08f;
    public float MinLaunchDegrees = 10f;
    public float MaxLaunchDegrees = 50f;

    private Rigidbody2D rb;
    private PaddleMovement paddleMovement;
    private InputAction startAction;
    private InputAction resetAction;
    private bool clicked = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
        paddleMovement = new PaddleMovement();
        startAction = paddleMovement.Player.Fire;
        resetAction = paddleMovement.Player.Reset;
    }

    private void OnEnable()
    {
        startAction.Enable();
        resetAction.Enable();
        ResetBall();
    }

    private void OnDisable()
    {
        startAction.Disable();
        resetAction.Disable();
    }

    private void Update()
    {
        if (resetAction.triggered)
        {
            ResetBall();
        }
        if (startAction.triggered && !clicked)
        {
            clicked = true;
            rb.simulated = true;
        }
    }

    private void ResetBall()
    {
        ResetPosition();
        SetRandomVelocity();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            rb.velocity *= 1 + VelocityMultiplier;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CallComponent<GoalController>(CollideWithGoal, collision.gameObject);
    }

    private void CollideWithGoal(GoalController goalController)
    {
        Debug.Log($"Hit {goalController.Position} goal");
        if (goalController.Position == GoalPosition.Left)
        {
            PointScoredEvent.Raise(PlayerSide.Right);
        }
        else if (goalController.Position == GoalPosition.Right)
        {
            PointScoredEvent.Raise(PlayerSide.Left);
        }
        ResetBall();
    }

    public Vector2 GetRandomVelocity()
    {
        // generates an angle between MinLaunchDegrees and MaxLaunchDegrees degrees in any of the 4 quadrants
        float randomAngle = Random.Range(MinLaunchDegrees, MaxLaunchDegrees) * Mathf.Deg2Rad;
        int xSign = Random.Range(0, 2) == 0 ? -1 : 1;
        int ySign = Random.Range(0, 2) == 0 ? -1 : 1;
        float x = Mathf.Cos(randomAngle) * xSign;
        float y = Mathf.Sin(randomAngle) * ySign;
        return new Vector2(x, y).normalized * ConvertBallSpeed(Speed.RuntimeValue);
    }

    public float ConvertBallSpeed(BallSpeedRank speed)
    {
        switch (speed)
        {
            case BallSpeedRank.VeryFast: return GameParameters.VeryFastBallSpeed;
            case BallSpeedRank.Fast: return GameParameters.FastBallSpeed;
            case BallSpeedRank.Medium: return GameParameters.MediumBallSpeed;
            case BallSpeedRank.Slow: return GameParameters.SlowBallSpeed;
            default: throw new ArgumentOutOfRangeException(nameof(speed));
        }
    }

    public void SetRandomVelocity()
    {
        rb.velocity = GetRandomVelocity();
    }

    public void ResetPosition()
    {
        gameObject.transform.position = Vector3.zero;
        clicked = false;
        rb.simulated = false;
    }
}
