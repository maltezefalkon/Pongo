using Assets.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets
{
    [Serializable]
    public class HumanPlayerInput : BasePlayerAgent
    {
        [SerializeField]
        public PlayerSide Side;

        private PaddleMovement paddleMovement;
        private InputAction moveAction;

        public void Awake()
        {
            if (Side == PlayerSide.None) throw new Exception("Can't determine player side");
            paddleMovement = new PaddleMovement();
            moveAction = moveAction = Side == PlayerSide.Left ? paddleMovement.Player.LeftPlayerMove : paddleMovement.Player.RightPlayerMove;
        }

        public void OnEnable()
        {
            moveAction.Enable();
        }

        public void OnDisable()
        {
            moveAction.Disable();
        }

        public override Vector2 GetHeading(GameObject paddle)
        {
            return moveAction.ReadValue<Vector2>();
        }

        public override string ToString()
        {
            return $"Human player ({Side})";
        }
    }
}
