using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] bool isInstantTurnOn;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;
    EnemySpawner enemySpawener;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Awake()
    {
        enemySpawener = FindObjectOfType<EnemySpawner>();
    }
    void Start()
    {
        waveConfig = enemySpawener.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
        StartCorrectOrientation();
    }
    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (waypointIndex < waypoints.Count)//if not at the end
        {
            //movement
            Vector3 targetPos = waypoints[waypointIndex].position + new Vector3(0, 1, 0);//here's the target
            float delta = speed * Time.deltaTime;//here's the distance/speed
            transform.position = Vector3.MoveTowards(transform.position, targetPos, delta);


            //rotation
            Vector3 direction = targetPos - transform.position;
            float turnDelta = turnSpeed * Time.deltaTime;
            if (direction.magnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                if (isInstantTurnOn)
                {
                    transform.rotation = targetRotation;//instant Turn
                }
                else
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnDelta);//Gradual turn
                }
            }
            if (transform.position == targetPos)//Update what the next point is
            {
                waypointIndex++;
            }
        }
        else//if last point reached
        {
            Destroy(gameObject);
        }
    }
    //They are flipped upside down without this... oops
    void StartCorrectOrientation()
    {
        if (transform.rotation.eulerAngles.z != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Adjust rotation to be upright
        }
    }

}