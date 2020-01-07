using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ver_01
{

    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AI/JumpPlatform")]
    public class JumpPlatform : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            control.jump = true;
            control.moveUp = true;

            if(control.aiProgress.pathFindingAgent.startSphere.transform.position.z < control.aiProgress.pathFindingAgent.endSphere.transform.position.z)
            {
                control.FaceForward(true);
            }
            else
            {
                control.FaceForward(false);
            }

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            
            float topDist = control.aiProgress.pathFindingAgent.endSphere.transform.position.y
                - control.frontSpheres[1].transform.position.y;

            float bottomDist = control.aiProgress.pathFindingAgent.endSphere.transform.position.y 
                - control.frontSpheres[0].transform.position.y;

            if (topDist < 1.5f && bottomDist > 0.5f) // 여기서 Left가 되버리는데... 내가 보기엔 걍 수치 문제다. 일단 넘어가자... 보정할거다.
            {
                if (control.IsFacingForward()) // 얼굴 방향
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

            if (bottomDist < 0.5f)
            {
                control.moveRight = false;
                control.moveLeft = false;
                control.moveUp = false;
                control.jump = false;

                animator.gameObject.SetActive(false);
                animator.gameObject.SetActive(true);
            }

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}

