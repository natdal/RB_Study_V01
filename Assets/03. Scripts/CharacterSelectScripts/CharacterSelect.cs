using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ver_01
{
    public enum PLAYERBLE_CHARACTER_TYPE
    {
        NONE,
        YELLOW,
        RED,
        GREEN,
        //BLUE,
    }

    [CreateAssetMenu(fileName = "chracterSelect", menuName ="ver_01/CharacterSelect/CharacterSelect")]
    public class CharacterSelect : ScriptableObject
    {
        public PLAYERBLE_CHARACTER_TYPE selectedCharacterType;
    }

}
