using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour // 하이어라키에 없으면 만들어. 있으면 하나로 유지해. 싱글 턴 ~
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                //_instance = (T)FindObjectOfType(typeof(T)); // 찾아. 스크립트를. 이거 안해줘도 어차피 밑에 if문에서 판단한다. 
                if (_instance == null) // 없어
                {
                    GameObject obj = new GameObject(); // 그럼 오브젝트 하나 만들어
                    _instance = obj.AddComponent<T>(); // 오브젝트에 나 자신을 추가해.
                    obj.name = typeof(T).ToString(); // 이름을 스크립트명 그대로 넣어.
                }
                return _instance; // 이제 넌 스태틱 싱글턴~뾰로롱
            }
        }
    }
}

