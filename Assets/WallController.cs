using Assets.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class WallController : MonoBehaviour
    {
        public WallPosition Position;
        public float Perturbance = 0.1f;
        public float GetPerturbance()
        {
            return UnityEngine.Random.Range(-Perturbance, Perturbance);
        }
    }
}
