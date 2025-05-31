using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    Transform playerTransform;
    int fireCount = 0;
    int iceCount = 0;
    int wallCount = 0;
    int moveCount = 0;
    string baseSpell;

    [SerializeField] private GameObject fireSpellPrefab;
    [SerializeField] private GameObject iceSpellPrefab;
    [SerializeField] private GameObject wallSpellPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CastSpell(List<string> spellComponents)
    {
        FormatComponents(spellComponents);

        if (wallCount > 0)
        {
            //Create shield pillar, then change the element accordingly
            if (moveCount != 0)
            {
                Debug.Log("Casting Shield Pillar");
                // Add logic to create a shield pillar without move effect
                //create 3 shield pillars in a row in front of player
                GameObject shieldPillar = Instantiate(wallSpellPrefab, playerTransform.position + playerTransform.up, playerTransform.rotation);
                if(wallCount == 2)
                {
                    GameObject shieldPillar2 = Instantiate(wallSpellPrefab, playerTransform.position + playerTransform.up * 2, playerTransform.rotation);
                    GameObject shieldPillar3 = Instantiate(wallSpellPrefab, playerTransform.position + playerTransform.up * 3, playerTransform.rotation);
                }
            }
        }
        fireCount = 0;
        iceCount = 0;
        wallCount = 0;
        moveCount = 0;
    }

    void FormatComponents(List<string> spellComponents)
    {
        //Sort the components in the order of fire, ice, shield, move
        string[] order = { "fire", "ice", "wall", "move" };
        spellComponents.Sort((a, b) =>
            System.Array.IndexOf(order, a).CompareTo(System.Array.IndexOf(order, b))
        );

        foreach (string component in spellComponents)
        {
            switch (component)
            {
                case "Fire":
                    fireCount++;
                    Debug.Log("Casting Fire Spell");
                    // Add fire spell logic here
                    break;
                case "Ice":
                    iceCount++;
                    Debug.Log("Casting Ice Spell");
                    // Add ice spell logic here
                    break;
                case "Wall":
                    wallCount++;
                    Debug.Log("Casting Shield Spell");
                    // Add shield spell logic here
                    break;
                case "Move":
                    moveCount++;
                    Debug.Log("Casting Move Spell");
                    // Add move spell logic here
                    break;
                default:
                    Debug.LogWarning("Unknown spell component: " + component);
                    break;
            }
        }
        // ProcessBaseSpell();
    }

    // void ProcessBaseSpell()
    // {
    //     baseSpell = "";

    //     if (fireCount > 0) baseSpell = "Fire";
    //     else if (iceCount > 0) baseSpell = "Ice";
    //     else if (shieldCount > 0) baseSpell = "Shield";
    //     else if (moveCount > 0) baseSpell = "Move";

    //     Debug.Log("Base Spell: " + baseSpell);
    // }
    

}
