using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class SelectCameraState : StateMachineBehaviour
    {
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PLAYERBLE_CHARACTER_TYPE[] arr = System.Enum.GetValues(typeof(PLAYERBLE_CHARACTER_TYPE)) as PLAYERBLE_CHARACTER_TYPE[];

            foreach (PLAYERBLE_CHARACTER_TYPE p in arr)
            {
                animator.SetBool(p.ToString(), false);
            }
        }
    }
}

