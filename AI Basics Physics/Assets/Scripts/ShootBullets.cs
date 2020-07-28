using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullets : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shellPrefab;
    public GameObject shellSpawnPos;
    public GameObject parentTank;
    public GameObject targetTank;
    float speed = 15;
    float turnSpeed = 2;
    bool canShoot = true;
    void Start()
    {
        
    }
    void CanShootAgain()
    {
        canShoot = true;
    }
    void Fire()
    {
        if (canShoot)
        {
            GameObject shell = Instantiate(shellPrefab, shellSpawnPos.transform.position, shellSpawnPos.transform.rotation);
            shell.GetComponent<Rigidbody>().velocity = speed * this.transform.forward;
            canShoot = false;
            Invoke("CanShootAgain", 0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {       
        
        Vector3 targetDirection = (targetTank.transform.position - parentTank.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(targetDirection.x,0,targetDirection.z));
        //turns Tank towards the target 
        parentTank.transform.rotation = Quaternion.Slerp(parentTank.transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        float? turretAngle = RotateTurret();
        if(turretAngle!=null && Vector3.Angle(targetDirection,parentTank.transform.forward) < 10 )
        {
            Fire();
        }
    }
    //rotates tank turrent up and down depending on target distance
    float? RotateTurret()
    {
        float? turretAngle = CalculateAngle(true);
        if (turretAngle != null)
        {
            this.transform.localEulerAngles = new Vector3(360f - (float)turretAngle, 0f, 0f);
        }
        return turretAngle;
    }
    //returns angle for the rotation of turret
    float? CalculateAngle(bool low)
    {
        Vector3 targetDirection = targetTank.transform.position - this.transform.position;
        float y = targetDirection.y;
        targetDirection.y = 0f;
        float x = targetDirection.magnitude;
        float gravity = 9.81f;
        float speedSquared = speed * speed;
        float underTheSquareRoot = ((speedSquared * speedSquared) - gravity * (gravity * x * x + 2 * y * speedSquared));

        if (underTheSquareRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSquareRoot);
            float highAngle = speedSquared + root;
            float lowAngle = speedSquared - root;

            if (low)
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            else
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
        }
        else
            return null;
    }
}
