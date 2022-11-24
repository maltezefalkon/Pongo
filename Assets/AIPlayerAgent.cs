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
    public class AIPlayerAgent : BasePlayerAgent
    {
        public bool ShouldStare;
        public float PositionTolerance = 1.5f;
        public BallController Ball;

        public override Vector2 GetHeading(GameObject paddle)
        {
            if (!ShouldStare || BallIsMovingTowardsPaddle(paddle))
            {
                float yDistance = paddle.transform.position.y - Ball.transform.position.y;
                if (Math.Abs(yDistance) > PositionTolerance)
                {
                    if (Ball.transform.position.y > paddle.transform.position.y)
                    {
                        return new Vector2(0, 1);
                    }
                    else if (Ball.transform.position.y < paddle.transform.position.y)
                    {
                        return new Vector2(0, -1);
                    }
                }
            }
            return Vector2.zero;
        }

        bool BallIsMovingTowardsPaddle(GameObject paddle)
        {
            float ballXVelocity = Ball.GetComponent<Rigidbody2D>().velocity.x;
            PlayerSide side = paddle.GetComponent<PaddleController>().Side;
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
