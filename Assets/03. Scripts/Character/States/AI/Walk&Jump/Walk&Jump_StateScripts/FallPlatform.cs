﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AI/FallPlatform")]
    public class FallPlatform : StateData
    {
        

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.transform.position.z < control.aiProgress.pathFindingAgent.endSphere.transform.position.z)
            {
                control.FaceForward(true);
            }else if (control.transform.position.z < control.aiProgress.pathFindingAgent.endSphere.transform.position.z)
            {
                control.FaceForward(false);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            
            if (control.IsFacingForward())
            {
                if (control.transform.position.z < control.aiProgress.pathFindingAgent.endSphere.transform.position.z)
                {
                    control.moveRight = true;
                    control.moveLeft = false;
                }
                else
                {
                    control.moveRight = false;
                    control.moveLeft = false;

                    animator.gameObject.SetActive(false);
                    animator.gameObject.SetActive(true);
                }
            }
            else
            {
                if (control.transform.position.z > control.aiProgress.pathFindingAgent.endSphere.transform.position.z)
                {
                    control.moveRight = false;
                    control.moveLeft = true;
                }
                else
                {
                    control.moveRight = false;
                    control.moveLeft = false;

                    animator.gameObject.SetActive(false);
                    animator.gameObject.SetActive(true);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}

