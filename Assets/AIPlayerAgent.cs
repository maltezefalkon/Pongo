using Assets.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.InputSystem;

namespace Assets
{
    [Serializable]
    public class AIPlayerAgent : BasePlayerAgent
    {
        public bool ShouldStare;
        public float PositionTolerance = 1.5f;

        public override Vector2 GetHeading(GameObject paddle)
        {
            GameObject ball = GameManager.Instance.Ball.gameObject;
            if (!ShouldStare || BallIsMovingTowardsPaddle(ball, paddle))
            {
                float yDistance = paddle.transform.position.y - ball.transform.position.y;
                if (Math.Abs(yDistance) > PositionTolerance)
                {
                    if (ball.transform.position.y > paddle.transform.position.y)
                    {
                        return new Vector2(0, 1);
                    }
                    else if (ball.transform.position.y < paddle.transform.position.y)
                    {
                        return new Vector2(0, -1);
                    }
                }
            }
            return Vector2.zero;
        }

        bool BallIsMovingTowardsPaddle(GameObject ball, GameObject paddle)
        {
            float ballXVelocity = ball.GetComponent<Rigidbody2D>().velocity.x;
            PlayerSide side = paddle.GetComponent<PaddleController>().side;
            switch (side)
            {
                case PlayerSide.Left:
                    return ballXVelocity < 0;
                case PlayerSide.Right:
                    return ballXVelocity > 0;
            }
            return false;
        }

        public override string ToString()
        {
            if (ShouldStare)
            {
                return "Staring AI";
            }
            else 
            {
                return "Non-staring AI";
            }
        }
    }
}
