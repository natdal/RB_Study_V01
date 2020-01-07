using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class PoolObject : MonoBehaviour
    {
        public POOL_OBJECT_TYPE poolObjectType;
        public float scheduledOffTime;
        private Coroutine offRoutine;

        private void OnEnable()
        {
            if (offRoutine != null)
            {
                StopCoroutine(offRoutine);
            }
            if (scheduledOffTime > 0f)
            {
                offRoutine = StartCoroutine(_ScheduledOff());
            }
        }

        public void TurnOff()
        {
            this.transform.parent = null;
            this.transform.position = Vector3.zero;
            this.transform.rotation = Quaternion.identity;
            PoolManager.Instance.AddObject(this);
        }

        IEnumerator _ScheduledOff()
        {
            yield return new WaitForSeconds(scheduledOffTime);

            if (!PoolManager.Instance.poolDictionary[poolObjectType].Contains(this.gameObject))
            {
                TurnOff();
            }
        }
    }
}

