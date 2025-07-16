using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CDController1 : MonoBehaviour
{
    public Image Image1;
    public Image Image2;
    void Update()
    {
        float degree1 = Mathf.Clamp01(PlayerController.SkillPass1 / PlayerController.CD1);
        float degree2 = Mathf.Clamp01(PlayerController.SkillPass2 / PlayerController.CD2);
        Image1.fillAmount = 1f - degree1;
        Image2.fillAmount = 1f - degree2;
        float timeLeft1 = Mathf.Max(0f, PlayerController.CD1 - PlayerController.SkillPass1);
        float timeLeft2 = Mathf.Max(0f, PlayerController.CD2 - PlayerController.SkillPass2);
        //if(timeLeft1 > 0f)
        //{
        //    Debug.Log("asd");
        //    Text1.text = Mathf.CeilToInt(timeLeft1).ToString();
        //}
        //else
        //{
        //    Text1.text = "";
        //}
        //if(timeLeft2 > 0f)
        //{
        //    Text2.text = Mathf.CeilToInt(timeLeft2).ToString();
        //}
        //else
        //{
        //    Text2.text = "";
        //}
    }
}
