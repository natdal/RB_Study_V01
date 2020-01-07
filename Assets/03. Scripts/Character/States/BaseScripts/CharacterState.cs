using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class CharacterState : StateMachineBehaviour // StateMachineBehaviour : Animator에 넣을 스크립트
    {
        //public CharacterControl characterControl;
        public List<StateData> listAbilityData = new List<StateData>(); // 행동 종류

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) // 상태에 들어왔어
        {
            foreach (StateData d in listAbilityData)
            {
                d.OnEnter(this, animator, stateInfo); // 스탯 리스트를 쫙 넣어서 상태를 들어가
            }
        }

        public void UpdateAll(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo) // 들어온 애니메이션 전부다 업데이트
        {
            foreach (StateData d in listAbilityData)
            {
                d.UpdateAbility(characterState, animator, stateInfo); // Statdata의 추상클래스에 캐릭터의 상태, 애니메이터, 현 상태 정보를 넘겨줘서 모든 정보값들을 업데이트한다.
            }
        }

        // 상속받은 OnStateUpdate는 프레임마다 호출되는 메서드다. 여기에 현재 animator를 업데이트 시켜 준다.
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator, stateInfo); // 그니깐 뭐 실행비슷하다고 봐야지
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StateData d in listAbilityData)
            {
                d.OnExit(this, animator, stateInfo); // 상태를 나가
            }
        }

        private CharacterControl characterControl;
        public CharacterControl GetCharacterControl(Animator animator) // 캐릭터컨트롤러의 애니메이터 찾아서 가져와.
        {
            if (characterControl == null) // 없으면
            {
                characterControl = animator.transform.root.GetComponent<CharacterControl>(); // AI를 선택하면 안되니까
                //characterControl = animator.GetComponentInParent<CharacterControl>(); // 지금 구동중인 애니메이터 찾아.
            }
            return characterControl; // 있으면 걍 넣어.
        }
    }
}

