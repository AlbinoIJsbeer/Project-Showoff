using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTips : MonoBehaviour
{
    [SerializeField] private List<GameObject> tips;

    private int index;

    void Start()
    {
        index = 0;
    }

    public void Update()
    {
        for (int i = 0; i < tips.Count; i++)
            if (i == index)
                tips[i].SetActive(true);
            else
                tips[i].SetActive(false);
    }

    public void Next()
    {
        if (index < tips.Count - 1)
            index++;
        else
            transform.gameObject.SetActive(false);
    }
}
