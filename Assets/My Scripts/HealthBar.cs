using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider easeHpBar;
    [SerializeField] private float lerpSpeed = .5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance == null)
            return;
        if(gameObject.tag != "Enemy") 
            hpBar.value = Player.instance.hp;
        else
        {
            hpBar.maxValue = GetComponentInParent<ZombieBehaviour>().maxHp;
            easeHpBar.maxValue = GetComponentInParent<ZombieBehaviour>().maxHp; 
            hpBar.value = GetComponentInParent<ZombieBehaviour>().hp;
        }
        if(hpBar.value != easeHpBar.value)
        {
            easeHpBar.value = Mathf.Lerp(easeHpBar.value, hpBar.value, lerpSpeed);
        }
    }
}
