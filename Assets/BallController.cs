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
    [Range(1, 5)] public int LaunchFlatteningFactor = 3;

    private Rigidbody2D rb;
    private PaddleMovement paddleMovement;
    private InputAction fire;
    private bool clicked = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
        paddleMovement = new PaddleMovement();
        fire = paddleMovement.Player.Fire;
        fire.Enable();
        GameManager.Instance.BeginRound();
    }

    private void Update()
    {
        if (fire.triggered && !clicked)
        {
            clicked = true;
            rb.simulated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CallComponent<GoalController>(CollideWithGoal, collision.gameObject);
        //CallComponent<PaddleController>(CollideWithPaddle, collision.gameObject);
    }

    //private void CollideWithPaddle(PaddleController paddleController)
    //{
    //    rb.velocity = new Vector2(-(rb.velocity.x + (rb.velocity.x * paddleController.GetPerturbance())), rb.velocity.y + (rb.velocity.y * paddleController.GetPerturbance()));
    //    Debug.Log($"Hit {paddleController.side} paddle");
    //}

    private void CollideWithGoal(GoalController goalController)
    {
        Debug.Log($"Hit {goalController.Position} goal");
        GameManager.Instance.Score(goalController.Position);
    }

    public void SetRandomVelocity()
    {
        IEnumerable<Vector2> candidates = Enumerable.Repeat<object>(null, LaunchFlatteningFactor).Select(x => Random.insideUnitCircle.normalized).ToList();
        float smallestY = candidates.Min(v => Math.Abs(v.y));
        Vector2 random = candidates.First(v => Math.Abs(v.y) == smallestY);
        Vector2 vel = random * Speed;
        rb.velocity = vel;
    }

    public void ResetPosition()
    {
        gameObject.transform.position = Vector3.zero;
        clicked = false;
        rb.simulated = false;
    }
}
