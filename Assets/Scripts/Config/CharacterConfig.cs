using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;


namespace Shooting.Asset
{
    public enum CharacterType
    {
        Player = 0,
        Enemy = 1,
    }
	[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Character/Character Settings")]
	public class CharacterConfig : ScriptableObject
	{
		[Serializable]
		public class CharacterInfo
		{
			public string name;
            public CharacterType characterType;
			public GameObject go;
		}

		public List<CharacterInfo> characterInfos = new List<CharacterInfo>();


		public CharacterInfo GetCharacterInfo(string name)
		{
			foreach(var oi in characterInfos){
				if (oi.name == name)
					return oi;
			}
			return null;
		}

		public void SetupWithDict(Dictionary <string, GameObject> dict)
		{
			characterInfos.Clear();
			foreach(var _kvp in dict)
			{
				CharacterInfo info = new CharacterInfo();
				info.name = _kvp.Key;
				info.go = _kvp.Value;
				characterInfos.Add(info);
			}
		}

        
        public void AutoSet()
        {
            foreach(var character in characterInfos)
            {
 //#if UNITY_EDITOR
                if(character.go == null)
                        continue;
               // var prefab = PrefabUtility.GetCorrespondingObjectFromSource<GameObject>(character.go);
                //if(prefab == null)
                //{
                //    Debug.Log($"can't find {character.go}'s prefab!");
                 //   continue;
                //}
                //character.name = prefab.name;
                character.name = character.go == null? string.Empty:character.go.name;
                Debug.Log($"find_{character.name}!");
//#endif
                
            }
        }


	}
}