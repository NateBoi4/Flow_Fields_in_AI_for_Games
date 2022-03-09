using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection
{
    static public void collideSphere2D(GameObject sphereOne, GameObject sphereTwo)
    {
        Vector2 sphereOnePos = new Vector2(sphereOne.transform.position.x, sphereOne.transform.position.y);
        Vector2 sphereTwoPos = new Vector2(sphereTwo.transform.position.x, sphereTwo.transform.position.y);
        Vector2 midline = sphereOnePos - sphereTwoPos;
        float size = Mathf.Sqrt((midline.x * midline.x) + (midline.y * midline.y));

        float segmentOne = sphereOne.transform.localScale.x * sphereOne.GetComponent<SphereCollider>().radius;
        float segmentTwo = sphereTwo.transform.localScale.x * sphereTwo.GetComponent<SphereCollider>().radius;

        bool colliding = (segmentOne + segmentTwo) >= size;

        if (colliding)
        {
            float penetration = segmentOne + segmentTwo - size;

            float totalInverseMass = (1 / sphereOne.GetComponent<Particle2D>().mass) + (1 / sphereTwo.GetComponent<Particle2D>().mass);

            Vector2 normal = midline / size;

            Vector2 movePerMass = normal * (penetration / totalInverseMass);

            float restitution = 1.0f;

            Vector2 vOne = new Vector2(sphereOne.GetComponent<Particle2D>().velocity.x, sphereOne.GetComponent<Particle2D>().velocity.y);
            Vector2 vTwo = new Vector2(sphereTwo.GetComponent<Particle2D>().velocity.x, sphereTwo.GetComponent<Particle2D>().velocity.y);

            Vector2 separatingVelocity = (vOne - vTwo) * normal;

            Vector2 newSeparatingVelocity = -(separatingVelocity) * restitution;

            //Vector2 contactPoint = new Vector2(sphereOne.transform.position.x, sphereOne.transform.position.y) + midline * 0.5f;
            //Vector3 contactPoint = sphereOne.transform.position + midline * 0.5f;

            //Vector2 contact = new Vector2(0.0f, 0.0f) + contactPoint;

            //Vector3 movementOne = movePerMass * (1 / sphereOne.GetComponent<Particle2D>().mass);
            //Vector3 movementTwo = movePerMass * (1 / sphereTwo.GetComponent<Particle2D>().mass);

            Vector2 deltaVelocity = newSeparatingVelocity - separatingVelocity;

            Vector2 impulseVelocity = deltaVelocity / totalInverseMass;

            Vector2 velImpulsePerMass = normal * impulseVelocity;

            Vector2 newVelocityOne = vOne + (velImpulsePerMass / sphereOne.GetComponent<Particle2D>().mass);
            Vector2 newVelocityTwo = vTwo - (velImpulsePerMass / sphereTwo.GetComponent<Particle2D>().mass);

            /*
            float inMomentum = Mathf.Abs(sphereOne.GetComponent<Particle2D>().mass * sphereOne.GetComponent<Particle2D>().velocity.sqrMagnitude
                + sphereTwo.GetComponent<Particle2D>().mass * sphereTwo.GetComponent<Particle2D>().velocity.sqrMagnitude);
            float outMomentum = Mathf.Abs(sphereOne.GetComponent<Particle2D>().mass * newVelocityOne.sqrMagnitude
                + sphereTwo.GetComponent<Particle2D>().mass * newVelocityTwo.sqrMagnitude);
            */

            sphereOne.GetComponent<Particle2D>().velocity = newVelocityOne;
            sphereTwo.GetComponent<Particle2D>().velocity = newVelocityTwo;
        }
    }

    static public void collideSphere3D(GameObject sphereOne, GameObject sphereTwo)
    {
        Vector3 sphereOnePos = sphereOne.transform.position;
        Vector3 sphereTwoPos = sphereTwo.transform.position;
        Vector3 midline = sphereOnePos - sphereTwoPos;
        float size = Mathf.Sqrt((midline.x * midline.x) + (midline.y * midline.y) + (midline.z * midline.z));

        float segmentOne = sphereOne.transform.localScale.x * sphereOne.GetComponent<SphereCollider>().radius;
        float segmentTwo = sphereTwo.transform.localScale.x * sphereTwo.GetComponent<SphereCollider>().radius;

        bool colliding = (segmentOne + segmentTwo) >= size;

        if (colliding)
        {
            sphereTwo.GetComponent<Health>().OnHit();
            //float penetration = segmentOne + segmentTwo - size;

            //float totalInverseMass = (1.0f / sphereOne.GetComponent<Particle3D>().mass) + (1.0f / sphereTwo.GetComponent<Particle3D>().mass);

            //Vector3 normal = midline / size;

            //Vector3 movePerMass = normal * (penetration / totalInverseMass);

            //float restitution = 1.0f;

            //Vector3 vOne = sphereOne.GetComponent<Particle3D>().velocity;
            //Vector3 vTwo = sphereTwo.GetComponent<Particle3D>().velocity;

            //Vector3 separatingVelocity = Vector3.Scale((vOne - vTwo), normal);

            //Vector3 newSeparatingVelocity = -(separatingVelocity) * restitution;

            ////Vector3 contactPoint = sphereOne.transform.position + midline * 0.5f;

            ////Vector3 contact = new Vector3(0.0f, 0.0f, 0.0f) + contactPoint;

            ////Vector3 movementOne = movePerMass * (1 / sphereOne.GetComponent<Particle2D>().mass);
            ////Vector3 movementTwo = movePerMass * (1 / sphereTwo.GetComponent<Particle2D>().mass);

            //Vector3 deltaVelocity = newSeparatingVelocity - separatingVelocity;

            //Vector3 impulseVelocity = deltaVelocity / totalInverseMass;

            //Vector3 velImpulsePerMass = Vector3.Scale(normal, impulseVelocity);

            //Vector3 newVelocityOne = vOne + (velImpulsePerMass / sphereOne.GetComponent<Particle3D>().mass);
            //Vector3 newVelocityTwo = vTwo - (velImpulsePerMass / sphereTwo.GetComponent<Particle3D>().mass);

            ////if (newVelocityOne == newVelocityTwo)
            ////{
            ////    Debug.Break();
            ////}
            ////if (Mathf.Approximately(newVelocityOne.x, newVelocityTwo.x) && Mathf.Approximately(newVelocityOne.y, newVelocityTwo.y) && Mathf.Approximately(newVelocityOne.z, newVelocityTwo.z))
            ////{
            ////    Debug.Break();
            ////}

            ////Debug.Log(newVelocityOne.ToString());
            ////Debug.Log(newVelocityTwo.ToString());



            ///*
            //float inMomentum = Mathf.Abs(sphereOne.GetComponent<Particle3D>().mass * sphereOne.GetComponent<Particle3D>().velocity.sqrMagnitude
            //    + sphereTwo.GetComponent<Particle3D>().mass * sphereTwo.GetComponent<Particle3D>().velocity.sqrMagnitude);
            //float outMomentum = Mathf.Abs(sphereOne.GetComponent<Particle3D>().mass * newVelocityOne.sqrMagnitude
            //    + sphereTwo.GetComponent<Particle3D>().mass * newVelocityTwo.sqrMagnitude);
            //*/

            //sphereOne.GetComponent<Particle3D>().velocity = newVelocityOne;
            //sphereTwo.GetComponent<Particle3D>().velocity = newVelocityTwo;
        }
    }

    static public void collideWall2D(GameObject sphere, GameObject wall)
    {
        Vector2 spherePos = new Vector2(sphere.transform.position.x, sphere.transform.position.y);
        float sphereRad = sphere.transform.localScale.x * sphere.GetComponent<SphereCollider>().radius;
        Vector2 wallDir = new Vector2(wall.transform.up.x, wall.transform.up.y);
        Vector2 O = spherePos * wallDir;
        Vector2 offset = new Vector2(wall.transform.position.x, wall.transform.position.y) * wallDir;

        Vector2 dist = O - offset;
        float centerDist = dist.magnitude;

        bool colliding = (centerDist * centerDist) <= (sphereRad * sphereRad);

        if (colliding)
        {
            Vector2 normal = wallDir;
            float penetration = -centerDist;

            if (centerDist < 0)
            {
                normal *= -1;
                penetration = -penetration;
            }

            penetration += sphereRad;

            //Inverse mass of particle and wall has no mass to consider
            float totalInverseMass = (1 / sphere.GetComponent<Particle2D>().mass) + (0.0f);

            Vector2 movePerMass = normal * (penetration / totalInverseMass);

            float restitution = 1.0f;

            Vector2 vSphere = new Vector2(sphere.GetComponent<Particle2D>().velocity.x, sphere.GetComponent<Particle2D>().velocity.y);

            Vector2 separatingVelocity = vSphere * normal;

            Vector2 newSeparatingVelocity = -(separatingVelocity) * restitution;

            //Vector2 deltaVelocity = separatingVelocity - newSeparatingVelocity;
            Vector2 deltaVelocity = newSeparatingVelocity - separatingVelocity;

            Vector2 impulseVelocity = deltaVelocity / totalInverseMass;

            Vector2 velImpulsePerMass = normal * impulseVelocity;

            Vector2 newVelocity = vSphere + (velImpulsePerMass / sphere.GetComponent<Particle2D>().mass);

            sphere.GetComponent<Particle2D>().velocity = newVelocity;
        }
    }

    static public void collideWall3D(GameObject sphere, GameObject wall)
    {
        Vector3 spherePos = sphere.transform.position;
        float sphereRad = sphere.transform.localScale.x * sphere.GetComponent<SphereCollider>().radius;
        Vector3 wallDir = wall.transform.up;
        Vector3 O = Vector3.Scale(spherePos, wallDir);
        Vector3 offset = Vector3.Scale(wall.transform.position, wallDir);

        Vector3 dist = O - offset;
        float centerDist = dist.magnitude;

        bool colliding = (centerDist * centerDist) <= (sphereRad * sphereRad);

        if (colliding)
        {
            Vector3 normal = wallDir;
            float penetration = -centerDist;

            if (centerDist < 0.0f)
            {
                normal *= -1.0f;
                penetration = -penetration;
            }

            penetration += sphereRad;

            //Inverse mass of particle and wall has no mass to consider
            float totalInverseMass = (1.0f / sphere.GetComponent<Particle3D>().mass) + (0.0f);

            Vector3 movePerMass = normal * (penetration / totalInverseMass);

            float restitution = 1.0f;

            Vector3 vSphere = sphere.GetComponent<Particle3D>().velocity;

            Vector3 separatingVelocity = Vector3.Scale(vSphere, normal);

            Vector3 newSeparatingVelocity = -(separatingVelocity) * restitution;

            //Vector2 deltaVelocity = separatingVelocity - newSeparatingVelocity;
            Vector3 deltaVelocity = newSeparatingVelocity - separatingVelocity;

            Vector3 impulseVelocity = deltaVelocity / totalInverseMass;

            Vector3 velImpulsePerMass = Vector3.Scale(normal, impulseVelocity);

            Vector3 newVelocity = vSphere + (velImpulsePerMass / sphere.GetComponent<Particle3D>().mass);

            sphere.GetComponent<Particle3D>().velocity = newVelocity;
        }
    }

    static public void collideBox2D(GameObject sphere, GameObject box)
    {
        //Using sqDist
        float sqDist = SqDistPointAABB(sphere.transform.position, box);
        float sphereRadius = sphere.transform.localScale.x * sphere.GetComponent<SphereCollider>().radius;
        bool colliding = sqDist <= (sphereRadius * sphereRadius);

        /*
        if (colliding)
        {
            Debug.Log("Colliding");
        }
        */

        //Using ClosestPoint
        Vector3 point = new Vector3(0.0f, 0.0f, 0.0f);
        point = ClosestPtPointAABB(sphere.transform.position, box, point);

        Vector3 v = point - sphere.transform.position;
        bool collision = Vector3.Dot(v, v) <= (sphereRadius * sphereRadius);
        /*
        if (collision)
        {
            Debug.Log("Collision");
        }
        */

        //Do Collision Stuff for Sphere here
        if (colliding || collision)
        {
            //Debug.Log("Barrel Collision");
            GameObject gun;
            gun = GameObject.FindGameObjectWithTag("Gun");
            gun.GetComponent<GunControl>().score += 1;
            GameObject.Destroy(box);
        }
    }

    static public void collideBox3D(GameObject sphere, GameObject box)
    {
        //Using sqDist
        float sqDist = SqDistPointAABB(sphere.transform.position, box);
        float sphereRadius = sphere.transform.localScale.x * sphere.GetComponent<SphereCollider>().radius;
        bool colliding = sqDist <= (sphereRadius * sphereRadius);

        /*
        if (colliding)
        {
            Debug.Log("Colliding");
        }
        */

        //Using ClosestPoint
        Vector3 point = new Vector3(0.0f, 0.0f, 0.0f);
        point = ClosestPtPointAABB(sphere.transform.position, box, point);

        Vector3 v = point - sphere.transform.position;
        bool collision = Vector3.Dot(v, v) <= (sphereRadius * sphereRadius);
        /*
        if (collision)
        {
            Debug.Log("Collision");
        }
        */

        //Do Collision Stuff for Sphere here
        if (colliding || collision)
        {
            box.GetComponent<MeshRenderer>().material.SetColor("_Color", sphere.GetComponent<MeshRenderer>().material.color);

            Vector3 normal = v.normalized;

            float penetration = sphereRadius - sqDist;

            float totalInverseMass = (1.0f / sphere.GetComponent<Particle3D>().mass) + (0.0f);

            Vector3 movePerMass = normal * (penetration / totalInverseMass);

            float restitution = 1.0f;

            Vector3 vSphere = sphere.GetComponent<Particle3D>().velocity;

            Vector3 separatingVelocity = Vector3.Scale(vSphere, normal);

            Vector3 newSeparatingVelocity = -(separatingVelocity) * restitution;

            //Vector2 deltaVelocity = separatingVelocity - newSeparatingVelocity;
            Vector3 deltaVelocity = newSeparatingVelocity - separatingVelocity;

            Vector3 impulseVelocity = deltaVelocity / totalInverseMass;

            Vector3 velImpulsePerMass = Vector3.Scale(normal, impulseVelocity);

            Vector3 newVelocity = vSphere + (velImpulsePerMass / sphere.GetComponent<Particle3D>().mass);

            sphere.GetComponent<Particle3D>().velocity = newVelocity;
        }
    }

    static private Vector3 ClosestPtPointAABB(Vector3 p, GameObject b, Vector3 q)
    {
        Vector3 boxMin = b.transform.position - new Vector3(b.transform.localScale.x / 2.0f, b.transform.localScale.y / 2.0f, b.transform.localScale.z / 2.0f);
        Vector3 boxMax = b.transform.position + new Vector3(b.transform.localScale.x / 2.0f, b.transform.localScale.y / 2.0f, b.transform.localScale.z / 2.0f);
        for (int i = 0; i < 3; i++)
        {
            float v = p[i];
            if (v < boxMin[i])
            {
                v = boxMin[i];
            }
            if (v > boxMax[i])
            {
                v = boxMax[i];
            }
            q[i] = v;
        }
        return q;
    }

    static private float SqDistPointAABB(Vector3 p, GameObject b)
    {
        float sqDist = 0.0f;
        Vector3 boxMin = b.transform.position - new Vector3(b.transform.localScale.x / 2.0f, b.transform.localScale.y / 2.0f, b.transform.localScale.z / 2.0f);
        Vector3 boxMax = b.transform.position + new Vector3(b.transform.localScale.x / 2.0f, b.transform.localScale.y / 2.0f, b.transform.localScale.z / 2.0f);
        for (int i = 0; i < 3; i++)
        {
            float v = p[i];
            if (v < boxMin[i])
            {
                sqDist += (boxMin[i] - v) * (boxMin[i] - v);
            }
            if (v > boxMax[i])
            {
                sqDist += (v - boxMax[i]) * (v - boxMax[i]);
            }
        }
        return sqDist;
    }
}

/*
    public Vector2 midline;
    public float size;

    public float segmentOne;
    public float segmentTwo;

    public bool colliding;
    public float penetration;

    public float massOne = 0.1f;
    public float massTwo = 1.9f;
    public float totalInverseMass;

    public Vector2 normal;

    public Vector2 movePerMass;

    public float restitution = 0.0f;

    public Vector2 velocityOne = new Vector2(0.5f, 1.9f);
    public Vector2 velocityTwo = new Vector2(-1.2f, 0.3f);
    public Vector2 separatingVelocity;
    public Vector2 newSeparatingVelocity;

    public Vector2 contactPoint;
    public Vector2 contact;

    public Vector2 movementOne;
    public Vector2 movementTwo;

    public Vector2 deltaVelocity;
    public Vector2 impulseVelocity;
    public Vector2 velImpulsePerMass;

    public Vector2 newVelocityOne;
    public Vector2 newVelocityTwo;

    public float inMomentum;
    public float outMomentum;
    */

/*
        if (GameObject.FindGameObjectWithTag("bullet") != null)
        {
            sphereTwo = GameObject.FindGameObjectWithTag("bullet");
        }
        else
        {
            sphereTwo = sphereOne;
        }

        midline = sphereOne.transform.position - sphereTwo.transform.position;
        size = Mathf.Abs(midline.sqrMagnitude); //new Vector2(Mathf.Abs(midline.x), Mathf.Abs(midline.y));

        segmentOne = sphereOne.GetComponent<SphereCollider>().radius;
        segmentTwo = sphereTwo.GetComponent<SphereCollider>().radius;

        colliding = testCollision(segmentOne, segmentTwo);

        penetration = segmentOne + segmentTwo - size;

        totalInverseMass = (1 / massOne) + (1 / massTwo);

        normal = midline / size;

        movePerMass = normal * (penetration / totalInverseMass);

        separatingVelocity = (velocityOne - velocityTwo) * normal;

        newSeparatingVelocity = -(separatingVelocity) * restitution;

        contactPoint = new Vector2(sphereOne.transform.position.x, sphereOne.transform.position.y) + midline * 0.5f;

        contact = new Vector2(0.0f, 0.0f) + contactPoint;

        movementOne = movePerMass * (1 / massOne);
        movementTwo = movePerMass * (1 / massTwo);

        deltaVelocity = newSeparatingVelocity - separatingVelocity;

        impulseVelocity = (deltaVelocity) / totalInverseMass;

        velImpulsePerMass = normal * impulseVelocity;

        newVelocityOne = velocityOne + (velImpulsePerMass / massOne);
        newVelocityTwo = velocityTwo + (velImpulsePerMass / massTwo);

        inMomentum = Mathf.Abs(massOne * velocityOne.sqrMagnitude + massTwo * velocityTwo.sqrMagnitude);
        outMomentum = Mathf.Abs(massOne * newVelocityOne.sqrMagnitude + massTwo * newVelocityTwo.sqrMagnitude);
        */
