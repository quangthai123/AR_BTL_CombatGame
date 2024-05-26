using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [Header("Attack Logic")]
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Transform attackPointPos;
    [SerializeField] private float sphereRadius;
    [SerializeField] private float canAttackDistance;
    private Rigidbody rb;
    private Animator anim;
    private bool canAttack = false;
    public bool isDeath = false;
    [Header("Stats")]
    [SerializeField] private float damage;
    public float maxHp;
    public float hp;
    public bool canDespawn = false;
    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        ResetStatesWhenSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDeath)
        {
            anim.SetBool("Walk", !canAttack);
            anim.SetBool("Attack", canAttack);
        }
        if (Player.instance == null)
            return;
        Vector3 playerPos = Player.instance.transform.position;
        transform.position = new Vector3(transform.position.x, playerPos.y, transform.position.z);
        if(Vector3.Distance(playerPos, transform.position) < canAttackDistance && !isDeath)
        {
            canAttack = true;
            rb.velocity = Vector3.zero;
        }
        else
        {
            canAttack = false;
        }
        if (!canAttack && !isDeath)
        {
            rb.velocity = (playerPos - transform.position).normalized * moveSpeed;
        }
        if(isDeath)
        {
            anim.SetBool("Dead", true);
            anim.SetBool("Walk", false);
            anim.SetBool("Attack", false);
            canAttack = false;
            rb.velocity = Vector3.zero;
            AudioManager.instance.PlayeSFX(2);
        } else
        {
            float angle = Mathf.Atan2(playerPos.x - transform.position.x, playerPos.z -transform.position.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
    public void DoDamagePlayer()
    {
        Player.instance.GetDamage(damage);
    }
    public void GetDamage(float _damage)
    {
        if (isDeath)
            return;
        hp -= _damage;
        if (hp <= 0f)
            isDeath = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
            GetDamage(Player.instance.damage);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPointPos.position, sphereRadius);
    }
    public void ResetStatesWhenSpawn()
    {
        canDespawn = false;
        isDeath = false;
        canAttack = false;
        anim.SetBool("Dead", false);
        anim.SetBool("Walk", !canAttack);
        anim.SetBool("Attack", canAttack);
        hp = 50;
    }
}
