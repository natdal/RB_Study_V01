using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class IteratingPlayer : MonoBehaviour
    {
        public List<int> intList = new List<int>();
        public List<string> stringList = new List<string>();

        public void IterateThrough<T>(List<T> targetList)
        {
            foreach (T data in targetList)
            {
                Debug.Log(data);
            }
        }

        private void Start()
        {
            IterateThrough(intList);
            IterateThrough(stringList);
        }
    }
}

