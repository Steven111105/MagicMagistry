using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTutorialToggle : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("ShowTutorial", 1) == 0)
        {
            GetComponent<Toggle>().isOn = false;
        }
        else
        {
            GetComponent<Toggle>().isOn = true;
        }
    }

    public void ToggleTutorial(bool toggle)
    {
        PlayerPrefs.SetInt("ShowTutorial", toggle ? 1 : 0);
    }
    
    [ContextMenu("Reset Tutorial Preference")]
    public void ResetTutorialPreference()
    {
        PlayerPrefs.DeleteAll();
        GetComponent<Toggle>().isOn = true;
    }
}
