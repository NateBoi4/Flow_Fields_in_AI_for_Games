using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int mHealth;

    private void Start()
    {
        mHealth = 100;
    }

    private void Update()
    {
        if (this.gameObject.tag != "Player")
        {
            if (mHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if(mHealth <= 0)
            {
                Time.timeScale = 0;
            }
        }
    }

    public void OnHit()
    {
        mHealth -= 50;
        //Debug.Log("Hit");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && this.gameObject.tag != "Player")
        {
            collision.gameObject.GetComponent<Health>().mHealth -= 10;
            //Debug.Log("Hurt");
        }
    }
}
