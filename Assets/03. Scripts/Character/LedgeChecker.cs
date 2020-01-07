using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class LedgeChecker : MonoBehaviour
    {
        public bool isGrabbingLedge;
        public Ledge grabbedLedge;
        Ledge checkLedge = null;

        private void OnTriggerEnter(Collider other)
        {
            checkLedge = other.gameObject.GetComponent<Ledge>();
            if (checkLedge != null)
            {
                isGrabbingLedge = true;
                grabbedLedge = checkLedge;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            checkLedge = other.gameObject.GetComponent<Ledge>();
            if (checkLedge != null)
            {
                isGrabbingLedge = false;
                //grabbedLedge = null;
            }
        }
    }
}