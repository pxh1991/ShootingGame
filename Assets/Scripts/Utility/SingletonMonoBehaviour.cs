using UnityEngine;

namespace Shooting.Tool
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance = null;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        #if UNITY_EDITOR
                        Debug.LogError(typeof(T) + " is nothing");
                        #endif
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(this);
            }
            else
            {
                Debug.Log(typeof(T) + "is exist!");
            }
        }
}
}