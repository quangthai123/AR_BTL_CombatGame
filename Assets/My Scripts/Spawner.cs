using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected List<Transform> listPoolObjs;
    [SerializeField] protected Transform spawnedObjsHolder;
    protected Transform objToSpawn;
    protected void Reset()
    {
        LoadPrefabs();
        LoadHolder();
    }
    protected virtual void Start()
    {
        LoadPrefabs();
        LoadHolder();
    }

    protected void LoadHolder()
    {
        if (spawnedObjsHolder != null) return;
        spawnedObjsHolder = transform.Find("Holder");
    }

    protected void LoadPrefabs()
    {
        if (prefabs.Count > 0) return;
        Transform prefabHolder = transform.Find("Prefabs");
        foreach (Transform t in prefabHolder)
        {
            this.prefabs.Add(t);
        }
        foreach (Transform t in prefabs)
        {
            t.gameObject.SetActive(false);
        }
    }
    protected Transform GetPoolObj()
    {
        Transform targetObj;
        if (listPoolObjs.Count > 0)
        {
            targetObj = listPoolObjs[listPoolObjs.Count-1];
            listPoolObjs.Remove(listPoolObjs[listPoolObjs.Count -1]);
        } else
            targetObj = Instantiate(prefabs[0]);
        return targetObj;
    }
    public virtual void Spawn(Vector3 pos, Quaternion rot)
    {
        this.objToSpawn = GetPoolObj();
        if (objToSpawn == null) Debug.Log("Can not spawn!");
        objToSpawn.SetPositionAndRotation(pos, rot);
        objToSpawn.parent = spawnedObjsHolder;
        objToSpawn.gameObject.SetActive(true);
    }
    public void Despawn(Transform obj)
    {
        this.listPoolObjs.Add(obj);
        obj.gameObject.SetActive(false);
    }
}
