using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum EmojiName
{
    happyFace=1,
    loveEyed=2,
    glassEyed=3,
}
public class UserInput : MonoBehaviour
{
    public static UserInput Instance;
    public Text titleText;
    public GameObject cube;
    public int letterCount;
    public TMP_InputField inputField;
    public List<GameObject> emojiPrefab;
    GameObject emoji;


    void Start()
    {
        if (!Instance) Instance = this;
    }

  

    public void OnSendButtonPressed()
    {
        if (emoji) Destroy(emoji);
        int index = 0;
        titleText.text = inputField.text;
        inputField.text = "";
        if (titleText.text.Contains("<sprite=5>") || titleText.text.Contains("<sprite=2>") || titleText.text.Contains("<sprite=3>"))
        {
            EmojiName emojiName = EmojiName.happyFace;
            if (titleText.text.Contains("<sprite=5>")) { emoji = emojiPrefab[0];  emojiName = EmojiName.happyFace; }
            if (titleText.text.Contains("<sprite=2>")) { emoji = emojiPrefab[1]; emojiName = EmojiName.loveEyed; }
            if (titleText.text.Contains("<sprite=3>")) { emoji = emojiPrefab[2]; emojiName = EmojiName.glassEyed; }


            for (int i = 0; i < titleText.text.Length; i++)
            {
                if (titleText.text[i] == '<')
                {
                    index = i;
                    break;
                }
            }
            int spcctr = 0;
            string str1;
            for (int i = 0; i < index+1; i++)
            {
                str1 = titleText.text.Substring(i, 1);
                if (str1 == " ")
                    spcctr++;
            }
            TextTest.Instance.charIndex = index+1- spcctr;
            TextTest.Instance.PrintPos();


            if (emojiName == EmojiName.happyFace) {  titleText.text = titleText.text.Replace("<sprite=5>", "        ");}
            if (emojiName == EmojiName.loveEyed) { titleText.text = titleText.text.Replace("<sprite=2>", "        ");}
            if (emojiName == EmojiName.glassEyed) { titleText.text = titleText.text.Replace("<sprite=3>", "        ");}

        }
    }

    
    public void OnEmojiClicked(string emojiText)
    {
        
        Debug.Log("emojitext : " + emojiText);
        Debug.Log(" inputText.text : " + inputField.text);
        inputField.text += emojiText;
    }
    public void LoadPrefab(Vector3 worldPos)
    {

         emoji = Instantiate(emoji, new Vector3(worldPos.x+3, worldPos.y, worldPos.z), Quaternion.identity);
         emoji.transform.localScale = new Vector3(6,6,6);
         emoji.transform.localRotation = Quaternion.Euler(new Vector3(0,90,0));

    }
}
