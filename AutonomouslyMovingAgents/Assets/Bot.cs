using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    public GameObject target;
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Seek(Vector3 location) 
    {
        agent.SetDestination(location);
    }
    void Flee(Vector3 location)
    {
        Vector3 vectDirection = location - this.transform.position;
        agent.SetDestination(this.transform.position - vectDirection);
    }
    void Update()
    {
        //Seek(target.transform.position);
        Flee(target.transform.position);
    }
}
