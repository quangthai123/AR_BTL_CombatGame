using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private PlayerInputControls inputControls;
    [SerializeField] private Transform spawnBulletPos;
    [SerializeField] private float shootingDelay;
    [SerializeField] private float shootingTimer;

    void Start()
    {
        inputControls = GetComponent<PlayerInputControls>();
        inputControls.OnShootActionPerformed += StartShooting;
    }

    private void StartShooting()
    {
        Debug.Log("Shoot!!!!!!!!!!!!!!!");
        if(shootingTimer <= 0)
        {
            Quaternion rot = Quaternion.Euler(90f, transform.localEulerAngles.y, 0f);
            BulletSpawner.instance.Spawn(spawnBulletPos.position, rot);
            shootingTimer = shootingDelay;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(shootingTimer > 0)
            shootingTimer -= Time.deltaTime;
    }
}
