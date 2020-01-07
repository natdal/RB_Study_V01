using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AbilityData/CheckTurboAndMovement")]
    public class CheckTurboAndMovement : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

            CharacterControl control = characterState.GetCharacterControl(animator);

            if ((control.moveLeft || control.moveRight) && control.turbo)
            {
                animator.SetBool(TRANSITION_PARAMETER.Move.ToString(), true);
                animator.SetBool(TRANSITION_PARAMETER.Turbo.ToString(), true);
            }
            else
            {
                animator.SetBool(TRANSITION_PARAMETER.Move.ToString(), false);
                animator.SetBool(TRANSITION_PARAMETER.Turbo.ToString(), false);
            }

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}

