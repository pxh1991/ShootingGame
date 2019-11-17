using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooting.Tool;
using Shooting.Asset;

namespace Shooting.Content
{
    public class AssetsManager:SingletonMonoBehaviour<AssetsManager>
    {
        public CharacterConfig characterConfig;

        public Dictionary<string,ComponentPool<Character>> assetPool = new Dictionary<string, Tool.ComponentPool<Character>>();

        protected override void Awake() 
        {
            

        }
    }

}
