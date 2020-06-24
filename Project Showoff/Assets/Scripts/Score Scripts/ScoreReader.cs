using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

class ScoreReader
{

    private XmlDocument _xml = new XmlDocument();
    private string[][] Scores = new string[1][];
    int i = 0;

    public ScoreReader()
        {
            _xml.Load("scores.xml");
        }

        public void loadHighScore()
        {
            XPathNavigator navigator = _xml.CreateNavigator();
            foreach (XPathNavigator PlayerName in navigator.Select("//PlayerName"))
            {
                Console.WriteLine(PlayerName.Value);
                Scores[0][i] = PlayerName.Value;
                i += 1;
            }

            foreach (XPathNavigator Score in navigator.Select("//Value"))
            {
                Console.WriteLine(Score.Value);
            }



    }

    }   
