using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject slashPrefab;
    public float slashCooldown = 0.5f;
    public float slashDamage;
    bool canSlash = true;
    public bool allowSlash = true;
    Vector2 slashDirection;
    AudioSource audioSource;
    [SerializeField] AudioClip slashSound;
    [SerializeField] AudioClip reflectSound;
    // Start is called before the first frame update
    void Start()
    {
        allowSlash = true;
        canSlash = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(allowSlash){
            if(canSlash && Input.GetMouseButton(0))
            {
                StartCoroutine(PerformSlash());
            }
        }
    }

    IEnumerator PerformSlash()
    {
        canSlash = false;
        GameObject slash = Instantiate(slashPrefab, transform);
        SlashAudio();
        yield return new WaitForSeconds(slashCooldown);
        canSlash = true;
    }

    public void ReflectAudio(){
        // Debug.Log("Reflecting");
        float randomPitch = Random.Range(0.8f, 1.2f);
        audioSource.pitch = randomPitch;
        audioSource.PlayOneShot(reflectSound);
    }

    public void SlashAudio(){
        float randomPitch = Random.Range(0.8f, 1.2f);
        audioSource.pitch = randomPitch;
        audioSource.PlayOneShot(slashSound);
    }
}
