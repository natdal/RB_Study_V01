using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AbilityData/Landing")]
    public class Landing : StateData
    {
        

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TRANSITION_PARAMETER.Jump.ToString(), false);
            animator.SetBool(TRANSITION_PARAMETER.Move.ToString(), false);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.jump)
            {
                //characterState.GetCharacterControl(animator).RIGID_BODY.AddForce(Vector3.up * jumpForce);
                animator.SetBool(TRANSITION_PARAMETER.Jump.ToString(), true);
                animator.SetBool(TRANSITION_PARAMETER.Move.ToString(), true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

    }
}

