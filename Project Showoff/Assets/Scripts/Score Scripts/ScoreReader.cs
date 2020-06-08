using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using TMPro;
using UnityEngine;

internal class ScoreReader
{

    private XmlDocument _xml = new XmlDocument();
    private string[][] Scores = new string[1][];
    private int i = 0;
    private List<string> _names = new List<string>();
    private List<string> _scores = new List<string>();

    public ScoreReader()
    {
        _xml.Load(ScoreManager._fileName);
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
            _scores.Add(Score.Value.ToString());
        }

        for (int i = 0; i < 4; i++)
        {
            TodayNames[i].GetComponent<TextMeshProUGUI>().text = _names[i];
            AllTimeNames[i].GetComponent<TextMeshProUGUI>().text = _names[i];
            
            TodayScores[i].GetComponent<TextMeshProUGUI>().text = _scores[i];
            AllTimeScores[i].GetComponent<TextMeshProUGUI>().text = _scores[i];
        }

    }

}
