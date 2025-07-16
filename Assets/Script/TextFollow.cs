using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFollow : MonoBehaviour
{
    public GameObject Follow;
    public TMP_Text TalkText;
    public float ActionTime = 0f;
    public float TalkInterval = 5f;
    public float TalkSpeed = 3f;
    public int Level = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Follow == null)Destroy(gameObject);
        Debug.Log(ActionTime);  
        if(ActionTime >= TalkInterval)
        {
            TalkText.enabled = true;
            if (Level == 1)
            {
                int rand = Random.Range(0, GameManager.LevelATexts.Length);
                TalkText.text = GameManager.LevelATexts[rand];
            }
            else if (Level == 2)
            {
                int rand = Random.Range(0, GameManager.LevelBTexts.Length);
                TalkText.text = GameManager.LevelBTexts[rand];
            }
            else if (Level == 3)
            {
                int rand = Random.Range(0, GameManager.LevelCTexts.Length);
                TalkText.text = GameManager.LevelCTexts[rand];
            }
            else if (Level == 4)
            {
                int rand = Random.Range(0, GameManager.LevelDTexts.Length);
                TalkText.text = GameManager.LevelDTexts[rand];
            }
            ActionTime = 0;
        }else if(ActionTime >= TalkSpeed)
        {
            TalkText.enabled = false;
        }
        if(ActionTime < TalkInterval)
        {
            ActionTime += Time.deltaTime;
        }
        transform.position = new Vector3(Follow.transform.position.x, Follow.transform.position.y + 2, Follow.transform.position.z);
    }
}
