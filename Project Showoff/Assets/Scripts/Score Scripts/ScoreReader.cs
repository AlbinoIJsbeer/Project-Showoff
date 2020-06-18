using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.XPath;
using TMPro;
using UnityEngine;

public class ScoreReader
{

    private XmlDocument _xml = new XmlDocument();
    private string[][] Scores = new string[1][];
    private int i = 0;
    private List<string> _names = new List<string>();
    private List<int> _scores = new List<int>();

    private Dictionary<string, int> PlayerScore = new Dictionary<string, int>(4);

    public ScoreReader()
    {
        if (File.Exists(ScoreManager._fileName))
        {
            _xml.Load(ScoreManager._fileName);
        }else throw new System.Exception("Highscore File not found");
        
        XmlElement root = _xml.DocumentElement;
        XmlNodeList elemList = root.GetElementsByTagName("Score");
    }

    public void loadHighScore(List<GameObject> TodayNames, List<GameObject> TodayScores, List<GameObject> AllTimeNames, List<GameObject> AllTimeScores)
    {
        if (_xml.ChildNodes.Count == 0)
        {
            Debug.Log("The Document is empty");
        }
        else if (_xml.ChildNodes.Count == 1)
        {
            Debug.Log("The Document only has a decleration node");
        }

        XPathNavigator navigator = _xml.CreateNavigator();
        foreach (XPathNavigator PlayerName in navigator.Select("//PlayerName"))
        {
            Debug.Log(PlayerName.Value);
            _names.Add(PlayerName.Value);
            i += 1;

        }

        foreach (XPathNavigator Score in navigator.Select("//Value"))
        {
            Debug.Log(Score.Value);
            _scores.Add(Score.ValueAsInt);
        }

        for (int i = 0; i < _names.Count; i++)
        {
            Debug.Log(_names.Count);
            PlayerScore.Add(_names[i], _scores[i]);
        }

        IOrderedEnumerable<KeyValuePair<string, int>> sortedScores = from pair in PlayerScore orderby pair.Value ascending select pair;

        foreach (KeyValuePair<string, int> pair in sortedScores)
        {
            TodayNames[0].GetComponent<TextMeshProUGUI>().text = pair.Key;
            TodayScores[0].GetComponent<TextMeshProUGUI>().text = pair.Value.ToString();

            TodayNames[1].GetComponent<TextMeshProUGUI>().text = pair.Key;
            TodayScores[1].GetComponent<TextMeshProUGUI>().text = pair.Value.ToString();

            TodayNames[2].GetComponent<TextMeshProUGUI>().text = pair.Key;
            TodayScores[2].GetComponent<TextMeshProUGUI>().text = pair.Value.ToString();

            TodayNames[3].GetComponent<TextMeshProUGUI>().text = pair.Key;
            TodayScores[3].GetComponent<TextMeshProUGUI>().text = pair.Value.ToString();

        }

    }

}
