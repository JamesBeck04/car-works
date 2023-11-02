using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LeaderBoard : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;


    private void Awake()
    {
        entryContainer = transform.Find("Highscorecontainer");
        entryTemplate = entryContainer.Find("HighscoreTemplate");

        entryTemplate.gameObject.SetActive(false);

        /*highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry{ time = 300, name = "Bob"},
            new HighscoreEntry{ time = 567, name = "Mike"},
            new HighscoreEntry{ time = 900, name = "Jeff"},
            new HighscoreEntry{ time = 234, name = "Garry"},
            new HighscoreEntry{ time = 562, name = "Rob"},
        };*/


        //AddHighscoreEntry(400, "ABC");


        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
    


            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
                {
                    if (highscores.highscoreEntryList[j].time < highscores.highscoreEntryList[i].time)
                    {
                        HighscoreEntry tmp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = tmp;
                    }
                }
            }



        Debug.Log(highscores.highscoreEntryList.Count);


        highscoreEntryTransformList = new List<Transform>();
       
            foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
            {

            
                CreateLeaderBoardEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);


            }
        
     

        /*Highscores highscores = new Highscores { highscoreEntryList = highscoreEntryList };
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
      */
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


    private void AddHighscoreEntry(float time, string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { time = time, name = name };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoreEntryList.Add(highscoreEntry);
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }


    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry {
        public float time;
        public string name;
    }

}
