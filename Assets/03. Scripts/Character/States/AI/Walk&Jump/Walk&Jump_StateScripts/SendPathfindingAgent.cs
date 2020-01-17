using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ver_01
{
    public enum AI_WALK_TRANSITIONS
    {
        start_walking,
        jump_platform,
        fall_platform,

        start_running,
    }

    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AI/SendPathfindingAgent")]
    public class SendPathfindingAgent : StateData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (control.aiProgress.pathFindingAgent == null)
            {
                GameObject p = Instantiate(Resources.Load("PathFindingAgent", typeof(GameObject)) as GameObject);
                control.aiProgress.pathFindingAgent = p.GetComponent<PathFindingAgent>();
            }

            control.aiProgress.pathFindingAgent.GetComponent<NavMeshAgent>().enabled = false;
            control.aiProgress.pathFindingAgent.transform.position = control.transform.position + (Vector3.up * 0.25f);
            control.aiProgress.pathFindingAgent.GoToTarget();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (control.aiProgress.pathFindingAgent.startWalking)
            {
                animator.SetBool(AI_WALK_TRANSITIONS.start_walking.ToString(), true);
                animator.SetBool(AI_WALK_TRANSITIONS.start_running.ToString(), true);
            }

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(AI_WALK_TRANSITIONS.start_walking.ToString(), false);
            animator.SetBool(AI_WALK_TRANSITIONS.start_running.ToString(), false);
        }
    }
}



