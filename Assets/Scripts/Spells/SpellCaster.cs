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
    [SerializeField] private GameObject firePoolPrefab;
    [SerializeField] private GameObject fireProjectilePrefab;
    [SerializeField] private GameObject iceSprayPrefab;
    [SerializeField] private GameObject iceProjectilePrefab;
    [SerializeField] private GameObject fireIcePoolPrefab;
    [SerializeField] private GameObject fireIceProjectilePrefab;
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

        //Wall
        if (baseSpell == "Wall")
        {
            List<GameObject> walls = new List<GameObject>();
            //Create shield pillar, then change the element accordingly
            Debug.Log("Casting Shield Pillar");
            // Add logic to create a shield pillar without move effect
            //create 3 shield pillars in a row in front of player
            GameObject shieldPillar = Instantiate(wallSpellPrefab, playerTransform.position + playerTransform.up * 1.5f, playerTransform.rotation);
            walls.Add(shieldPillar);
            if (wallCount >= 2)
            {
                //2 walls
                GameObject shieldPillar2 = Instantiate(wallSpellPrefab, shieldPillar.transform.position - shieldPillar.transform.right, playerTransform.rotation);
                GameObject shieldPillar3 = Instantiate(wallSpellPrefab, shieldPillar.transform.position + shieldPillar.transform.right, playerTransform.rotation);
                walls.Add(shieldPillar2);
                walls.Add(shieldPillar3);
                if (wallCount == 3)
                {
                    //3 walls
                    shieldPillar.transform.position = playerTransform.position + playerTransform.up * 2;
                    shieldPillar2.transform.position = shieldPillar.transform.position - shieldPillar.transform.right - shieldPillar.transform.up;
                    shieldPillar3.transform.position = shieldPillar.transform.position + shieldPillar.transform.right - shieldPillar.transform.up;
                    GameObject shieldPillar4 = Instantiate(wallSpellPrefab, shieldPillar.transform.position - shieldPillar.transform.right * 2 - shieldPillar.transform.up * 2, playerTransform.rotation);
                    GameObject shieldPillar5 = Instantiate(wallSpellPrefab, shieldPillar.transform.position + shieldPillar.transform.right * 2 - shieldPillar.transform.up * 2, playerTransform.rotation);
                    walls.Add(shieldPillar4);
                    walls.Add(shieldPillar5);
                }
            }

            if (moveCount > 0)
            {
                foreach (GameObject wall in walls)
                {
                    // Add move effect to the wall
                    // For example, make the wall move towards the player
                    wall.GetComponent<Rigidbody2D>().velocity = wall.transform.up * 3f * moveCount;
                    wall.GetComponent<WallScript>().SpawnWall(3f);
                }
            }
            else
            {
                foreach (GameObject wall in walls)
                {
                    // If no move effect, just set the wall to be stationary
                    wall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    //and last longer
                    wall.GetComponent<WallScript>().SpawnWall(7f);
                }
            }
            foreach (GameObject wall in walls)
            {
                wall.transform.rotation = Quaternion.identity;
                if (fireCount >= 1)
                {
                    Debug.Log("Adding Fire Effect to Wall(1 fire)");
                    // Add fire effect to the wall
                    wall.AddComponent<FireWall>();
                    if (fireCount == 2)
                    {
                        Debug.Log("2 fire walls");
                        GameObject puddle = Instantiate(firePoolPrefab, wall.transform.position, wall.transform.rotation);
                        puddle.transform.localScale = new Vector3(2.5f, 2.5f, 1f);
                        puddle.GetComponent<FirePuddle>().damage = 2;
                    }
                }
            }

            
        }
        else if (baseSpell == "Fire")
        {
            if (moveCount != 0)
            {
                //fireball
                GameObject fireball = Instantiate(fireProjectilePrefab, playerTransform.position, Quaternion.Euler(0, 0, playerTransform.rotation.eulerAngles.z));
                // Debug.Log("Player angle" + playerTransform.rotation.eulerAngles.z);
                // Debug.Log("Casting Fireball, angle" + fireball.transform.rotation.eulerAngles.z);
                // Instantiate the bullet
                fireball.GetComponent<Projectile>().Shoot(transform.up, 5*moveCount, false, 0, false);
                if (fireCount == 2)
                {
                    fireball.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                    fireball.GetComponent<Fireball>().SetupSize(2);
                    fireball.GetComponent<Fireball>().damage = 10f;
                }
                else
                {
                    fireball.GetComponent<Fireball>().SetupSize(1);
                    fireball.GetComponent<Fireball>().damage = 5f;
                }
            }
            else
            {
                if (iceCount == 0)
                {
                    //fire puddle
                    GameObject firePuddle = Instantiate(firePoolPrefab, playerTransform.position + playerTransform.up * 2f, playerTransform.rotation);
                    firePuddle.GetComponent<FirePuddle>().damage = 1;
                    if (fireCount == 2)
                    {
                        firePuddle.transform.localScale = new Vector3(2.5f, 2.5f, 1f);
                        firePuddle.GetComponent<FirePuddle>().damage = 2;
                    }
                    else if (fireCount == 3)
                    {
                        firePuddle.transform.localScale = new Vector3(3.5f, 3.5f, 1f);
                        firePuddle.GetComponent<FirePuddle>().damage = 3;
                    }
                }
                else
                {
                    //has 1 or 2 ice, so fire ice puddle
                    //1 ice, 1 fire
                    GameObject fireIcePuddle = Instantiate(fireIcePoolPrefab, playerTransform.position + playerTransform.up * 2f, playerTransform.rotation);
                    fireIcePuddle.GetComponent<FireIcePuddle>().damage = 1;

                    //2 ice, 1 fire
                    if (iceCount == 2 && fireCount == 1)
                    {
                        fireIcePuddle.transform.localScale = new Vector3(2.5f, 2.5f, 1f);
                    }
                    //1 ice, 2 fire
                    else if (iceCount == 1 && fireCount == 2)
                    {
                        fireIcePuddle.GetComponent<FireIcePuddle>().damage = 2;
                    }
                }
            }
        }
        else if (baseSpell == "Ice")
        {
            if (moveCount > 0)
            {
                Debug.Log("Casting Ice Ball");
                GameObject iceBall = Instantiate(iceProjectilePrefab, playerTransform.position, Quaternion.Euler(0, 0, playerTransform.rotation.eulerAngles.z));
                iceBall.GetComponent<Projectile>().Shoot(transform.up, 5*moveCount, false, 0, false);
                if (iceCount == 1)
                {
                    iceBall.GetComponent<IceBall>().SetupSize(1);
                    iceBall.GetComponent<IceBall>().damage = 5f;
                }
                else
                {
                    //2 ice
                    iceBall.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                    iceBall.GetComponent<IceBall>().SetupSize(2);
                    iceBall.GetComponent<IceBall>().damage = 10f;
                }
            }
            else
            {
                Debug.Log("Ice Spray");
                // Instantiate the ice spell
                GameObject iceSpell = Instantiate(iceSprayPrefab, playerTransform.position, playerTransform.rotation);
                if (iceCount == 1)
                {
                    iceSpell.transform.localScale = new Vector3(3f, 3.5f, 1f);
                }
                else if (iceCount == 2)
                {
                    iceSpell.transform.localScale = new Vector3(4.5f, 4f, 1f);
                    iceSpell.GetComponent<IceSpray>().damage = 5f;
                }
                else if (iceCount == 3)
                {
                    iceSpell.transform.localScale = new Vector3(6f, 5f, 1f);
                    iceSpell.GetComponent<IceSpray>().damage = 10f;
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
        foreach (string component in spellComponents)
        {
            switch (component)
            {
                case "Fire":
                    fireCount++;
                    break;
                case "Ice":
                    iceCount++;
                    break;
                case "Wall":
                    wallCount++;
                    break;
                case "Move":
                    moveCount++;
                    break;
                default:
                    Debug.LogWarning("Unknown spell component: " + component);
                    break;
            }
        }
        ProcessBaseSpell();
    }

    void ProcessBaseSpell()
    {
        baseSpell = "";

        if (wallCount > 0) baseSpell = "Wall";
        else if (fireCount > 0) baseSpell = "Fire";
        else if (iceCount > 0) baseSpell = "Ice";
        else if (moveCount > 0) baseSpell = "Move";

        Debug.Log("Base Spell: " + baseSpell);
    }
    

}
