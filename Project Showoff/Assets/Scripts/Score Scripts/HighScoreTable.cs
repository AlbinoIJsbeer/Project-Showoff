using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField]
    private Transform entryContainer;
    [SerializeField]
    private Transform entryTemplate;
    private List<HighScoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryTemplate.gameObject.SetActive(false);

        highscoreEntryList = new List<HighScoreEntry>()
        {
            new HighScoreEntry{ score = 0, name = "Test" } //after done re comment this
        };

        
        //string jsonString = PlayerPrefs.GetString("highscoretable");
        //HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        //if (highscores == null)
        //{
        //    // There's no stored table, initialize
        //    Debug.Log("Initializing table with default values...");                       //after done uncomment this
        //    AddHighScoreEntry(0, "Test");
            
        //    // Reload
        //    jsonString = PlayerPrefs.GetString("highscoreTable");
        //    highscores = JsonUtility.FromJson<HighScores>(jsonString); //after done uncomment this
        //}

        for (int i = 0; i < highscoreEntryList.Count; i++)
        {
            for(int j = 0; j < highscoreEntryList.Count; j++)
            {
                if(highscoreEntryList[j].score < highscoreEntryList[i].score)
                {
                    HighScoreEntry tmp = highscoreEntryList[i]; // after one add highscores.
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();

        foreach (HighScoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

        HighScores highscores = new HighScores { highscoreEntryList = highscoreEntryList };
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoretable", json);
        PlayerPrefs.Save();
        
    }

    private void CreateHighscoreEntryTransform(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 100f;
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

        entryTransform.Find("Position").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("Points").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;

        transformList.Add(entryTransform);
    }

    public static void AddHighScoreEntry(int _score, string _name)
    {
        HighScoreEntry highscoreEntry = new HighScoreEntry { score = _score, name = _name };

        string jsonString = PlayerPrefs.GetString("highscoretable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        if (highscores == null)
        {
            // There's no stored table, initialize
            highscores = new HighScores()
            {
                highscoreEntryList = new List<HighScoreEntry>()
            };
        }

        highscores.highscoreEntryList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoretable", json);
        PlayerPrefs.Save();
    }

}
