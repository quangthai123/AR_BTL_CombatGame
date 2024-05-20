using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public bool isDeath = false;
    public float hp;
    public float damage;
    private Animator anim;
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (isDeath)
            anim.SetTrigger("IsDead");
        FaceToNearestEnemy();
    }

    private void FaceToNearestEnemy()
    {
        Vector3 nearestEnemy = new Vector3();
        Transform zombiesHolder = ZombieSpawner.instance.transform.Find("Holder");
        if (zombiesHolder.childCount > 0)
        {
            float minDistance = Mathf.Infinity;
            foreach(Transform zombie in zombiesHolder)
            {
                if(Vector3.Distance(zombie.position, transform.position) < minDistance && !zombie.GetComponent<ZombieBehaviour>().isDeath)
                {
                    minDistance = Vector3.Distance(zombie.position, transform.position);
                    nearestEnemy = zombie.position;
                }
            }
        }
        float angle = Mathf.Atan2(nearestEnemy.x - transform.position.x, nearestEnemy.z - transform.position.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void GetDamage(float _damage)
    {
        if (isDeath) return;
        anim.SetTrigger("IsHit");
        hp -= _damage;
        if (hp <= 0)
            isDeath = true;
    }
}
