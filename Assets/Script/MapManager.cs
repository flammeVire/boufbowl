using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<GameObject> AllObject;

    public static MapManager instance;

    private void Start()
    {
        instance = this;
    }


    public List<GameObject> GetAllObject() { return AllObject; }

    public void AddItemOnMap(GameObject obj)
    {
        AllObject.Add(obj);
    }

    public void RemoveItemAtList(GameObject obj)
    {
        AllObject.Remove(obj);
    }

    public bool SomethingOverlap(GameObject obj)
    {
        foreach(var i in AllObject)
        {
            if (Vector3Int.CeilToInt(obj.transform.position)== Vector3Int.CeilToInt(i.transform.position))
            {
                return true;
            }
        }
        return false;
    }
}
