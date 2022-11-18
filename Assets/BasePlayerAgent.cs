using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    [Serializable]
    public abstract class BasePlayerAgent : MonoBehaviour
    {
        public abstract Vector2 GetHeading(GameObject paddle);
    }
}
