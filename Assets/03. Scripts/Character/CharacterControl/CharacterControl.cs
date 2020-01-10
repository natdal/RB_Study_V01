using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{

    public enum TRANSITION_PARAMETER
    {
        Move,
        Jump,
        ForceTransition,
        Grounded,
        Attack,
        ClickAnimation,
        TransitionIndex,
        Turbo,
        Turn,
    }

    public enum GAME_SCENES
    {
        SelectScene,
        GameScene
    }

    public class CharacterControl : MonoBehaviour
    {
        
        // Input Manager로 조작하기 위해서 따로 만들어줌.
        [Header("Input")]
        public bool turbo;
        public bool moveUp;
        public bool moveDown;
        public bool moveRight;
        public bool moveLeft;
        public bool jump;
        public bool attack;

        [Header("SubComponents")]
        public LedgeChecker ledgeChecker;
        public AnimationProgress animationProgress;
        public AIProgress aiProgress;
        public DamageDetector damageDetector;
        //public GameObject colliderEdgePrefab;
        public List<GameObject> bottomSpheres = new List<GameObject>();
        public List<GameObject> frontSpheres = new List<GameObject>();

        // 중력계산
        [Header("Gravity")]
        public float gravityMultiplier;
        public float pullMultiplier;

        [Header("Setup")]
        public PLAYERBLE_CHARACTER_TYPE playerbleCharacterType;
        public Animator skinnedMeshAnimator;
        public Material material;
        public List<Collider> ragdollParts = new List<Collider>();
        public GameObject leftHand_Attack;
        public GameObject rightHand_Attack;

        private List<TriggerDetector> triggerDetectors = new List<TriggerDetector>();
        private Dictionary<string, GameObject> childObjects = new Dictionary<string, GameObject>(); // 찾은 자식 몸부분은 또 쓸거니까 담아둔다.

        private Rigidbody rigid;
        public Rigidbody RIGID_BODY
        {
            get
            {
                if (rigid == null)
                {
                    rigid = GetComponent<Rigidbody>();
                }
                return rigid;
            }
        }

        private void Awake()
        {
            bool switchBack = false;

            if (!IsFacingForward())
            {
                switchBack = true;
            }

            FaceForward(true);
            SetColliderSpheres(); // 충돌판정

            if (switchBack)
            {
                FaceForward(false);
            }

            ledgeChecker = GetComponentInChildren<LedgeChecker>();
            animationProgress = GetComponent<AnimationProgress>();
            aiProgress = GetComponentInChildren<AIProgress>();
            damageDetector = GetComponentInChildren<DamageDetector>();

            RegisterCharacter(); // 이렇게 해야 뭔 캐릭터인줄 바로 찾지
        }

        private void RegisterCharacter() // 캐릭터매니져에 나를 포함시켜
        {
            if (!CharacterManager.Instance.characters.Contains(this))
            {
                CharacterManager.Instance.characters.Add(this);
            }
        }

        // 자식들 트리거 다 들고와
        public List<TriggerDetector> GetAllTriggers()
        {
            if (triggerDetectors.Count == 0)
            {
                TriggerDetector[] arr = this.gameObject.GetComponentsInChildren<TriggerDetector>();

                foreach (TriggerDetector d in arr)
                {
                    triggerDetectors.Add(d); // 자식들 트리거 다들고와서 넣어줌.
                }
            }

            return triggerDetectors;
        }

        /*
        private IEnumerator Start() // 죽기 시작해!, Start를 써서 걍 프로그램 시작하면 5초 뒤에 죽어!
        {
            yield return new WaitForSeconds(5f);
            RIGID_BODY.AddForce(200f * Vector3.up);
            yield return new WaitForSeconds(0.5f);
            TurnOnRagdoll();
        }
        */

        public void SetRagdollParts()
        {
            ragdollParts.Clear();

            Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

            foreach (Collider c in colliders)
            {
                if(c.gameObject != this.gameObject) // 이 오브젝트가 현제 게임오브젝트에 포함 안되면. 그러니까 최상위에 있는것만 남는겨
                {
                    if (c.gameObject.GetComponent<LedgeChecker>() == null)
                    {
                        c.isTrigger = true; // 이러면 통과하지롱
                        ragdollParts.Add(c);

                        if (c.GetComponent<TriggerDetector>() == null) // 트리거 디텍터가 없으면
                        {
                            c.gameObject.AddComponent<TriggerDetector>(); // 각 레그돌에 트리거디텍터를 붙여준다.
                        }
                    }
                }
            }
        }

        public void TurnOnRagdoll() // 수달이가 죽었어!
        {
            RIGID_BODY.useGravity = false; // 강체 필요엄쪄!
            RIGID_BODY.velocity = Vector3.zero; // 속도 필요엄쪄!
            this.gameObject.GetComponent<BoxCollider>().enabled = false; // 최상위 콜라이더 해제하고
            skinnedMeshAnimator.enabled = false; // 애니메이션 필요엄서!
            skinnedMeshAnimator.avatar = null; // 아바타도 필요엄성!

            foreach (Collider c in ragdollParts) // 각각 레그돌 파츠들을 활성화
            {
                c.isTrigger = false; // 이제 충돌하지롱
                c.attachedRigidbody.velocity = Vector3.zero; // 방향 필요엄서!
            }
        }

        private void SetColliderSpheres()
        {
            BoxCollider box = GetComponent<BoxCollider>();

            float bottom = box.bounds.center.y - box.bounds.extents.y; // 중점에서 반지름을 빼니깐 아래다.
            float top = box.bounds.center.y + box.bounds.extents.y; // 중점에서 반지름을 더하니까 위다.
            float front = box.bounds.center.z + box.bounds.extents.z; // 중점에서 반지름을 더하니까 앞이다.
            float back = box.bounds.center.z - box.bounds.extents.z; // 중점에서 반지름을 빼니까 뒤다.

            GameObject bottomFrontHor = CreateEdgeSphere(new Vector3(0f, bottom, front));
            GameObject bottomFrontVer = CreateEdgeSphere(new Vector3(0f, bottom + 0.05f, front));
            GameObject bottomBack = CreateEdgeSphere(new Vector3(0f, bottom, back));
            GameObject topFront = CreateEdgeSphere(new Vector3(0f, top, front));

            bottomFrontHor.transform.parent = this.transform;
            bottomFrontVer.transform.parent = this.transform;
            bottomBack.transform.parent = this.transform;
            topFront.transform.parent = this.transform;

            bottomSpheres.Add(bottomFrontHor);
            bottomSpheres.Add(bottomBack);

            frontSpheres.Add(bottomFrontVer);
            frontSpheres.Add(topFront);

            float horSec = (bottomFrontHor.transform.position - bottomBack.transform.position).magnitude / 5f; // 5조각으로 범위를 나눠서 한 섹션의 길이를 계산.
            CreateMiddleSpheres(bottomFrontHor, -this.transform.forward, horSec, 4, bottomSpheres);

            float verSec = (bottomFrontVer.transform.position - topFront.transform.position).magnitude / 10f; // 5조각으로 범위를 나눠서 한 섹션의 길이를 계산.
            CreateMiddleSpheres(bottomFrontVer, this.transform.up, verSec, 9, frontSpheres);
        }

        private void FixedUpdate()
        {
            if (RIGID_BODY.velocity.y < 0f)
            {
                RIGID_BODY.velocity += (-Vector3.up * gravityMultiplier);
            }

            if (RIGID_BODY.velocity.y > 0f && !jump)
            {
                RIGID_BODY.velocity += (-Vector3.up * pullMultiplier);
            }
        }

        public void CreateMiddleSpheres(GameObject start, Vector3 dir, float sec, int interations, List<GameObject> spheresList)
        {
            for (int i = 0; i < interations; i++)
            {
                Vector3 pos = start.transform.position + (dir * sec * (i + 1)); // 시작점으로부터 loop가 한번씩 돌때마다 sec길이만큼 더 멀어짐.

                GameObject newObj = CreateEdgeSphere(pos);
                newObj.transform.parent = this.transform;
                spheresList.Add(newObj);
            }
        }

        public GameObject CreateEdgeSphere(Vector3 pos)
        {
            GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), pos, Quaternion.identity) as GameObject;
            return obj;
        }

        public void MoveForward(float speed, float speedGraph) // 앞으로 움직여. Stat의 MoveForward 스텟에서 호출함.
        {
            transform.Translate(Vector3.forward * speed * speedGraph * Time.deltaTime); // 앞으로 움직이는거
        }

        public void FaceForward(bool forward) // 시작할때 바라볼 방향
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(GAME_SCENES.SelectScene.ToString()))
            {
                return;
            }

            if (forward)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }

        public bool IsFacingForward() // 크... 이게 island 퍼즐 방법이구나... manualInput을 끄고 켜는거였구만... 그러면 같은 방향을 또 바라보게 되네...
        {
            if(transform.forward.z > 0f) // 오른쪽
            {
                return true;
            }
            else // 왼쪽
            {
                return false;
            }
        }

        // 몸 부위 찾는거. 카메라같은거 시점 설정할 때 이걸로 선택함.
        public Collider GetBodyPart(string name)
        {
            foreach (Collider c in ragdollParts)
            {
                if (c.name.Contains(name))
                {
                    return c;
                }
            }

            return null;
        }

        // 몸의 특정 부분 가져오는거
        public GameObject GetChildObj(string name)
        {
            if (childObjects.ContainsKey(name)) // 한번 찾았던 거는 다시 찾을 필요 없으니까
            {
                return childObjects[name];
            }

            Transform[] arr = this.gameObject.GetComponentsInChildren<Transform>();

            foreach (Transform t in arr)
            {
                if (t.gameObject.name.Equals(name))
                {
                    childObjects.Add(name, t.gameObject);
                    return t.gameObject;
                }
            }

            return null;
        }


        public void ChangeMaterial() // 인스펙터 창에서 Mat이랑 Shader 쉽게 바꾸는 방법.
        {
            if (material == null) // 메테리얼이 없을 때
            {
                Debug.LogError("No material specified");
            }

            Renderer[] arrMaterials = this.gameObject.GetComponentsInChildren<Renderer>(); // 하위에 있는 놈들 다 찾아.

            foreach (Renderer r in arrMaterials) // 하위에 있는 놈들 싹다 바꿔
            {
                if (r.gameObject != this.gameObject) // 이 오브젝트가 입력한 material과 이름이 다를 때.
                {
                    r.material = material; // 메테리얼 적용해.
                }
            }
        }
        
    }
}

