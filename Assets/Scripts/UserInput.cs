using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class UserInput : MonoBehaviour
{
    public static UserInput Instance;
    public Text titleText;
    public TMP_InputField inputField;
    public List<EmojiInfo> emojiInfos;
    GameObject emoji;
    [HideInInspector]public List<GameObject> emojisOnScene;

    void Start()
    {
        if (!Instance) Instance = this;
    }


    public void OnSendButtonPressed()
    {
        titleText.text = inputField.text;
        inputField.text = "";
        string emojiCOde = "";
        int index = 0;


        if (emojisOnScene.Count > 0)
        { 
            for(int k=0;k<emojisOnScene.Count;k++)
            {
                Destroy(emojisOnScene[k]);
            }
            emojisOnScene.Clear();
        }

        EmojiName emojiName = EmojiName.happyFace;
        if (titleText.text.Contains("<sprite="))
        {
            int totalEmojiInText = Regex.Matches(titleText.text, "<sprite=").Count;
            for (int j=0;j< totalEmojiInText; j++)
            {
                for (int i = 0; i < titleText.text.Length; i++)
                {
                    if (titleText.text[i] == '<' && titleText.text[i + 1] == 's' && titleText.text[i + 2] == 'p')
                    {
                        index = i;
                        break;
                    }
                }

                emojiCOde = titleText.text.Substring(index, 10);
                for(int l = 0; l < emojiInfos.Count; l++)
                {
                    if (emojiInfos[l].emojiCode == emojiCOde)
                    {
                        emojiName = emojiInfos[l].emojiName;
                        emoji = emojiInfos[l].emojiPrefab;
                    }
                }

                LetterPositionTracker.Instance.charIndex = index + 1 - SpaceCounterInString(index);
                LetterPositionTracker.Instance.FindPos();

                titleText.text = titleText.text.Remove(titleText.text.IndexOf(emojiCOde),emojiCOde.Length).Insert(titleText.text.IndexOf(emojiCOde),"        ");
            }
            
        }
    }

    
    public void OnEmojiClicked(string emojiText)
    {
        inputField.text += emojiText;
    }
    public void LoadPrefab(Vector3 worldPos)
    {

         emoji = Instantiate(emoji, new Vector3(worldPos.x+3, worldPos.y, worldPos.z), Quaternion.identity);
         emoji.transform.localScale = new Vector3(6,6,6);
         emoji.transform.localRotation = Quaternion.Euler(new Vector3(0,90,0));

         emojisOnScene.Add(emoji);

    }

    int SpaceCounterInString(int index)
    {
        int spaceCounter = 0;
        string strTemp;

        for (int i = 0; i <=index; i++)
        {
            strTemp = titleText.text.Substring(i, 1);
            if (strTemp == " ")
                spaceCounter++;
        }

        return spaceCounter;
    }
}
