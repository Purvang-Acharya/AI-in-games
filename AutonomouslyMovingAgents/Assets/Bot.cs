using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    public GameObject target;
    Vector3 wanderTarget = Vector3.zero;
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
    void Wander()
    {
        float wanderRadius = 10;
        float wanderDistance = 20;
        float wanderJitter = 1;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;
        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);
        Seek(targetWorld);
    }


    void Hide()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        for(int i = 0; i < World.Instance.GetHidingSpots().Length; i++)
        {
            Vector3 hideDir = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePos = World.Instance.GetHidingSpots()[i].transform.position + hideDir.normalized * 5;


            if (Vector3.Distance(this.transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                dist = Vector3.Distance(this.transform.position, hidePos);
            }
        }
        Seek(chosenSpot);
    }

    void CleverHide()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        GameObject chosenGO = World.Instance.GetHidingSpots()[0];
        for (int i = 0; i < World.Instance.GetHidingSpots().Length; i++)
        {
            Vector3 hideDir = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePos = World.Instance.GetHidingSpots()[i].transform.position + hideDir.normalized * 10;


            if (Vector3.Distance(this.transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                chosenDir = hideDir;
                chosenGO = World.Instance.GetHidingSpots()[i];
                dist = Vector3.Distance(this.transform.position, hidePos);
            }
        }

        Collider hideCol = chosenGO.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit info;
        float distance = 100.0f;
        hideCol.Raycast(backRay, out info, distance);
        Seek(info.point + chosenDir.normalized * 2);
    }

    bool CanSeeTarget()
    {
        RaycastHit rayCastInfo;
        Vector3 rayToTarget = target.transform.position - this.transform.position;
        if(Physics.Raycast(this.transform.position,rayToTarget,out rayCastInfo))
        {
            if(rayCastInfo.transform.gameObject.tag == "cop")
            {
                return true;
            }
        }
        return false;
    }

    void Update()
    {
        //Seek(target.transform.position);
        //Flee(target.transform.position);
        //Pursuit();
        //Evade();
        //Wander();
        //Hide();
        if (CanSeeTarget())
            CleverHide();
    }
}
