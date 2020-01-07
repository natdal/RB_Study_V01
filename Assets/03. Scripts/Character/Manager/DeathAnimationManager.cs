using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class DeathAnimationManager : Singleton<DeathAnimationManager>
    {
        DeathAnimationLoader deathAnimationLoader;
        List<RuntimeAnimatorController> candidates = new List<RuntimeAnimatorController>();

        void SetupDeathAnimationLoader()
        {
            if (deathAnimationLoader == null)
            {
                GameObject obj = Instantiate(Resources.Load("DeathAnimationLoader", typeof(GameObject)) as GameObject);
                DeathAnimationLoader loader = obj.GetComponent<DeathAnimationLoader>();

                deathAnimationLoader = loader;
            }
        }

        public RuntimeAnimatorController GetAnimator(GENERAL_BODY_PART generalBodyPart, AttackInfo info)
        {
            SetupDeathAnimationLoader();

            candidates.Clear();

            foreach (DeathAnimationData data in deathAnimationLoader.deathAnimationDataList) // 쫙 돌면서 들어온 info에 맞는 값들을 찾는다. 어퍼컷의 경우 lanchtoAir다.
            {

                if (info.deathType == data.deathType)
                {
                    if (info.deathType != DEATH_TYPE.NONE)
                    {
                        candidates.Add(data.animator);
                    }
                    else if (!info.mustCollide)
                    {
                        candidates.Add(data.animator);
                    }
                    else
                    {
                        foreach (GENERAL_BODY_PART part in data.generalBodyParts)
                        {
                            if (part == generalBodyPart)
                            {
                                candidates.Add(data.animator);
                                break;
                            }
                        }
                    }
                }

            }

            return candidates[Random.Range(0, candidates.Count)]; // 후보자들중 랜덤으로 애니메이션 뽑아라. 다양하고 알차게 죽을 수 있다.
        }
    }
}


