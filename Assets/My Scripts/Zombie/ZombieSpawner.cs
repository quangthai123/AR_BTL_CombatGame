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
    protected override void Start()
    {
        base.Start();
        if (GameManager.instance.choseDiff == 0)
        {
            InvokeRepeating("SpawnZombie", 4f, 4f);
            maxZombieQuantity = 10;
        }
        else if(GameManager.instance.choseDiff == 1)
        {
            InvokeRepeating("SpawnZombie", 3f, 3f);
            maxZombieQuantity = 20;
        }
        else
        {
            InvokeRepeating("SpawnZombie", 2f, 2f);
            maxZombieQuantity = 40;
        }

    }
    //public override void Spawn(Vector3 pos, Quaternion rot)
    //{
    //    base.Spawn(pos, rot);
    //    this.objToSpawn.GetComponent<ZombieBehaviour>().ResetStatesWhenSpawn();
    //}
    public void SpawnZombie()
    {
        if (Player.instance == null || currentZombieQuantity >= maxZombieQuantity)
            return;
        Debug.Log("Spawn New Zombie");
        Vector3 rdPos = new Vector3(Player.instance.transform.position.x +Random.Range(xRange.x, xRange.y), Player.instance.transform.position.y, Player.instance.transform.position.z + Random.Range(zRange.x, zRange.y));
        this.Spawn(rdPos, Quaternion.identity);
        currentZombieQuantity++;
    }
}
