using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting.Tool
{
    public class ComponentPool<T> where T : UnityEngine.Component
    {
        private GameObject seed;
        public List<T> usedList = new List<T>();
        public List<T> unusedList = new List<T>();



        public T Get(GameObject prefab, System.Func<T> initializer = null)
        {
            if(seed == null)
                seed = prefab;
            T t;
            if (unusedList.Count > 0){
                int index = unusedList.Count - 1;
                t = unusedList[index];
                unusedList.RemoveAt(index);
                usedList.Add(t);
            }
            else
            {
                if (initializer != null){
                    t = initializer();
                    usedList.Add(t);
                }
                else{
                    GameObject go = GameObject.Instantiate(seed);
                    go.transform.localPosition = Vector3.zero;
                    t = go.GetComponent<T>();
                    usedList.Add(t);
                }
            }
            return t;
        }

        public void Release(T t)
        {
            t.gameObject.SetActive(false);
            usedList.Remove(t);
            unusedList.Add(t);
        }

        public void ClearUnused()
        {
            for (int i = 0; i < unusedList.Count; i++)
            {
                T t = unusedList[i];
                GameObject.Destroy(t.gameObject);
            }
            unusedList.Clear();
        }

        public void ClearUsed()
        {
            for (int i = 0; i < usedList.Count; i++)
            {
                T t = usedList[i];
                GameObject.Destroy(t.gameObject);
            }
            usedList.Clear();
        }

        public void ClearAll()
        {
            ClearUnused();
            ClearUsed();
        }
    }


}
