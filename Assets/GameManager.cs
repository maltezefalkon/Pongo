using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets
{
    public class GameManager
    {
        public static GameManager Instance = new GameManager();

        public BallController Ball = GameObject.Find("Ball").GetComponent<BallController>();
        public GameObject LeftPlayerPaddle = GameObject.Find("LeftPaddle");
        public GameObject RightPlayerPaddle = GameObject.Find("RightPaddle");
        public GameObject LeftPlayerDefendingGoal = GameObject.Find("ArenaLeftWall");
        public GameObject RightPlayerDefendingGoal = GameObject.Find("ArenaRightWall");
        public TextMeshProUGUI LeftPlayerScoreDisplay = GameObject.Find("LeftPlayerScore").GetComponent<TextMeshProUGUI>();
        public TextMeshProUGUI RightPlayerScoreDisplay = GameObject.Find("RightPlayerScore").GetComponent<TextMeshProUGUI>();

        private int _rightPlayerScore = 0;
        private int _leftPlayerScore = 0;

        public int RightPlayerScore
        {
            get { return _rightPlayerScore; }
            set
            {
                _rightPlayerScore = value;
                RightPlayerScoreDisplay.text = _rightPlayerScore.ToString();
            }
        }

        public int LeftPlayerScore
        {
            get { return _leftPlayerScore; }
            set
            {
                _leftPlayerScore = value;
                LeftPlayerScoreDisplay.text = _leftPlayerScore.ToString();
            }
        }

        private GameManager()
        {
        }


        public void BeginRound()
        {
            Ball.ResetPosition();
            Ball.SetRandomVelocity();
        }
    }
}
