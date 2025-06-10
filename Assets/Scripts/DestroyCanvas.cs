using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("ShowTutorial", 1) == 0)
        {
            Destroy(gameObject); // Destroy this canvas immediately if ShowTutorial is set to false
        }
        else
        {
            Destroy(gameObject, 25f); // Destroy this canvas after 25f seconds
        }
    }
}
