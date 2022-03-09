using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2D : MonoBehaviour
{
    public float mass;
    public float inverseMass;
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 accumulatedForces;
    public float dampeningConstant;

    public float particleType;
    public bool staticParticle;

    public GameObject[] particles;
    public GameObject[] boxes;
    public int particleLimit = 25;
    public int count;

    public GameObject rightWall;
    public GameObject leftWall;
    public GameObject downWall;
    public GameObject upWall;

    // Start is called before the first frame update
    void Start()
    {
        if (staticParticle)
        {
            mass = 2.0f;
            inverseMass = 1.0f / mass;
            acceleration = new Vector2(0.0f, 0.0f);
            dampeningConstant = 0.99f;
            velocity = new Vector2(0.0f, 0.0f);
        }
        particles = new GameObject[particleLimit];
        count = 0;
        rightWall = GameObject.FindGameObjectWithTag("Right");
        leftWall = GameObject.FindGameObjectWithTag("Left");
        downWall = GameObject.FindGameObjectWithTag("Down");
        upWall = GameObject.FindGameObjectWithTag("Up");
        boxes = new GameObject[15];
    }

    // Update is called once per frame
    void Update()
    {
        inverseMass = 1.0f / mass; //get the inverse of the game objects mass
        particles = GameObject.FindGameObjectWithTag("Gun").GetComponent<GunControl>().projectiles;
        count = GameObject.FindGameObjectWithTag("Gun").GetComponent<GunControl>().count;
        boxes = GameObject.FindGameObjectsWithTag("barrel");
        /*
        if (!staticParticle)
        {
            inverseMass = 1.0f / mass; //get the inverse of the game objects mass
            
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
                //GetComponent<BuoyancyForce>().updateForce(this.gameObject);
                //GetComponent<PushForce>().updateForce(this.gameObject);
                //GetComponent<PullForce>().updateForce(this.gameObject);
            }
            else
            {
                GetComponent<PushForce>().updateForce(this.gameObject);
                GetComponent<PullForce>().updateForce(this.gameObject);
            }
        }
        */
        
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    acceleration = new Vector2(0.0f, -30.0f);
        //}
        //else if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    acceleration = new Vector2(0.0f, 30.0f);
        //}
        //else if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    acceleration = new Vector2(30.0f, 0.0f);
        //}
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    acceleration = new Vector2(-30.0f, 0.0f);
        //}
        //else
        //{
        //    acceleration = new Vector2(0.0f, 0.0f);
        //}
    }

    //If we leave the Camera's view, destroy ourselves.
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        Integrator.integrate2D(this.gameObject);
        foreach (GameObject barrel in boxes)
        {
            if (barrel)
            {
                CollisionDetection.collideBox2D(this.gameObject, barrel);
            }
        }
        /*
        if (count > 1)
        {
            /*
            foreach (GameObject particle in particles)
            {
                if (particle != this.gameObject && particle != null)
                {
                    CollisionDetection.collideSphere2D(this.gameObject, particle);
                }
            }
            
            for(int i = int.Parse(name); i < particleLimit; i++)
            {
                for(int j = i + 1; j < particleLimit; j++)
                {
                    if (particles[i] != null && particles[j] != null)
                    {
                        CollisionDetection.collideSphere2D(particles[i], particles[j]);
                    }
                }
            }
        }
        */
        //CollisionDetection.collideWall2D(this.gameObject, rightWall);
        //CollisionDetection.collideWall2D(this.gameObject, leftWall);
        //CollisionDetection.collideWall2D(this.gameObject, downWall);
        //CollisionDetection.collideWall2D(this.gameObject, upWall);
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
