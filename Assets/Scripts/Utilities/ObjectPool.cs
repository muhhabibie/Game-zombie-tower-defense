using System.Collections.Generic;
using UnityEngine;

namespace LastBastion.Utilities
{
    public class ObjectPool<T> where T : Component
    {
        private readonly Queue<T> pool = new Queue<T>();
        private readonly T prefab;
        private readonly Transform parent;

        public ObjectPool(T prefab, int initialSize = 0, Transform parent = null)
        {
            this.prefab = prefab;
            this.parent = parent;
            for (int i = 0; i < initialSize; i++)
            {
                var obj = GameObject.Instantiate(prefab, parent);
                obj.gameObject.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        public T Get()
        {
            if (pool.Count > 0)
            {
                var obj = pool.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }
            return GameObject.Instantiate(prefab, parent);
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}