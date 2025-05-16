using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject player;


    public void UpgradePlayer(string upgradeType){
        switch(upgradeType){
            case "Dash":
                // Increase dash speed
                break;
            case "Attack":
                // Increase attack damage
                break;
            case "Health":
                // Increase health
                break;
            case "Reflect":
                // Increase reflect damage
                break;
            default:
                Debug.Log("Invalid upgrade type");
                break;
        }
    }
}
