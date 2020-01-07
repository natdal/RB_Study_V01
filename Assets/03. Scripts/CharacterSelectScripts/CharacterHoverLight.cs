using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public class CharacterHoverLight : MonoBehaviour
    {
        public Vector3 offset = new Vector3();

        CharacterControl hoverSelectedCharacter;
        MouseControl mouseHoverSelect;
        Vector3 targetPos = new Vector3();
        Light light;

        private void Start()
        {
            mouseHoverSelect = GameObject.FindObjectOfType<MouseControl>();
            light = GetComponent<Light>();
        }
        
        private void Update()
        {
            if (mouseHoverSelect.selectedCharacterType == PLAYERBLE_CHARACTER_TYPE.NONE)
            {
                hoverSelectedCharacter = null;
                light.enabled = false;
            }
            else
            {
                light.enabled = true;
                LightUpSelectedCharacter();
            }
        }

        private void LightUpSelectedCharacter()
        {
            if (hoverSelectedCharacter == null)
            {
                hoverSelectedCharacter = CharacterManager.Instance.GetCharacter(mouseHoverSelect.selectedCharacterType);
                this.transform.position = hoverSelectedCharacter.skinnedMeshAnimator.transform.position + hoverSelectedCharacter.transform.TransformDirection(offset);
                this.transform.parent = hoverSelectedCharacter.skinnedMeshAnimator.transform;
            }
        }
        
    }

}
