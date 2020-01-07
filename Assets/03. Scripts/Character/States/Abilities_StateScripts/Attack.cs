using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    [CreateAssetMenu(fileName = "New State", menuName = "ver_01/AbilityData/Attack")]
    public class Attack : StateData
    {
        public bool debug;
        public float startAttackTime;
        public float endAttackTime;
        public List<string> colliderNames = new List<string>(); // 타격을 줄 객체 이름.
        public DEATH_TYPE deathType;
        //public bool lanchIntoAir;
        public bool mustCollide;
        public bool mustFaceAttacker;
        public float lethalRange;
        public int maxHits;

        private List<AttackInfo> finishedAttacks = new List<AttackInfo>();
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TRANSITION_PARAMETER.Attack.ToString(), false);

            GameObject obj = PoolManager.Instance.GetObject(POOL_OBJECT_TYPE.ATTACKINFO); 
            AttackInfo info = obj.GetComponent<AttackInfo>();

            obj.SetActive(true);
            info.ResetInfo(this, characterState.GetCharacterControl(animator));

            if (!AttackManager.Instance.currentAttacks.Contains(info)) // 현재 값이 없으면 넣어줘.
            {
                AttackManager.Instance.currentAttacks.Add(info);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RegisterAttack(characterState, animator, stateInfo);
            DeregisterAttack(characterState, animator, stateInfo);
            CheckCombo(characterState, animator, stateInfo);
        }

        public void RegisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if ((startAttackTime <= stateInfo.normalizedTime) && (endAttackTime > stateInfo.normalizedTime)) // 공격중!
            {
                foreach (AttackInfo info in AttackManager.Instance.currentAttacks)
                {
                    if (info == null)
                    {
                        continue;
                    }

                    if ((!info.isRegisterd) && (info.attackAbility == this))
                    {
                        if (debug)
                        {
                            Debug.Log(this.name + " regestered: " + stateInfo.normalizedTime); // 여기에 f9눌러서 브레이크 포인트를 만든다. 유니티 화면 보면서 임팩트 타이밍을 잡는다.
                        }

                        info.Register(this); // 주먹을 뻗기시작한 시점. 브레이크 포인트 잡으려면 여기다. Player_LeadJab의 StartAttackTime을 조절해 주면 된다.
                    }
                }
            }
        }

        public void DeregisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= endAttackTime) // 공격이 끝났어!
            {
                foreach (AttackInfo info in AttackManager.Instance.currentAttacks)
                {
                    if (info == null) // 정보가 엄서용
                    {
                        continue;
                    }

                    if ((info.attackAbility == this) && (!info.isFinished)) // 피니쉬는 아닌데 어텍이 들어왔을 때
                    {
                        info.isFinished = true; // 주먹을 다 뻗었을 시점. 브레이크 포인트 잡으려면 여기다. Player_LeadHap의 EndAttackTime을 조절해 주면 된다.
                        info.GetComponent<PoolObject>().TurnOff();

                        if (debug)
                        {
                            Debug.Log(this.name + " de-regestered: " + stateInfo.normalizedTime); // 여기에 f9눌러서 브레이크 포인트를 만든다. 유니티 화면 보면서 임팩트 타이밍을 잡는다.
                        }
                        
                    }
                }
            }
        }

        public void CheckCombo(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= startAttackTime + ((endAttackTime - startAttackTime) / 3f))
            {
                if (stateInfo.normalizedTime < endAttackTime + ((endAttackTime - startAttackTime) / 2f))
                {
                    CharacterControl control = characterState.GetCharacterControl(animator);
                    if (control.animationProgress.attackTriggered)
                    {
                        //Debug.Log("Uppercut Triggered");
                        animator.SetBool(TRANSITION_PARAMETER.Attack.ToString(), true);
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TRANSITION_PARAMETER.Attack.ToString(), false);

            ClearAttack();
        }

        public void ClearAttack()
        {
            finishedAttacks.Clear();

            foreach (AttackInfo info in AttackManager.Instance.currentAttacks)
            {
                if (info == null || info.attackAbility == this/*info.isFinished*/)
                {
                    finishedAttacks.Add(info);
                }
            }

            foreach (AttackInfo info in finishedAttacks)
            {
                if (AttackManager.Instance.currentAttacks.Contains(info))
                {
                    AttackManager.Instance.currentAttacks.Remove(info);
                }
            }

        }

    }
}

