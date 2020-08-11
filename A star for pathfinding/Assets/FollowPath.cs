using UnityEngine;

public class FollowPath : MonoBehaviour {

   
    public GameObject wpManager;
    // Array of waypoints
    GameObject[] wps;
    UnityEngine.AI.NavMeshAgent agent;
    

    // Use this for initialization
    void Start() {

        // Get hold of wpManager and Graph scripts
        wps = wpManager.GetComponent<WPManager>().waypoints;
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        
    }

    public void GoToHeli() {

        // Use the AStar method passing it currentNode and distination
        agent.SetDestination(wps[4].transform.position);
       // g.AStar(currentNode, wps[4]);
        // Reset index
        //currentWP = 0;
    }

    public void GoToRuin() {

        // Use the AStar method passing it currentNode and distination
        agent.SetDestination(wps[0].transform.position);
        //g.AStar(currentNode, wps[0]);
        // Reset index
        //currentWP = 0;
    }

    public void GoBehindHeli() {

        // Use the AStar method passing it currentNode and distination
        agent.SetDestination(wps[11].transform.position);
        //g.AStar(currentNode, wps[11]);
        // Reset index
        //currentWP = 0;
    }

    // Update is called once per frame
    void LateUpdate() {

       

    }
}