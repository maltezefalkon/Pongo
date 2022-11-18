using Assets.Enums;
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
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        public BallController Ball = FindObject<BallController>("Ball");
        public AIPlayerAgent StaringAI = FindObject<AIPlayerAgent>("AIAgent2 (Stares)");
        public AIPlayerAgent NonStaringAI = FindObject<AIPlayerAgent>("AIAgent1");
        public HumanPlayerAgent LeftPlayerInput = FindObject<HumanPlayerAgent>("LeftHumanPlayer");
        public HumanPlayerAgent RightPlayerInput = FindObject<HumanPlayerAgent>("RightHumanPlayer");
        public TextMeshProUGUI LeftPlayerScoreDisplay = FindObject<TextMeshProUGUI>("LeftPlayerScore");
        public TextMeshProUGUI RightPlayerScoreDisplay = FindObject<TextMeshProUGUI>("RightPlayerScore");
        public PaddleController LeftPlayerPaddleController = FindObject<PaddleController>("LeftPaddle");
        public PaddleController RightPlayerPaddleController = FindObject<PaddleController>("RightPaddle");

        public GameObject Particles = FindObject("Particles");
        public GameObject SetupDialog = FindObject("SetupUIPanel");
        public GameObject LeftPlayerGoal = FindObject("LeftGoal");
        public GameObject RightPlayerGoal = FindObject("RightGoal");
        public GameObject GameElements = FindObject("GameElements");
        public GameObject GameUI = FindObject("GameUI");
        public GameObject GameOverUI = FindObject("GameOverPanel");
        public GameObject StartingLeftPaddlePosition = FindObject("LeftPaddleStartingPosition");
        public GameObject StartingRightPaddlePosition = FindObject("RightPaddleStartingPosition");

        public SetupUIManager SetupUIManager = FindObject<SetupUIManager>("SetupUIManager");
        public GameOverUIManager GameOverUIManager = FindObject<GameOverUIManager>("GameOverUIManager");

        private Player _leftPlayer;
        private Player _rightPlayer;

        private GameManager()
        {
        }

        public static T FindObject<T>(string name) where T : MonoBehaviour
        {
            return FindObject(name).GetComponent<T>();
        }

        public static GameObject FindObject(string name)
        {
            return Resources.FindObjectsOfTypeAll<GameObject>().First(gameObject => gameObject.name == name);
        }

        public Player GetPlayer(PlayerSide side)
        {
            if (side == PlayerSide.None) throw new ArgumentException(nameof(side));
            if (side == PlayerSide.Left) return LeftPlayer;
            if (side == PlayerSide.Right) return RightPlayer;
            throw new Exception("Unexpected error");
        }

        public Player RightPlayer
        {
            get
            {
                if (null == _rightPlayer)
                {
                    _rightPlayer = new Player(RightPlayerScoreDisplay, RightPlayerPaddleController, RightPlayerGoal, RightPlayerInput);
                }
                return _rightPlayer;
            }
        }

        public Player LeftPlayer
        {
            get
            {
                if (null == _leftPlayer)
                {
                    _leftPlayer = new Player(LeftPlayerScoreDisplay, LeftPlayerPaddleController, LeftPlayerGoal, LeftPlayerInput);
                }
                return _leftPlayer;
            }
        }

        public int WinningScore = 3;

        public void BeginRound()
        {
            Ball.ResetPosition();
            Ball.SetRandomVelocity();
            // waits for click to launch
        }

        public void Score(GoalPosition position)
        {
            if (position == GoalPosition.Left)
            {
                RightPlayer.Score += 1;
                Highlight(RightPlayer.ScoreDisplay.transform);
            }
            else if (position == GoalPosition.Right)
            {
                LeftPlayer.Score += 1;
                Highlight(LeftPlayer.ScoreDisplay.transform);
            }

            if (RightPlayer.Score >= WinningScore)
            {
                Win(PlayerSide.Right);
            }
            else if (LeftPlayer.Score >= WinningScore)
            {
                Win(PlayerSide.Left);
            }
            else
            {
                BeginRound();
            }
        }

        private void Win(PlayerSide winningSide)
        {
            ShowGameOver(true, winningSide);
        }

        private void Highlight(Transform transform)
        {
            Particles.transform.position = transform.position;
            Particles.GetComponent<ParticleSystem>().Play();
        }

        public void StartGame()
        {
            ShowSetupUI(false);
            ShowGame(true);
            BeginRound();
        }

        private void ShowSetupUI(bool show)
        {
            EnableGameControls(!show);
            SetupDialog.SetActive(show);
        }

        private void ShowGame(bool show)
        {
            EnableGameControls(show);
            GameElements.SetActive(show);
            GameUI.SetActive(show);
        }

        public void Initialize()
        {
            ResetScores();
            ResetPositions();
            ShowGameOver(false, PlayerSide.None);
            ShowGame(false);
            ShowSetupUI(true);
        }

        private void ResetPositions()
        {
            LeftPlayer.Paddle.transform.position = StartingLeftPaddlePosition.transform.position;
            RightPlayer.Paddle.transform.position = StartingRightPaddlePosition.transform.position;
        }

        private void ResetScores()
        {
            LeftPlayer.Score = 0;
            RightPlayer.Score = 0;
        }

        private void ShowGameOver(bool show, PlayerSide winningSide)
        {
            EnableGameControls(!show);
            GameOverUIManager.WinningSide = winningSide;
            GameOverUI.SetActive(show);
        }

        private void EnableGameControls(bool enabled)
        {
            LeftPlayerPaddleController.enabled = enabled;
            RightPlayerPaddleController.enabled = enabled;
        }
    }
}
