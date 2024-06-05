using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diemCaoText;
    [SerializeField] private GameObject chonDoKhoUI;

    void Start()
    {
        diemCaoText.text = "Điểm Cao: " + PlayerPrefs.GetInt("diemCao");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SetActiveChonDK()
    {
        chonDoKhoUI.SetActive(true);
    }
    public void ChonDe()
    {
        PlayerPrefs.SetInt("choseDiff", 0);
        SceneManager.LoadScene(1);
    }
    public void ChonVua()
    {
        PlayerPrefs.SetInt("choseDiff", 1);
        SceneManager.LoadScene(1);
    }
    public void ChonKho()
    {
        PlayerPrefs.SetInt("choseDiff", 2);
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        chonDoKhoUI.SetActive(false);
    }
}
