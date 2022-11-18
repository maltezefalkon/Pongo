using Assets.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets
{
    public class Player
    {
        private TextMeshProUGUI _scoreDisplay;
        private PaddleController _paddle;
        private GameObject _goal;

        public Player(TextMeshProUGUI scoreDisplay, PaddleController paddle, GameObject goal, BasePlayerAgent agent)
        {
            _scoreDisplay = scoreDisplay;
            _paddle = paddle;
            _goal = goal;
            _agent = agent;
        }

        private int _score = 0;
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                ScoreDisplay.text = _score.ToString();
            }
        }

        public PaddleController Paddle => _paddle;

        public TextMeshProUGUI ScoreDisplay => _scoreDisplay;

        private BasePlayerAgent _agent;
        public BasePlayerAgent Agent
        {
            get
            {
                return _agent;
            }
            set
            {
                _agent = value;
                Paddle.PlayerInput = _agent;
            }
        }
    }
}