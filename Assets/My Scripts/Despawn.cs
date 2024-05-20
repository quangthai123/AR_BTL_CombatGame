using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    private void OnEnable()
    {
        if(gameObject.tag == "Bullet")
            Invoke("DespawnGO", 1.5f);
    }

    private void DespawnGO()
    {
        if(gameObject.tag == "Bullet")
            BulletSpawner.instance.Despawn(transform);
        else
            ZombieSpawner.instance.Despawn(transform);
    }
    private void Update()
    {
        if(gameObject.tag == "Enemy")
        {
            if(GetComponent<ZombieBehaviour>().canDespawn)
            {
                DespawnGO();
                ZombieSpawner.instance.currentZombieQuantity--;
                GameManager.instance.killed++;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.tag == "Bullet" && collision.gameObject.tag == "Enemy")
        {
            DespawnGO();
        }
    }
}
