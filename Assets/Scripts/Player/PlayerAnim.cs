using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnim : MonoBehaviour
{
    Animator animator;
    public bool isMoving = false;
    public bool isMovingLeft = false;
    SpriteRenderer sr;
    [SerializeField] GameObject blackoutPanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] EnemySpawner enemySpawner;
    bool hasShownGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMovingLeft)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
        animator.SetBool("IsMoving", isMoving);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    public void TriggerDashAnimation()
    {
        animator.SetTrigger("Dash");
        animator.SetBool("IsDashing", true);
    }

    public void StopDashAnimation()
    {
        animator.SetBool("IsDashing", false);
        animator.ResetTrigger("Dash");
    }

    public void Die()
    {
        if(hasShownGameOver) return;
        hasShownGameOver = true;
        StartCoroutine(Blackout());
        animator.SetTrigger("Die");
        // Disable player movement
        Transform player = transform.parent;
        player.GetComponent<PlayerMovement>().canMove = false;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player.GetComponent<Collider2D>().enabled = false;
        player.GetComponent<SpellChecker>().isDead = true;
        enemySpawner.StopAllCoroutines();
    }
    
    IEnumerator Blackout()
    {
        blackoutPanel.SetActive(true);
        float timer = 0;
        while (timer < 1.5f)
        {
            timer += Time.deltaTime;
            blackoutPanel.GetComponent<Image>().color = new Color(0, 0, 0, timer);
            yield return null;
        }
        blackoutPanel.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        gameOverPanel.GetComponent<GameOverScript>().ShowResult();
    }
}
