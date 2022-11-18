using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class Startup : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.Initialize();
        }
    }
}
