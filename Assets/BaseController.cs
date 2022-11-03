using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class BaseController : MonoBehaviour
    {
        protected BaseController CallComponent<T>(Action<T> action, GameObject gObject = null) where T : MonoBehaviour
        {
            GameObject g = gObject ?? gameObject;
            T foundComponent = g.GetComponent<T>();
            if (foundComponent != null)
            {
                action(foundComponent);
                return null;
            }
            return this;
        }

        protected BaseController CallComponent(Type componentType, Action<MonoBehaviour> action, GameObject gObject = null)
        {
            GameObject g = gObject ?? gameObject;
            MonoBehaviour foundComponent = g.GetComponent(componentType) as MonoBehaviour;
            if (foundComponent != null)
            {
                action(foundComponent);
                return null;
            }
            return this;
        }
    }
}
