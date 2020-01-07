using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class PoolManager : Singleton<PoolManager>
    {
        public Dictionary<POOL_OBJECT_TYPE, List<GameObject>> poolDictionary = new Dictionary<POOL_OBJECT_TYPE, List<GameObject>>();

        public void SetUpDictionary()
        {
            POOL_OBJECT_TYPE[] arr = System.Enum.GetValues(typeof(POOL_OBJECT_TYPE)) as POOL_OBJECT_TYPE[];

            foreach (POOL_OBJECT_TYPE p in arr)
            {
                if (!poolDictionary.ContainsKey(p))
                {
                    poolDictionary.Add(p, new List<GameObject>());
                }
            }

        }

        public GameObject GetObject(POOL_OBJECT_TYPE objType)
        {
            if (poolDictionary.Count == 0)
            {
                SetUpDictionary();
            }
            List<GameObject> list = poolDictionary[objType];
            GameObject obj = null;

            if (list.Count > 0)
            {
                obj = list[0];
                list.RemoveAt(0);
            }
            else
            {
                obj = PoolObjectLoader.InstantiatePrefab(objType).gameObject;
            }

            return obj;

        }

        public void AddObject(PoolObject obj)
        {
            List<GameObject> list = poolDictionary[obj.poolObjectType];
            list.Add(obj.gameObject);
            obj.gameObject.SetActive(false);
        }
    }
}

