using Assets;
using Assets.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class BallController : BaseController
{
    public float Speed = 2f;
    public float MinLaunchDegrees = 15f;
    public float MaxLaunchDegrees = 40f;

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
            GameManager.Instance.BeginRound();
        }
        if (startAction.triggered && !clicked)
        {
            clicked = true;
            rb.simulated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CallComponent<GoalController>(CollideWithGoal, collision.gameObject);
    }

    private void CollideWithGoal(GoalController goalController)
    {
        Debug.Log($"Hit {goalController.Position} goal");
        GameManager.Instance.Score(goalController.Position);
    }

    public Vector2 GetRandomVelocity()
    {
        // generates an angle between 15 and 40 degrees in any of the 4 quadrants
        float randomAngle = Random.Range(MinLaunchDegrees, MaxLaunchDegrees) * Mathf.Deg2Rad;
        int xSign = Random.Range(0, 2) == 0 ? -1 : 1;
        int ySign = Random.Range(0, 2) == 0 ? -1 : 1;
        float x = Mathf.Cos(randomAngle) * xSign;
        float y = Mathf.Sin(randomAngle) * ySign;
        return new Vector2(x, y).normalized * Speed;
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
