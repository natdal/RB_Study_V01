using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AbilityData/CheckRunningTurn")]
    public class CheckRunningTurn : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.IsFacingForward())
            {
                if (control.moveLeft)
                {
                    animator.SetBool(TRANSITION_PARAMETER.Turn.ToString(), true);
                }
            }

            if (!control.IsFacingForward())
            {
                if (control.moveRight)
                {
                    animator.SetBool(TRANSITION_PARAMETER.Turn.ToString(), true);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TRANSITION_PARAMETER.Turn.ToString(), false);
        }
    }
}

