using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ver_01
{

    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AI/StartRunning")]
    public class StartRunning : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            Vector3 dir = control.aiProgress.pathFindingAgent.startSphere.transform.position - control.transform.position;

            if (dir.z > 0f)
            {
                control.FaceForward(true);
                control.moveRight = true;
                control.moveLeft = false;
            }
            else
            {
                control.FaceForward(false);
                control.moveRight = false;
                control.moveLeft = true;
            }

            control.turbo = true;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            Vector3 dist = control.aiProgress.pathFindingAgent.startSphere.transform.position - control.transform.position;

            if (Vector3.SqrMagnitude(dist) < 2f)
            {
                control.moveRight = false;
                control.moveLeft = false;
                control.turbo = false;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}



