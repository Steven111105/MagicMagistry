using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellChecker : MonoBehaviour
{
    public GameObject canvasObject;
    public GameObject fullSpellTextPrefab;
    public TMP_Text spellText;
    private List<string> inputSequence = new List<string>();
    private List<string> currentComponents = new List<string>();
    private string confirmCode = "RL";
    private string cancelCode = "RR";
    private Dictionary<string, string> spellBook = new Dictionary<string, string>()
    {
        { "Fire", "LLL"},
        { "Ice", "LLR"},
        { "Wall", "LRL"},
        { "Move", "LRR"},
    };

    public bool isDead = false;
    AudioSource audioSource;
    [SerializeField] AudioClip castSFX;
    [SerializeField] AudioClip addSpellSFX;
    void OnEnable()
    {
        isDead = false;
        spellText.text = "";
        inputSequence.Clear();
        currentComponents.Clear();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isDead)
        {
            if (Input.GetMouseButtonDown(0)) // Left Click
            {
                inputSequence.Add("L");
                spellText.text += "L";
                CheckInput();
            }
            else if (Input.GetMouseButtonDown(1)) // Right Click
            {
                inputSequence.Add("R");
                spellText.text += "R";
                CheckInput();
            }
        }
    }

    private void CheckInput()
    {
        string currentInput = string.Join("", inputSequence);

        // First, check if the input is the Confirm code
        if (currentInput == confirmCode)
        {
            // Debug.Log("Confirmed");
            ConfirmSpell();
            inputSequence.Clear();
            return;
        }
        else if (currentInput == cancelCode)
        {
            // Debug.Log("Cancelled");
            spellText.text = "";
            currentComponents.Clear();
            inputSequence.Clear();
            return;
        }

        // Otherwise, check if it matches a spell
        foreach (var spell in spellBook)
        {
            if (currentInput == spell.Value)
            {
                if (currentComponents.Count != 3)
                {
                    currentComponents.Add(spell.Key);
                    // Remove the length of the spell from the right of the text
                    spellText.text = spellText.text.Substring(0, spellText.text.Length - spell.Value.Length);

                    // Replace it with the name of the spell
                    if (currentComponents.Count != 1)
                    {
                        spellText.text += " + ";
                    }
                    spellText.text += " " + spell.Key + " ";
                    // Debug.Log("Added component: " + spell.Key);
                    inputSequence.Clear();
                    audioSource.PlayOneShot(addSpellSFX);
                    return;
                }
                else
                {
                    spellText.text = spellText.text.Substring(0, spellText.text.Length - spell.Value.Length);
                    inputSequence.Clear();
                    ShowFullSpellUI();
                }
            }
            if (!IsPrefixOfAnySpell(currentInput))
            {
                Debug.Log("Invalid sequence. Resetting current input.");
                spellText.text = spellText.text.Substring(0, spellText.text.Length - inputSequence.Count);
                inputSequence.Clear();
                return;
            }
        }
    }

    private bool IsPrefixOfAnySpell(string sequence)
    {
        foreach (var spell in spellBook.Values)
        {
            if (spell.StartsWith(sequence))
            {
                return true;
            }
        }
        // Also allow prefixing for Confirm
        return confirmCode.StartsWith(sequence);
    }

    private void ConfirmSpell()
    {
        if (currentComponents.Count == 0)
        {
            Debug.Log("No components to cast!");
            spellText.text = "";
            currentComponents.Clear();
            return;
        }

        string fullSpell = string.Join(" + ", currentComponents);
        // Debug.Log("Casting spell: " + fullSpell);

        GetComponent<SpellCaster>().CastSpell(currentComponents);
        audioSource.PlayOneShot(castSFX);

        spellText.text = "";
        currentComponents.Clear();
    }
    
    void ShowFullSpellUI()
    {
        GameObject text = Instantiate(fullSpellTextPrefab, spellText.transform.position, Quaternion.identity, spellText.transform);
        text.GetComponent<TMP_Text>().text = "Spell Slot is Full!";
        text.transform.localPosition = new Vector3(0f, 200f, 0f);
        return;
    }
}

