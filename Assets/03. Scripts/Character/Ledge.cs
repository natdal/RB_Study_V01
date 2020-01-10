﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class Ledge : MonoBehaviour
    {
        public Vector3 offset;
        public Vector3 endPosition;

        public static bool IsLedge(GameObject obj)
        {
            if (obj.GetComponent<Ledge>() == null)
            {
                return false;
            }

            return true;
        }

        public static bool IsLedgeChecker(GameObject obj)
        {
            if (obj.GetComponent<LedgeChecker>() == null)
            {
                return false;
            }

            return true;
        }

        public static bool IsCharacter(GameObject obj)
        {
            if (obj.transform.root.GetComponent<CharacterControl>() == null)
            {
                return false;
            }

            return true;
        }
    }

}
