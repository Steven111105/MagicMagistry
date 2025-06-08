using UnityEngine;

public class ShootingEnemy : Enemy
{
    [SerializeField] public float shootingRange = 10f;
    float approachRange;
    [SerializeField] public float shootingCooldown = 1f;
    float shootProgress;
    [SerializeField] public float bulletSpeed = 10f;
    [SerializeField] public float bulletDamage;
    [SerializeField] public GameObject bulletPrefab;
    bool isWaiting = false;
    public override void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //basically get closer to the player, not just on the edge of range
        approachRange = shootingRange * 0.6f;
        bulletDamage = damage;
        isWaiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if distance not in range, move towards player
        if (!NeedToMove(isWaiting))
        {
            PrepareShoot();
            isWaiting = true;
        }
        else
        {
            isWaiting = false;
            shootProgress = 0;
            Move();
        }
        if (!flashing)
        {
            // Debug.Log("Enemy is not flashing, change to white");
            sr.color = new Color(255, 255, 255); // Reset color to white
        }
    }

    bool NeedToMove(bool isWaiting){
        float distance = Vector3.Distance(transform.position, player.position);
        if(isWaiting){
            //is waiting means it has prepared a shoot so check within shooting range
            return distance >= shootingRange;
        }else{
            //not preparing shoot, so it needs to get inside approach range
            return distance >= approachRange;
        }
    }

    void PrepareShoot(){
        rb.velocity = Vector2.zero; // Stop the enemy's movement
        shootProgress += Time.deltaTime;
        if (shootProgress >= shootingCooldown)
        {
            Shoot();
            shootProgress = 0;
        }
    }


    public override void Move(){
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * currentSpeed;
        }
    }

    void Shoot()
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().Shoot((player.position - transform.position).normalized, bulletSpeed, true, damage);
    }
        
}
