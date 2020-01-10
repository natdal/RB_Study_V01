using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class DamageDetector : MonoBehaviour
    {
        CharacterControl control;
        GENERAL_BODY_PART damagedPart;

        public int damageTaken;

        private void Awake()
        {
            damageTaken = 0;
            control = GetComponent<CharacterControl>();
        }

        private void Update()
        {
            if (AttackManager.Instance.currentAttacks.Count > 0)
            {
                CheckAttack();
            }
        }

        private void CheckAttack()
        {
            foreach (AttackInfo info in AttackManager.Instance.currentAttacks)
            {
                if (info == null) // 맞은놈이 없어.
                {
                    continue; // 지나갑니다.
                }

                if (!info.isRegisterd) // 등록되지 않은 목표여.
                {
                    continue; // 지나갑니다.
                }

                if (info.isFinished) // 떡실신 됬어.
                {
                    continue; // 지나갑니다.
                }

                if (info.currentHits >= info.maxHits) // 최대 공격력보다 현재 데미지가 크면 뭔가 문제가 있는거다.
                {
                    continue; // 그러므로 지나갑니다.
                }

                if (info.attacker == control) // 때리는 사람이 나 자신이면
                {
                    continue; // 지나가세요.
                }

                if (info.mustFaceAttacker) // 해머 땅 찍을 때
                {
                    Vector3 vec = this.transform.position - info.attacker.transform.position;
                    if ((vec.z * info.attacker.transform.forward.z) < 0f)
                    {
                        continue;
                    }
                }

                if (info.mustCollide) // 걍 때리는겨. 딱 명치 때리는거
                {
                    if (IsCollided(info)) // 맞았네?
                    {
                        TakeDamage(info); // 아파야겠네?
                    }
                }
                else // 충격파로 때리는거. 명치도 포함이여.
                {
                    float dist = Vector3.SqrMagnitude(this.gameObject.transform.position - info.attacker.transform.position);
                    Debug.Log(this.gameObject.name + " dist: " + dist.ToString());
                    if (dist <= info.lethalRange) // 거리 안에 있냐
                    {
                        TakeDamage(info);
                    }
                }
            }
        }

        private bool IsCollided(AttackInfo info) // 명확하잖여 위에서 플래그 선언에서 true 주는것보다 이렇게 하면 더 직관적이여. 메서드 이름으로부터 뭐인지 아니까.
        {
            foreach (TriggerDetector trigger in control.GetAllTriggers())
            {
                foreach (Collider collider in trigger.collidingParts)
                {
                    foreach (ATTACK_PART_TYPE part in info.attackParts)
                    {
                        if (part == ATTACK_PART_TYPE.LEFT_HAND)
                        {
                            if (collider.gameObject == info.attacker.leftHand_Attack)
                            {
                                damagedPart = trigger.generalBodyPart; // 어디 맞았어?
                                return true;
                            }
                        }
                        else if (part == ATTACK_PART_TYPE.RIGHT_HAND)
                        {
                            if (collider.gameObject == info.attacker.rightHand_Attack)
                            {
                                damagedPart = trigger.generalBodyPart; // 어디 맞았어?
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private void TakeDamage(AttackInfo info)
        {
            //////////////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! hp 시스템 자리!!!!!!!!!!!!! 중요!!!!!!!
            if (damageTaken > 0) // 여기에 hp 시스템을 만들면 된다. 근데 지금은 한방컷이라 걍 나감.
            {
                return;
            }

            if (!info.mustCollide)
            {
                CameraManager.Instance.ShakeCamera(0.35f);
            }
            
            Debug.Log(info.attacker.gameObject.name + " hits: " + this.gameObject.name);
            Debug.Log(this.gameObject.name + " hit " + damagedPart.ToString());

            control.skinnedMeshAnimator.runtimeAnimatorController = DeathAnimationManager.Instance.GetAnimator(damagedPart, info); // 데미지 받은 부위랑 info에 따라 애니메이터 컨트롤러를 바꾼다.
            info.currentHits++;

            control.GetComponent<BoxCollider>().enabled = false;
            control.ledgeChecker.GetComponent<BoxCollider>().enabled = false;
            control.RIGID_BODY.useGravity = false;
            
            damageTaken++; // 위에 hp 시스템 봐바. 임시로 한번 데미지 주면 더이상 데미지 안주게 만든거 뿐임.
        }
    }
}

