using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ver_01
{
    
    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AI/StartWalking")]
    public class StartWalking : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            Vector3 dir = control.aiProgress.pathFindingAgent.startSphere.transform.position - control.transform.position;

            if (dir.z > 0f)
            {
                control.moveRight = true;
                control.moveLeft = false;
            }
            else
            {
                control.moveRight = false;
                control.moveLeft = true;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            Vector3 dist = control.aiProgress.pathFindingAgent.startSphere.transform.position - control.transform.position;

            //jump
            if (control.aiProgress.pathFindingAgent.startSphere.transform.position.y < control.aiProgress.pathFindingAgent.endSphere.transform.position.y)
            {
                if (Vector3.SqrMagnitude(dist) < 0.01f)
                {
                    control.moveRight = false;
                    control.moveLeft = false;

                    animator.SetBool(AI_WALK_TRANSITIONS.jump_platform.ToString(), true);
                    
                }
            }

            //fall
            if (control.aiProgress.pathFindingAgent.startSphere.transform.position.y > control.aiProgress.pathFindingAgent.endSphere.transform.position.y)
            {
                animator.SetBool(AI_WALK_TRANSITIONS.fall_platform.ToString(), true);
            }

            //straight
            if (control.aiProgress.pathFindingAgent.startSphere.transform.position.y == control.aiProgress.pathFindingAgent.endSphere.transform.position.y)
            {
                if (Vector3.SqrMagnitude(dist) < 0.5f)
                {
                    control.moveRight = false;
                    control.moveLeft = false;

                    Vector3 playerDist = control.transform.position - CharacterManager.Instance.GetPlayableCharacter().transform.position;
                    if(playerDist.sqrMagnitude > 1f)
                    {
                        animator.gameObject.SetActive(false);
                        animator.gameObject.SetActive(true);
                    }
                    // temporary attack solution
                    /*else
                    {
                        if (CharacterManager.Instance.GetPlayableCharacter().damageDetector.damageTaken == 0)
                        {
                            if (control.IsFacingForward())
                            {
                                control.moveRight = true;
                                control.moveLeft = false;
                                control.attack = true;
                            }
                            else
                            {
                                control.moveRight = false;
                                control.moveLeft = true;
                                control.attack = true;
                            }
                        }
                    }
                    */
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(AI_WALK_TRANSITIONS.jump_platform.ToString(), false);
            animator.SetBool(AI_WALK_TRANSITIONS.fall_platform.ToString(), false);
        }
    }
}



