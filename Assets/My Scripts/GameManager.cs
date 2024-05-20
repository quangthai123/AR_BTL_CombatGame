using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject xROrigin;
    [SerializeField] private GameObject playerHpBar;
    public int trackables;
    public int killed = 0;
    [SerializeField] private GameObject killTextGO;
    [SerializeField] private TextMeshProUGUI killText;

    private void Awake()
    {
        if(instance != null) 
        {
            Destroy(gameObject);
        } else
            instance = this;
    }
    void Update()
    {
        killText.text = "Killed: " + killed;
        trackables = xROrigin.transform.Find("Trackables").childCount;      
        if(Player.instance != null )
        {
            playerHpBar.SetActive(true);
            killTextGO.SetActive(true);
        }
        else
        {
            playerHpBar.SetActive(false);
            killTextGO.SetActive(false);
        }
    }
}
