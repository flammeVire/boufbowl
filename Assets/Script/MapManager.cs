using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    List<GameObject> AllObject;

    public static MapManager mapManager;

    private void Start()
    {
        mapManager = this;
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
}
