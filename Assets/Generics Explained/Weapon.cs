using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class Weapon<T> : MonoBehaviour where T: MonoBehaviour // T가 MonoBehaviour에 있다. 이거 안해주면 모노외에 다른 클래스가 들어갈 수 있어서 AddComponent같은 경우엔 에러가 뜬다.
    {
        private void Start()
        {
            Declare();
        }

        void Declare()
        {
            Debug.Log("hello i am a " + typeof(T));

            if (!this.gameObject.name.Contains("copy")) // 카피라는 단어가 들어간게 하나도 없을 때. 이거 안씌워주면 AddComponent때문에 계속 돈다.
            {
                GameObject copy = new GameObject();
                copy.name = "copy of a " + typeof(T);

                copy.AddComponent<T>(); // T 범위 지정해줘야 됨.
            }
        }
    }
}

