using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Integrator
{
    static public void integrate2D(GameObject sphere)
    {
        if(sphere.GetComponent<Particle2D>().inverseMass <= 0.0f) //Make sure game object does not have infinite mass
        {
            return;
        }

        //Update position based on velocity
        sphere.transform.position += sphere.GetComponent<Particle2D>().velocity * Time.deltaTime;

        //Work out the acceleration from the accumulated forces
        Vector3 resultingAcceleration = sphere.GetComponent<Particle2D>().acceleration;
        resultingAcceleration += sphere.GetComponent<Particle2D>().accumulatedForces * sphere.GetComponent<Particle2D>().inverseMass;

        //Update velocity based on acceleration
        sphere.GetComponent<Particle2D>().velocity += resultingAcceleration * (2*Time.deltaTime) * 0.5f;

        //Update velocity based on damping constant
        sphere.GetComponent<Particle2D>().velocity *= Mathf.Pow(sphere.GetComponent<Particle2D>().dampeningConstant, Time.deltaTime);

        //Clear the Forces
        sphere.GetComponent<Particle2D>().clearAccumulator();
    }

    static public void integrate3D(GameObject sphere)
    {
        if (sphere.GetComponent<Particle3D>().inverseMass <= 0.0f) //Make sure game object does not have infinite mass
        {
            return;
        }

        //Update position based on velocity
        sphere.transform.position += sphere.GetComponent<Particle3D>().velocity * Time.deltaTime;

        //Work out the acceleration from the accumulated forces
        Vector3 resultingAcceleration = sphere.GetComponent<Particle3D>().acceleration;
        resultingAcceleration += sphere.GetComponent<Particle3D>().accumulatedForces * sphere.GetComponent<Particle3D>().inverseMass;

        //Update velocity based on acceleration
        sphere.GetComponent<Particle3D>().velocity += resultingAcceleration * (2.0f * Time.deltaTime) * 0.5f;

        //Update velocity based on damping constant
        sphere.GetComponent<Particle3D>().velocity *= Mathf.Pow(sphere.GetComponent<Particle3D>().dampeningConstant, Time.deltaTime);

        //Clear the Forces
        sphere.GetComponent<Particle3D>().clearAccumulator();
    }
}
