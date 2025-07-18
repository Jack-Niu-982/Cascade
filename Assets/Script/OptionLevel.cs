using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionLevel : MonoBehaviour
{
    public GameObject Panel1;
    public GameObject Panel2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Option1()
    {
        Panel1.SetActive(true);
    }
    public void Option2()
    {
        Panel2.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
