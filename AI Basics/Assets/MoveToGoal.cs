using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToGoal : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2.0f;
    public Transform goal;
    public float accuracy = 0.01f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = goal.position - this.transform.position;
        if(direction.magnitude>accuracy)
        this.transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}
