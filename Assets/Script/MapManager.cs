using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public List<GameObject> AllObject;

    public static MapManager instance;

    public Transform[] SpawnPoint;

    private void Start()
    {
        SpawnPoint = new Transform[AllObject.Count];
        for(int i = 0; i < SpawnPoint.Length; i++)
        {
            SpawnPoint[i] = AllObject[i].transform;
        }
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
        foreach (var i in AllObject)
        {
            if (i == null) continue;

            if (i == obj) continue;

            if (Vector3Int.FloorToInt(obj.transform.position) ==
                Vector3Int.FloorToInt(i.transform.position))
            {
                return true;
            }
        }

        return false;
    }

    public void ResetAllSpawn()
    {
        for(int i = 0; i< SpawnPoint.Length; i++)
        {
            AllObject[i].transform.position = SpawnPoint[i].position;
        }
    }
}
