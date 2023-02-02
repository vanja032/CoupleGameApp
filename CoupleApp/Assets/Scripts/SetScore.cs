using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetScore : MonoBehaviour
{
    [Header("Messages")]
    [SerializeField]
    string[] messages = new string[365];
    [Header("Emoji images")]
    [SerializeField]
    Sprite[] emojis = new Sprite[54];
    [Header("Emoji count")]
    [SerializeField]
    Image emoji_count_image;
    [SerializeField]
    TextMeshProUGUI emoji_count;
    [Header("Fire count")]
    [SerializeField]
    TextMeshProUGUI fire_count;
    [Header("Message")]
    [SerializeField]
    Image emoji_image;
    [SerializeField]
    TextMeshProUGUI message;

    string delimeter = ";";
    int allowed_secs = 60;//60 seconds
    int time_now = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
    int random_num = 0;

    public int emojs = 0;
    public int fires = 0;
    public int last_time = 0;
    public int last_emoj = 0;
    public string emoj_list = "";
    private void Awake()
    {
        string m = "";
        foreach (string el in messages)
            m += ";" + el;
        Debug.Log(m);
        random_num = UnityEngine.Random.Range(0, 54);
        if (!PlayerPrefs.HasKey("emojis")) PlayerPrefs.SetInt("emojis", 0);
        if (!PlayerPrefs.HasKey("fires")) PlayerPrefs.SetInt("fires", 0);
        if (!PlayerPrefs.HasKey("last_time")) PlayerPrefs.SetInt("last_time", time_now - allowed_secs);
        if (!PlayerPrefs.HasKey("last_emoji")) PlayerPrefs.SetInt("last_emoji", random_num);
        if (!PlayerPrefs.HasKey("emojis_list")) PlayerPrefs.SetString("emojis_list", "");
    }
    void Start()
    {
        if(PlayerPrefs.GetInt("last_time") <= time_now - allowed_secs && PlayerPrefs.GetInt("last_time") >= time_now - 2 * allowed_secs)
        {
            string[] taken_emojis = PlayerPrefs.GetString("emojis_list").Split(delimeter);
            bool exist = false;
            foreach (string emoji_num in taken_emojis)
            {
                if (emoji_num.Equals(random_num.ToString()))
                {
                    exist = true;
                    break;
                }
            }
            if (!exist)
            {
                PlayerPrefs.SetInt("emojis", PlayerPrefs.GetInt("emojis") + 1);
            }

            if(PlayerPrefs.GetInt("fires") < 365)
                PlayerPrefs.SetInt("fires", PlayerPrefs.GetInt("fires") + 1);
            PlayerPrefs.SetInt("last_time", time_now);
            PlayerPrefs.SetInt("last_emoji", random_num);
            PlayerPrefs.SetString("emojis_list", PlayerPrefs.GetString("emojis_list") + delimeter + random_num.ToString());
        }
        else if(PlayerPrefs.GetInt("last_time") < time_now - 2 * allowed_secs)
        {
            PlayerPrefs.SetInt("fires", 1);
            PlayerPrefs.SetInt("last_time", time_now);
            PlayerPrefs.SetInt("last_emoji", random_num);
            PlayerPrefs.SetString("emojis_list", "");

            PlayerPrefs.SetInt("emojis", 1);
        }

        emoji_count_image.sprite = emojis[PlayerPrefs.GetInt("fires") / 7];
        emoji_count.text = PlayerPrefs.GetInt("emojis") + "/54";

        fire_count.text = PlayerPrefs.GetInt("fires") + "/365";

        emoji_image.sprite = emojis[PlayerPrefs.GetInt("last_emoji")];
        message.text = messages[PlayerPrefs.GetInt("fires") - 1];

        emojs = PlayerPrefs.GetInt("emojis");
        fires = PlayerPrefs.GetInt("fires");
        last_time = PlayerPrefs.GetInt("last_time");
        last_emoj = PlayerPrefs.GetInt("last_emoji");
        emoj_list = PlayerPrefs.GetString("emojis_list");
    }
}
