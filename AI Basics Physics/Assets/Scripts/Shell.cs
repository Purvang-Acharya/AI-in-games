using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;
    float mass = 10;
    float force = 100;
    float acceleration;
    float speedZ;
    float speedY;
    

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        acceleration = force / mass;
        speedZ = speedZ + acceleration * Time.deltaTime;
        speedY = speedY + 0.0981f * Time.deltaTime;
        this.transform.Translate(0, -speedY, speedZ);
        force = 0;
    }
}
