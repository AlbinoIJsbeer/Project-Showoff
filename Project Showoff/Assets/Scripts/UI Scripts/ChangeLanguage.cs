using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    public void ChangeToEnglish()
    {
        Manager.Instance._language = Language.ENGLISH;
    }

    public void ChangeToDutch()
    {
        Manager.Instance._language = Language.DUTCH;
    }

    public void ChangeToGerman()
    {
        Manager.Instance._language = Language.GERMAN;
    }
}
