using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AbilityData/Idle")]
    public class Idle : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TRANSITION_PARAMETER.Jump.ToString(), false);
            animator.SetBool(TRANSITION_PARAMETER.Attack.ToString(), false);
            animator.SetBool(TRANSITION_PARAMETER.Move.ToString(), false);

            CharacterControl control = characterState.GetCharacterControl(animator);
            control.animationProgress.disallowEarlyTurn = false;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

            CharacterControl control = characterState.GetCharacterControl(animator);
            
            if (control.animationProgress.attackTriggered/*control.attack*/)
            {
                animator.SetBool(TRANSITION_PARAMETER.Attack.ToString(), true);
            }

            if (control.jump)
            {
                animator.SetBool(TRANSITION_PARAMETER.Jump.ToString(), true);
            }

            if (control.moveLeft && control.moveRight)
            {
                // do nothing
            }else if (control.moveRight)
            {
                animator.SetBool(TRANSITION_PARAMETER.Move.ToString(), true);
            }else if (control.moveLeft)
            {
                animator.SetBool(TRANSITION_PARAMETER.Move.ToString(), true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TRANSITION_PARAMETER.Attack.ToString(), false);
        }
    }
}

