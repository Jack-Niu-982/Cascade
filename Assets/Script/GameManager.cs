using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string menuScene = "MainMenu";
    public string BattleScene = "Actual level4";
    public string CurrentScene = "MainMenu";
    public static bool IfDie = false;
    public static string[] LevelATexts = { "Kill him!", "That’s the bloody Prince!" };
    public static string[] LevelBTexts = { "You dictator!", "They kept us here!" };
    public static string[] LevelCTexts = { "Take his crown!", "No mercy!" };
    public static string[] LevelDTexts = { "You’re no better than any of us!", "You don’t deserve to rule!" };
    public static string[] Boss1Texts = { "The crown has betrayed the people!","The Tarnens will take back what’s ours!"};
    public static string[] Boss2Texts = { "You know nothing of our suffering!", "Why do you get to see the sun?" };

    public GameObject PausePanel;
    public GameObject DiePanel;
    public Slider HPSlider;
    public Slider MPSlider;
    public GameObject PlayerUI;

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
        if(CurrentScene == "MainMenu")
        {
            PlayerUI.SetActive(false);
        }
        else
        {
            PlayerUI.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        if (IfDie)
        {
            Time.timeScale = 0f;
            DiePanel.SetActive(true);
        }
        HPSlider.value = PlayerController.PlayerHP / 50;
        MPSlider.value = PlayerController.PlayerMP / 100;
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
        PlayerController.PlayerHP = 50;
        PlayerController.PlayerMP = 100;
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
