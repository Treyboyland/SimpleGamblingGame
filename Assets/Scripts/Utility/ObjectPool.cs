using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    T prefab;

    List<T> pool = new List<T>();

    T CreateObject()
    {
        var newObj = Instantiate(prefab, transform);
        newObj.gameObject.SetActive(false);
        pool.Add(newObj);

        return newObj;
    }

    public T GetObject()
    {
        foreach (var obj in pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                return obj;
            }
        }

        return CreateObject();
    }

    public int GetActiveObjectCount()
    {
        return pool.Where(x => x.gameObject.activeInHierarchy).Count();
    }

    public List<T> GetActiveObjects()
    {
        return pool.Where(x => x.gameObject.activeInHierarchy).ToList();
    }
}
