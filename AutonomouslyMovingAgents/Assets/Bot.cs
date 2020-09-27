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

    void Pursuit()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float relAngle = Vector3.Angle(this.transform.forward, this.transform.TransformVector(target.transform.forward));
        float targetAngle = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDirection));
        if ((targetAngle>90 && relAngle<20)||target.GetComponent<Drive>().currentSpeed < 0.01f)
        {
            Seek(target.transform.position);
            return;
        }
        float lookAhead = targetDirection.magnitude/(agent.speed + target.GetComponent<Drive>().currentSpeed);
        Seek(target.transform.position + target.transform.forward * lookAhead);

    }
    void Evade()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
       /*float relAngle = Vector3.Angle(this.transform.forward, this.transform.TransformVector(target.transform.forward));
        float targetAngle = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDirection));
        if ((targetAngle > 90 && relAngle < 20) || target.GetComponent<Drive>().currentSpeed < 0.01f)
        {
            Seek(target.transform.position);
            return;
        }*/
        float lookAhead = targetDirection.magnitude / (agent.speed + target.GetComponent<Drive>().currentSpeed);
        Flee(target.transform.position + target.transform.forward * lookAhead);
    }
    void Update()
    {
        //Seek(target.transform.position);
        //Flee(target.transform.position);
        //Pursuit();
        Evade();
    }
}
