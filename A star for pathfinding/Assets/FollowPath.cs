using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal;
    float speed = 5.0f;
    float accuracy = 1.0f;
    float rotSpeed = 2.0f;
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWayPoint = 0;
    Graph g;
    public void GoToHeli()
    {
        g.AStar(currentNode, wps[0]);
        currentWayPoint = 0;
    }
    public void GoToRuin()
    {
        g.AStar(currentNode, wps[12]);
        currentWayPoint = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[1];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if( g.getPathLength() == 0 || currentWayPoint == g.getPathLength() )
            return;
        currentNode = g.getPathPoint(currentWayPoint);
        if (Vector3.Distance(g.getPathPoint(currentWayPoint).transform.position, transform.position) < accuracy)
            currentWayPoint++;
        if(currentWayPoint < g.getPathLength())
        {
            goal = g.getPathPoint(currentWayPoint).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y,goal.position.z);
            Vector3 direction = (lookAtGoal - this.transform.position).normalized;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(lookAtGoal), Time.deltaTime*rotSpeed);
            this.transform.Translate(0, 0, Time.deltaTime * speed);
        }
    }
}
