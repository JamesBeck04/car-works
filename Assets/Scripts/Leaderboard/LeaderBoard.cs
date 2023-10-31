using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LeaderBoard : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;
    float time = 0f;

    private void Awake()
    {
        entryContainer = transform.Find("Highscorecontainer");
        entryTemplate = entryContainer.Find("HighscoreTemplate");

        entryTemplate.gameObject.SetActive(false);

     
        for (int i = 0; i < 5; i++)
        {
           
        }
    }

    private void CreateLeaderBoardEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 50f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("Pos").GetComponent<TextMeshProUGUI>().text = rankString;

        float time = highscoreEntry.time;
        int minutes = (int)(time / 60) % 60;
        int seconds = (int)(time % 60);


        entryTransform.Find("Time").GetComponent<TextMeshProUGUI>().text = minutes.ToString() + "m " + seconds.ToString() + "s ";

        string name = highscoreEntry.name;
        entryTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;

        transformList.Add(entryTransform);
    }


    private class HighscoreEntry {
        public float time;
        public string name;
    }

}
