using System.Collections.Generic;
using UnityEngine;
using Shooting.Tool;
using Shooting.Asset;
using System;

namespace Shooting.Content
{
    public class AssetsManager:SingletonMonoBehaviour<AssetsManager>
    {
        public CharacterConfig characterConfig;

        private Dictionary<string,ComponentPool<Character>> assetPool = new Dictionary<string, Tool.ComponentPool<Character>>();

        protected override void Awake() 
        {
            if(characterConfig == null || characterConfig.characterInfos == null)
            {
                Debug.LogError($"Character Config is null!");
                return;
            }
            foreach(var character in characterConfig.characterInfos)
            {
                if(assetPool.ContainsKey(character.name))
                {
                    Debug.Log($"character {character.name} is exist!");
                    continue;
                }
                if(character.go == null)
                {
                    Debug.Log($"character {character.name} is null!");
                    continue;
                }
                var characterPool = new ComponentPool<Character>();
                characterPool.AddSeed(character.go);
                assetPool.Add(character.name,characterPool);
            } 

        }

        public void CreateAsset(string name,Action<GameObject,Character> callback)
        {
            if(assetPool.TryGetValue(name,out ComponentPool<Character> pool))
            {
                var character = pool.Get(null,null);
                character.init();
                callback?.Invoke(character.gameObject,character);
                return;
            }
            callback?.Invoke(null,null);
        }

        public void ReleaseAsset(string name,Character character)
        {
            if(assetPool.TryGetValue(name,out ComponentPool<Character> pool))
            {
                character.Reset();
                pool.Release(character);
            }
        }
        

    }

}
