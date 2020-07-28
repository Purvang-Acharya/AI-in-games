using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWayPoint = 0;

    public float speed = 10.0f;
    public float rotSpeed = 10.0f;
    public float lookAhead = 10.0f;
    GameObject tracker;
    // Start is called before the first frame update
    void Start()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.transform.position = this.transform.position;
        tracker.transform.rotation = this.transform.rotation;
        tracker.GetComponent<MeshRenderer>().enabled = false;

    }

    // Update is called once per frame
    void ProgressTracker()
    {
        if (Vector3.Distance(tracker.transform.position,this.transform.position) > lookAhead)
            return;
        if ((Vector3.Distance(tracker.transform.position, waypoints[currentWayPoint].transform.position)) < 3)
            currentWayPoint++;
        if (currentWayPoint >= waypoints.Length)
            currentWayPoint = 0;
        tracker.transform.LookAt(waypoints[currentWayPoint].transform);
        tracker.transform.Translate(0, 0, (speed + 20)*Time.deltaTime);
    }
    void Update()
    {
        ProgressTracker();

        Quaternion lookAtWP = Quaternion.LookRotation(tracker.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookAtWP, rotSpeed* Time.deltaTime);
        this.transform.Translate(0, 0, speed * Time.deltaTime);
        
    }
}
