using Assets.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class SetupUIManager : MonoBehaviour
    {
        private BasePlayerAgent GetAgent(int index, PlayerSide? side)
        {
            switch (index)
            {
                case 0:  return GetHumanAgent(side ?? throw new ArgumentException("No side specified for player agent", nameof(side)));
                case 1:  return GameManager.Instance.StaringAI;
                case 2:  return GameManager.Instance.NonStaringAI;
                default: throw new ArgumentException("Invalid index: " + index.ToString(), nameof(index));
            }
        }

        private BasePlayerAgent GetHumanAgent(PlayerSide side)
        {
            if (side == PlayerSide.None)
            {
                throw new ArgumentException(nameof(side));
            }
            else if (side == PlayerSide.Left)
            {
                return GameManager.Instance.LeftPlayerInput;
            }
            else if (side == PlayerSide.Right)
            {
                return GameManager.Instance.RightPlayerInput;
            }
            else
            {
                throw new Exception("Unexpected error");
            }
        }

        public void SetLeftPlayerAgent(int index)
        {
            GameManager.Instance.LeftPlayer.Agent = GetAgent(index, PlayerSide.Left);
            Debug.Log($"Left player: {GameManager.Instance.LeftPlayer.Agent}");
        }

        public void SetRightPlayerAgent(int index)
        {
            GameManager.Instance.RightPlayer.Agent = GetAgent(index, PlayerSide.Right);
            Debug.Log($"Right player: {GameManager.Instance.RightPlayer.Agent}");
        }

        public void Play()
        {
            GameManager.Instance.StartGame();
        }
    }
}
