using Assets.Enums;
using Assets.Scriptables;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class GameController : MonoBehaviour
    {
        public ScriptableInt RightPlayerScore;
        public ScriptableInt LeftPlayerScore;
        public GameParameters GameParameters;
        public ScriptablePlayerSideEvent GameOverEvent;

        public void HandlePointScored(PlayerSide side)
        {
            if (side == PlayerSide.Left)
            {
                LeftPlayerScore.RuntimeValue += 1;
                if (LeftPlayerScore.RuntimeValue >= GameParameters.WinningScore)
                {
                    GameOverEvent.Raise(PlayerSide.Left);
                }
            }
            else if (side == PlayerSide.Right)
            {
                RightPlayerScore.RuntimeValue += 1;
                if (RightPlayerScore.RuntimeValue >= GameParameters.WinningScore)
                {
                    GameOverEvent.Raise(PlayerSide.Right);
                }
            }
        }
    }
}
