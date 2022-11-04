using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : BaseController
{
    public float Speed = 2f;

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
        switch (goalController.Position)
        {
            case Assets.Enums.GoalPosition.Left:
                GameManager.Instance.RightPlayerScore += 1;
                GameManager.Instance.BeginRound();
                break;
            case Assets.Enums.GoalPosition.Right:
                GameManager.Instance.LeftPlayerScore += 1;
                GameManager.Instance.BeginRound();
                break;
        }
    }

    public void SetRandomVelocity()
    {
        Vector2 random = Random.insideUnitCircle.normalized * Speed;
        rb.velocity = new Vector2(random.x, random.y / 1.5f);
    }

    public void ResetPosition()
    {
        gameObject.transform.position = Vector3.zero;
        clicked = false;
        rb.simulated = false;
    }
}
