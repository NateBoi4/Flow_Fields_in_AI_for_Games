using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 5.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    public GameObject sphere;
    public GameObject weapon;
    public GameObject target;

    public float speed = 6.0f;

    public float rotationSpeed = 0.1f;
    private float rotationVelocity;

    private int projectileType;

    private Vector3 barrelPos;
    private Vector3 barrelDir;
    private Quaternion barrelRot;
    private Vector3 projectileSpawn;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        projectileType = 1;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        //if (Input.GetButtonDown("Jump") && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //}

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //Find the position of the gun barrel
        barrelPos = weapon.transform.position;
        //Find the front end of the gun barrel
        barrelDir = weapon.transform.forward;
        //Find the rotation of the gun barrel
        barrelRot = weapon.transform.rotation;

        projectileSpawn = barrelPos + barrelDir;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile3D();
        }

        
    }

    private void FireProjectile3D()
    {
        //Set up projectile particle
        //Spawn a clone of the existing bullet prefab
        //Scale the clone size to fit inside the barrel
        //Get the position of the target to shoot at
        //Add the particle 2D component to clone object
        GameObject sphereClone;
        sphereClone = Instantiate(sphere, projectileSpawn, barrelRot);
        //sphereClone.name = count.ToString();
        //projectiles[count] = sphereClone;
        //count++;
        //sphereClone.transform.localScale += scaleChange;
        Vector3 targetPos = target.transform.position;
        sphereClone.AddComponent<Particle3D>();
        sphereClone.GetComponent<Particle3D>().particleType = projectileType;
        switch (projectileType) //Check Shot type and apply appropriate forces to bullet clone
        {
            case 1: //BOWLING BALL
                sphereClone.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.yellow);
                sphereClone.GetComponent<Particle3D>().velocity = (projectileSpawn - targetPos) * 20.0f;
                sphereClone.GetComponent<Particle3D>().mass = 2.0f;
                sphereClone.GetComponent<Particle3D>().acceleration = new Vector3(0.0f, 0.0f, 0.0f);
                sphereClone.GetComponent<Particle3D>().dampeningConstant = 0.99f;
                sphereClone.transform.localScale += new Vector3(-0.9f, -0.9f, -0.9f);
                break;
            case 2: //BASEBALL
                sphereClone.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
                sphereClone.GetComponent<Particle3D>().velocity = (targetPos - projectileSpawn);
                sphereClone.GetComponent<Particle3D>().mass = 20.0f;
                sphereClone.GetComponent<Particle3D>().acceleration = new Vector3(0.0f, 0.0f, 0.0f);
                sphereClone.GetComponent<Particle3D>().dampeningConstant = 0.99f;
                sphereClone.transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
                break;
            case 3: //MARBLE
                sphereClone.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
                sphereClone.GetComponent<Particle3D>().velocity = (targetPos - projectileSpawn);
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
