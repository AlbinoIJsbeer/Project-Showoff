using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndgameScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreN;
    [SerializeField]
    private TextMeshProUGUI _nameN;

    void Start()
    {
        _nameN.text = Manager.Instance.Name;
        _scoreN.text = Manager.Instance.Score.ToString();
    }

}
