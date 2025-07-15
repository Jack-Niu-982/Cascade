using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string menuScene = "MainMenu";
    public string battleScene = "BattleScene";
    public string CurrentScene = "MainMenu";
    public static bool IfDie = false;

    public GameObject PausePanel;
    public GameObject DiePanel;

    private bool ifPause = false;
    void Start()
    {
        PausePanel.SetActive(false);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        if (IfDie)
        {
            Time.timeScale = 0f;
            DiePanel.SetActive(true);
        }
    }
    public void LoadScene(string name)
    {
        CurrentScene = name;
        SceneManager.LoadScene(name);
    }
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(CurrentScene);
    }
    public void Retry()
    {
        IfDie = false;
        PlayerController.PlayerHP = 10;
        PlayerController.PlayerMP = 10;
        Time.timeScale = 1f;
        DiePanel.SetActive(false);
    }
    public void PauseGame()
    {
        if (!ifPause)
        {
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
            ifPause = true;
        }
        else
        {
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
            ifPause = false;
        }
    }
}
