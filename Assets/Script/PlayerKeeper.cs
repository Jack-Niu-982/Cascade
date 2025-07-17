using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKeeper : MonoBehaviour
{
    private static PlayerKeeper instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerController.PlayerHP = 50;
        PlayerController.PlayerMP = 100;

        transform.position = new Vector3(-5.08f, 0, 0);
        Debug.Log("Reset");
    }
}
