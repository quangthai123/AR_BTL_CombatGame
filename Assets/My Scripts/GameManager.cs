using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject xROrigin;
    [SerializeField] private GameObject playerHpBar;
    [SerializeField] private GameObject meshQuantityGO;
    [SerializeField] private TextMeshProUGUI meshQuantityText;
    [SerializeField] private GameObject killTextGO;
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private GameObject zombiesUIGO;
    [SerializeField] private TextMeshProUGUI zombiesUIText;
    [SerializeField] private GameObject healthUIGO;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject healthButton;

    [Space]
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject endCanvas;
    public int trackables;
    public int killed = 0;

    [Header("Spawn Zombie Infor")]
    [SerializeField] private int spawnByChangedMeshQuantity;
    [SerializeField] private int currentMeshQuantity;
    [SerializeField] private int changedMeshQuantity;

    [Header("Spawn Health Infor")]
    [SerializeField] private Transform health;
    [SerializeField] private Vector2 xRange;
    [SerializeField] private Vector2 zRange;
    [SerializeField] private float heightY;
    public int choseDiff = 0;
    public int healthCanSpawn = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
            instance = this;
        choseDiff = PlayerPrefs.GetInt("choseDiff");
        Time.timeScale = 1f;
    }
    private void Start()
    {
        if (choseDiff == 0)
        {
            spawnByChangedMeshQuantity = 10;
        }
        else if (choseDiff == 1)
        {
            spawnByChangedMeshQuantity = 7;
        }
        else
            spawnByChangedMeshQuantity = 3;
    }
    void Update()
    {
        meshQuantityText.text = "Số lưới thay đổi: " + changedMeshQuantity;
        killText.text = "Đã tiêu diệt: " + killed;
        zombiesUIText.text = "Zombies: " + ZombieSpawner.instance.currentZombieQuantity;
        trackables = xROrigin.transform.Find("Trackables").childCount;
        healthText.text = ": " + healthCanSpawn;
        if (Player.instance != null)
        {
            playerHpBar.SetActive(true);
            killTextGO.SetActive(true);
            meshQuantityGO.SetActive(true);
            zombiesUIGO.SetActive(true);
            healthUIGO.SetActive(true);
            healthButton.SetActive(true);
            if (Player.instance.isDeath)
            {
                endCanvas.SetActive(true);
                Time.timeScale = 0f;
                AudioManager.instance.PlayeSFX(3);
                if (killed > PlayerPrefs.GetInt("diemCao"))
                    PlayerPrefs.SetInt("diemCao", killed);
            }
        }
        else
        {
            playerHpBar.SetActive(false);
            killTextGO.SetActive(false);
            meshQuantityGO.SetActive(false);
            zombiesUIGO.SetActive(false);
            healthUIGO.SetActive(false);
            healthButton.SetActive(false);
        }
        if (currentMeshQuantity != trackables && changedMeshQuantity < spawnByChangedMeshQuantity)
        {
            currentMeshQuantity = trackables;
            changedMeshQuantity++;
        }
        if (changedMeshQuantity >= spawnByChangedMeshQuantity)
        {
            currentMeshQuantity = trackables;
            changedMeshQuantity = 0;
            ZombieSpawner.instance.SpawnZombie();
        }
    }
    public void IncreaseHealthByKilledZombies()
    {
        if (choseDiff == 0)
        {
            if (killed % 10 == 0)
                healthCanSpawn++;
        }
        else if (choseDiff == 1)
        {
            if (killed % 20 == 0)
                healthCanSpawn++;
        }
        else
        {
            if (killed % 40 == 0)
                healthCanSpawn++;
        }
    }
    public void SpawnHealth()
    {
        if (healthCanSpawn <= 0) return;
        Vector3 rdPos = new Vector3(Player.instance.transform.position.x + Random.Range(xRange.x, xRange.y), Player.instance.transform.position.y + heightY, Player.instance.transform.position.z + Random.Range(zRange.x, zRange.y));
        Instantiate(health, rdPos, Quaternion.identity);
        healthCanSpawn--;
    }
    public void PauseGame()
    {
        //if(Player.instance.isDeath && Player.instance!=null) return; 
        Time.timeScale = 0f;
        pauseCanvas.SetActive(true);
    }
    public void ContinueGame()
    {
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        Debug.Log("Scene 0");
        SceneManager.LoadScene(0);
    }
}
