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
    public float XOffset = 0, YOffset = 2;
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
            else if (Level == 5)
            {
                int rand = Random.Range(0, GameManager.Boss1Texts.Length);
                TalkText.text = GameManager.Boss1Texts[rand];
            }
            else if (Level == 6)
            {
                int rand = Random.Range(0, GameManager.Boss2Texts.Length);
                TalkText.text = GameManager.Boss2Texts[rand];
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
        transform.position = new Vector3(Follow.transform.position.x + XOffset, Follow.transform.position.y + YOffset, Follow.transform.position.z);
    }
}
