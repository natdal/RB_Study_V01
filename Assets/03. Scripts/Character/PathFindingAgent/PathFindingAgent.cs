using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ver_01
{

    public class PathFindingAgent : MonoBehaviour
    {
        public bool targetPlayableCharacter;
        public GameObject target;
        NavMeshAgent navMeshAgent;

        List<Coroutine> moveRoutines = new List<Coroutine>();

        public GameObject startSphere;
        public GameObject endSphere;
        public bool startWalking;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void GoToTarget()
        {
            navMeshAgent.enabled = true;
            startSphere.transform.parent = null;
            endSphere.transform.parent = null;
            startWalking = false;

            navMeshAgent.isStopped = false;

            if (targetPlayableCharacter)
            {
                target = CharacterManager.Instance.GetPlayableCharacter().gameObject;
            }
            navMeshAgent.SetDestination(target.transform.position);

            if (moveRoutines.Count != 0)
            {
                if (moveRoutines[0] != null)
                {
                    StopCoroutine(moveRoutines[0]);
                }
                
                moveRoutines.RemoveAt(0);
            }

            moveRoutines.Add(StartCoroutine(_Move()));
        }

        IEnumerator _Move()
        {
            while (true)
            {
                if (navMeshAgent.isOnOffMeshLink)
                {
                    startSphere.transform.position = navMeshAgent.currentOffMeshLinkData.startPos;
                    endSphere.transform.position = navMeshAgent.currentOffMeshLinkData.endPos;

                    navMeshAgent.CompleteOffMeshLink();
                    
                    navMeshAgent.isStopped = true;
                    startWalking = true;
                    yield break;
                }

                Vector3 dist = transform.position - navMeshAgent.destination;
                if (Vector3.SqrMagnitude(dist) < 0.5f)
                {
                    startSphere.transform.position = navMeshAgent.destination;

                    endSphere.transform.position = navMeshAgent.destination;

                    navMeshAgent.isStopped = true;
                    startWalking = true;
                    yield break;
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}

