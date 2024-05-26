using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float speed;

    void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
        if (Player.instance != null)
        {
            if (transform.position.y > Player.instance.transform.position.y)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, Player.instance.transform.position.y, transform.position.z);
            }
        }
    }
}
