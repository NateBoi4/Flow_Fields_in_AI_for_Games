using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle3D : MonoBehaviour
{
    public float mass;
    public float inverseMass;
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 accumulatedForces;
    public float dampeningConstant;

    public float particleType;

    public GameObject[] particles;
    public int particleLimit = 100;
    public int count;

    public GameObject rightWall;
    public GameObject leftWall;
    public GameObject downWall;
    public GameObject upWall;
    public GameObject forwardWall;
    public GameObject backWall;
    public GameObject staticSphere;

    public GameObject[] boxes;

    public GameObject[] enemies;

    public float lifetime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        particles = new GameObject[particleLimit];
        count = 0;

        //rightWall = GameObject.FindGameObjectWithTag("Right");
        //leftWall = GameObject.FindGameObjectWithTag("Left");
        //downWall = GameObject.FindGameObjectWithTag("Down");
        //upWall = GameObject.FindGameObjectWithTag("Up");
        //forwardWall = GameObject.FindGameObjectWithTag("Forward");
        //backWall = GameObject.FindGameObjectWithTag("Back");

        //boxes = GameObject.FindGameObjectsWithTag("Boxes");

        //staticSphere = GameObject.FindGameObjectWithTag("Sphere");

        enemies = GameObject.FindGameObjectsWithTag("Enemies");
    }

    private void Awake()
    {
        Destroy(this.gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemies");
        inverseMass = 1.0f / mass; //get the inverse of the game objects mass
        //particles = GameObject.FindGameObjectWithTag("Gun").GetComponent<GunControl>().projectiles;
        //count = GameObject.FindGameObjectWithTag("Gun").GetComponent<GunControl>().count;
        /*
            
        if (particleType == 1)
        {
            GetComponent<BasicSpringForce>().updateForce(this.gameObject);
            GetComponent<PushForce>().updateForce(this.gameObject);
            GetComponent<PullForce>().updateForce(this.gameObject);
        }
        else if (particleType == 4)
        {
            GetComponent<AnchoredSpringForce>().updateForce(this.gameObject);
            GetComponent<PushForce>().updateForce(this.gameObject);
            GetComponent<PullForce>().updateForce(this.gameObject);
        }
        else if (particleType == 3)
        {
            GetComponent<BungieForce>().updateForce(this.gameObject);
            GetComponent<PushForce>().updateForce(this.gameObject);
            GetComponent<PullForce>().updateForce(this.gameObject);
        }
        else if (particleType == 2)
        {
            GetComponent<BuoyancyForce>().updateForce(this.gameObject);
            GetComponent<PushForce>().updateForce(this.gameObject);
            GetComponent<PullForce>().updateForce(this.gameObject);
        }
        else
        {
            GetComponent<PushForce>().updateForce(this.gameObject);
            GetComponent<PullForce>().updateForce(this.gameObject);
        }
        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            acceleration = new Vector2(0.0f, -30.0f);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            acceleration = new Vector2(0.0f, 30.0f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            acceleration = new Vector2(30.0f, 0.0f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            acceleration = new Vector2(-30.0f, 0.0f);
        }
        else
        {
            acceleration = new Vector2(0.0f, 0.0f);
        }

        */
    }

    void FixedUpdate()
    {
        Integrator.integrate3D(this.gameObject);
        foreach (GameObject enemy in enemies)
        {
            if (enemy)
            {
                CollisionDetection.collideSphere3D(this.gameObject, enemy);
            }
        }

        //if (this.tag != "Sphere")
        //{
        //    if (count > 1)
        //    {
        //        /*
        //        foreach (GameObject particle in particles)
        //        {
        //            if (particle != this.gameObject && particle != null)
        //            {
        //                CollisionDetection.collideSphere3D(this.gameObject, particle);
        //            }
        //        }
        //        *///*
        //        for (int i = int.Parse(name); i < particleLimit; i++)
        //        {
        //            for (int j = i + 1; j < particleLimit; j++)
        //            {
        //                if (particles[i] != null && particles[j] != null)
        //                {
        //                    CollisionDetection.collideSphere3D(particles[i], particles[j]);
        //                }
        //            }
        //        }
        //        //*/
        //    }
        //}

        //CollisionDetection.collideWall3D(this.gameObject, rightWall);
        //CollisionDetection.collideWall3D(this.gameObject, leftWall);
        //CollisionDetection.collideWall3D(this.gameObject, downWall);
        //CollisionDetection.collideWall3D(this.gameObject, upWall);
        //CollisionDetection.collideWall3D(this.gameObject, forwardWall);
        //CollisionDetection.collideWall3D(this.gameObject, backWall);

        //if (this.tag != "Sphere")
        //{
        //    CollisionDetection.collideSphere3D(this.gameObject, staticSphere);
        //}

        //foreach (GameObject box in boxes)
        //{
        //    CollisionDetection.collideBox3D(this.gameObject, box);
        //}
    }

    public void clearAccumulator()
    {
        //Clear Forces
        accumulatedForces = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void addForce(Vector3 force)
    {
        //Add a force
        accumulatedForces += force;
    }
}
