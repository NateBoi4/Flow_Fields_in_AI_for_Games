using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunControl : MonoBehaviour
{
    //Bullet and Target game objects
    public GameObject sphere;
    public GameObject target;

    public GameObject[] projectiles;

    //Scale the bullet size
    public Vector3 scaleChange;
    //Gun barrel configurations
    public Vector3 barrelPos;
    public Vector3 barrelDir;
    public Quaternion barrelRot;
    //Position to spawn projectile
    public Vector3 projectileSpawn;

    //Type of shot being used between pistol, artillery, fireball, and laser
    public int projectileType;
    //Amount of time fire key is held for
    public float holdShot;
    //Number of spawned projectiles
    public int count;
    public int projectileLimit = 25;

    public int score = 0;
    public Text scoreText;

    public int shotCount = 25;
    public Text shotCountText;

    // Start is called before the first frame update
    void Start()
    {
        //Set default values for shot type and fire time
        projectileType = 2;
        holdShot = 1.0f;
        count = 0;
        projectiles = new GameObject[projectileLimit];

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = ("Score: " + score);
        shotCountText.text = ("Shots Left: " + shotCount);
        //Find the position of the gun barrel
        barrelPos = transform.position;
        //Find the front end of the gun barrel
        barrelDir = transform.up;
        //Find the rotation of the gun barrel
        barrelRot = transform.rotation;

        //Rotate the Gun to track the target
        Vector3 relativePos = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        rotation *= Quaternion.Euler(new Vector3(0, -90, -90));
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 1.0f);

        //Spawn the projectile at the front end of the gun barrel
        projectileSpawn = barrelPos + barrelDir;

        if (Input.GetKey(KeyCode.Alpha1)) 
        {
            //Rotate the gun in the positive Z direction
            transform.Rotate(new Vector3(0.0f, 0.0f, 0.25f), Space.World);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            //Rotate the gun in the negative Z direction
            transform.Rotate(new Vector3(0.0f, 0.0f, -0.25f), Space.World);
        }
        /*
        if (Input.GetKeyDown(KeyCode.W)) //Swap between 3 projectile types
        {
            if(projectileType < 2)
            {
                projectileType++; //Increment shot type
            }
            else
            {
                projectileType = 1; //Reset shot type if incremented past limit
            }
        }
        */
        if (Input.GetKey(KeyCode.Return))
        {
            //Get how long the shot is charged for up to a set limit.
            if (holdShot < 5.0f)
            {
                holdShot += 0.01f;
            }
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (shotCount > 0)
            {
                //Fire projectile and reset hold shot time
                if (Camera.main.orthographic)
                {
                    fireProjectile2D();
                }
                else
                {
                    fireProjectile3D();
                }
            }
            holdShot = 1.0f;
        }

        if (Input.GetKey(KeyCode.C)) 
        {
            foreach(GameObject projectile in projectiles)
            {
                Destroy(projectile);
            }
            count = 0;
        }
    }

    void fireProjectile2D()
    {
        //Set up projectile particle
        //Spawn a clone of the existing bullet prefab
        //Scale the clone size to fit inside the barrel
        //Get the position of the target to shoot at
        //Add the particle 2D component to clone object
        GameObject sphereClone;
        sphereClone = Instantiate(sphere, projectileSpawn, barrelRot);
        sphereClone.name = count.ToString();
        projectiles[count] = sphereClone;
        count++;
        shotCount--;
        //sphereClone.transform.localScale += scaleChange;
        Vector3 targetPos = target.transform.position;
        sphereClone.AddComponent<Particle2D>();
        sphereClone.GetComponent<Particle2D>().staticParticle = false;
        sphereClone.GetComponent<Particle2D>().particleType = projectileType;
        switch (projectileType) //Check Shot type and apply appropriate forces to bullet clone
        {
            case 1: //BOWLING BALL
                sphereClone.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                sphereClone.GetComponent<Particle2D>().velocity = (targetPos - projectileSpawn) * holdShot;
                sphereClone.GetComponent<Particle2D>().mass = 200.0f;
                sphereClone.GetComponent<Particle2D>().acceleration = new Vector2(0.0f, 0.0f);
                sphereClone.GetComponent<Particle2D>().dampeningConstant = 0.99f;
                sphereClone.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case 2: //BUOYANCY BALL
                sphereClone.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
                sphereClone.GetComponent<Particle2D>().velocity = (targetPos - projectileSpawn) * holdShot;
                sphereClone.GetComponent<Particle2D>().mass = 0.5f;
                sphereClone.GetComponent<Particle2D>().acceleration = new Vector2(0.0f, -10.0f);
                sphereClone.GetComponent<Particle2D>().dampeningConstant = 0.5f;
                sphereClone.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case 3: //MARBLE
                sphereClone.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
                sphereClone.GetComponent<Particle2D>().velocity = (targetPos - projectileSpawn) * holdShot;
                sphereClone.GetComponent<Particle2D>().mass = 2.0f;
                sphereClone.GetComponent<Particle2D>().acceleration = new Vector2(0.0f, 0.0f);
                sphereClone.GetComponent<Particle2D>().dampeningConstant = 0.09f;
                sphereClone.transform.localScale += new Vector3(-0.5f, -0.5f, -0.5f);
                break;
            case 4: //UNUSED
                sphereClone.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.yellow);
                sphereClone.GetComponent<Particle2D>().velocity = (targetPos - projectileSpawn) * holdShot;
                sphereClone.GetComponent<Particle2D>().mass = 0.1f;
                sphereClone.GetComponent<Particle2D>().acceleration = new Vector2(0.0f, 0.0f);
                sphereClone.GetComponent<Particle2D>().dampeningConstant = 0.99f;
                break;
            default:
                sphereClone.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.black);
                break;

        }
    }

    void fireProjectile3D()
    {
        //Set up projectile particle
        //Spawn a clone of the existing bullet prefab
        //Scale the clone size to fit inside the barrel
        //Get the position of the target to shoot at
        //Add the particle 2D component to clone object
        GameObject sphereClone;
        sphereClone = Instantiate(sphere, projectileSpawn, barrelRot);
        sphereClone.name = count.ToString();
        projectiles[count] = sphereClone;
        count++;
        //sphereClone.transform.localScale += scaleChange;
        Vector3 targetPos = target.transform.position;
        sphereClone.AddComponent<Particle3D>();
        sphereClone.GetComponent<Particle3D>().particleType = projectileType;
        switch (projectileType) //Check Shot type and apply appropriate forces to bullet clone
        {
            case 1: //BOWLING BALL
                sphereClone.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                sphereClone.GetComponent<Particle3D>().velocity = (targetPos - projectileSpawn) * holdShot;
                sphereClone.GetComponent<Particle3D>().mass = 200.0f;
                sphereClone.GetComponent<Particle3D>().acceleration = new Vector3(0.0f, 0.0f, 0.0f);
                sphereClone.GetComponent<Particle3D>().dampeningConstant = 0.99f;
                sphereClone.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case 2: //BASEBALL
                sphereClone.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
                sphereClone.GetComponent<Particle3D>().velocity = (targetPos - projectileSpawn) * holdShot;
                sphereClone.GetComponent<Particle3D>().mass = 20.0f;
                sphereClone.GetComponent<Particle3D>().acceleration = new Vector3(0.0f, 0.0f, 0.0f);
                sphereClone.GetComponent<Particle3D>().dampeningConstant = 0.99f;
                sphereClone.transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
                break;
            case 3: //MARBLE
                sphereClone.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
                sphereClone.GetComponent<Particle3D>().velocity = (targetPos - projectileSpawn) * holdShot;
                sphereClone.GetComponent<Particle3D>().mass = 2.0f;
                sphereClone.GetComponent<Particle3D>().acceleration = new Vector3(0.0f, 0.0f, 0.0f);
                sphereClone.GetComponent<Particle3D>().dampeningConstant = 0.99f;
                sphereClone.transform.localScale += new Vector3(-0.5f, -0.5f, -0.5f);
                break;
            default:
                sphereClone.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.black);
                break;

        }
    }
}
