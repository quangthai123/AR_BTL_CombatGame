using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieSpawner : Spawner
{
    public int maxZombieQuantity;
    public int currentZombieQuantity;
    public static ZombieSpawner instance;
    [SerializeField] private Vector2 xRange;
    [SerializeField] private Vector2 zRange;
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    private void Start()
    {
        InvokeRepeating("SpawnZombie", 2f, 2f);
    }
    //public override void Spawn(Vector3 pos, Quaternion rot)
    //{
    //    base.Spawn(pos, rot);
    //    this.objToSpawn.GetComponent<ZombieBehaviour>().ResetStatesWhenSpawn();
    //}
    private void SpawnZombie()
    {
        Debug.Log("Spawn New Zombie");
        if (Player.instance == null || currentZombieQuantity >= maxZombieQuantity)
            return;
        Vector3 rdPos = new Vector3(Player.instance.transform.position.x +Random.Range(xRange.x, xRange.y), Player.instance.transform.position.y, Player.instance.transform.position.z + Random.Range(zRange.x, zRange.y));
        this.Spawn(rdPos, Quaternion.identity);
        currentZombieQuantity++;
    }
}
