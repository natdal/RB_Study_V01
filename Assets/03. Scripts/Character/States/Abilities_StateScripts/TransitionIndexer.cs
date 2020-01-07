using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public enum TRANSITION_CONDITION_TYPE
    {
        Up,
        Down,
        Left,
        Right,
        Attack,
        Jump,
        Grabbing_ledge,
    }

    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AbilityData/TransitionIndexer")]
    public class TransitionIndexer : StateData
    {
        public int index;
        public List<TRANSITION_CONDITION_TYPE> transitionConditions = new List<TRANSITION_CONDITION_TYPE>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (MakeTransition(control))
            {
                animator.SetInteger(TRANSITION_PARAMETER.TransitionIndex.ToString(), index);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (MakeTransition(control))
            {
                animator.SetInteger(TRANSITION_PARAMETER.TransitionIndex.ToString(), index);
            }
            else{
                animator.SetInteger(TRANSITION_PARAMETER.TransitionIndex.ToString(), 0);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetInteger(TRANSITION_PARAMETER.TransitionIndex.ToString(), 0);
        }

        private bool MakeTransition(CharacterControl control)
        {
            foreach (TRANSITION_CONDITION_TYPE c in transitionConditions)
            {
                switch (c)
                {
                    case TRANSITION_CONDITION_TYPE.Up:
                        {
                            if (!control.moveUp)
                            {
                                return false;
                            }
                        }
                        break;
                    case TRANSITION_CONDITION_TYPE.Down:
                        {
                            if (!control.moveDown)
                            {
                                return false;
                            }
                        }
                        break;
                    case TRANSITION_CONDITION_TYPE.Left:
                        {
                            if (!control.moveLeft)
                            {
                                return false;
                            }
                        }
                        break;
                    case TRANSITION_CONDITION_TYPE.Right:
                        {
                            if (!control.moveRight)
                            {
                                return false;
                            }
                        }
                        break;
                    case TRANSITION_CONDITION_TYPE.Attack:
                        {
                            if (!control.animationProgress.attackTriggered /*!control.attack*/)
                            {
                                return false;
                            }
                        }
                        break;
                    case TRANSITION_CONDITION_TYPE.Jump:
                        {
                            if (!control.jump)
                            {
                                return false;
                            }
                        }
                        break;
                    case TRANSITION_CONDITION_TYPE.Grabbing_ledge:
                        {
                            if (!control.ledgeChecker.isGrabbingLedge)
                            {
                                return false;
                            }
                        }
                        break;
                }
            }

            return true;
        }
        
    }
}

