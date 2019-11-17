using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using  System;


namespace Shooting.Asset
{
	[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Character/Character Settings")]
	public class CharacterConfig : ScriptableObject
	{
		[Serializable]
		public class CharacterInfo
		{
			public string name;
			public GameObject value;
		}

		public List<CharacterInfo> characterInfos = new List<CharacterInfo>();


		public CharacterInfo GetOffsetInfo(string name)
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
				info.value = _kvp.Value;
				characterInfos.Add(info);
			}
		}

	}
}