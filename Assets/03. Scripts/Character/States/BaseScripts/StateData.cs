using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public abstract class StateData : ScriptableObject // ScriptableObject : 메소드를 실행하는 스크립트가 아닌 데이터를 사용하기 위한 스크립트
    {
        public abstract void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo); // 현 상태에 들어왔을 때
        public abstract void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo); // 상태를 업데이트해라.
        public abstract void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo); // 현 상태를 나갔을 때
    }

}
