using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingHealth : MonoBehaviour
{
    public float health = 25f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //Destroy(gameObject);
            return true;
        }
        return false;
    }
}
