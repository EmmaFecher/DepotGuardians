using System.Collections.Generic;
using UnityEngine;

public class BasicTowerScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject enemySpawner;
    public Transform ballJoint;
    public Transform placeToShootFrom;
    [SerializeField] float range;
    [SerializeField] float yRange;
    [SerializeField] float shootingDelay;
    public float bulletSpeed = 10f;
    GameObject enemy = null;
    float maxLookUpAngle = 60f;
    float maxLookDownAngle = 30f;
    [SerializeField] List<GameObject> enemiesInRange;
    private void Awake() 
    {
        if(enemySpawner == null){
            enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        }
    }
    private void Update()
    {
        CheckIfEnemiesDestroyed();
        AddEnemiesToRange();
        RemoveEnemiesFromRange();
        enemy = FindEnemy();

        if (enemy != null)
        {
            if (!IsInvoking("ShootCoroutine"))
            {
                InvokeRepeating("ShootCoroutine", shootingDelay, shootingDelay);
            }
            KeepPositionZero();
            RotateToEnemy();
            enemy = FindEnemy();
        }
        else//enemy is null
        {
            if (IsInvoking("ShootCoroutine"))
            {
                CancelInvoke("ShootCoroutine");
            }
        }
    }
    private GameObject FindEnemy()
    {
        if(enemiesInRange.Count == 0)
        {
            return null;
        }
        else
        {
            return enemiesInRange[0];
        }
    }
    void CheckIfEnemiesDestroyed()
    {
        enemiesInRange.RemoveAll(item => item == null);
    }
    private void RotateToEnemy()
    {
        Vector3 directionToEnemy = enemy.transform.position - ballJoint.position;
        // Calculate the rotation needed to look at the enemy
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy, Vector3.up);

        //dont change z
        float originalZ = ballJoint.rotation.eulerAngles.z;

        //clamp the changeable ones
        float clampedX = Mathf.Clamp(lookRotation.eulerAngles.x, -maxLookDownAngle, maxLookUpAngle);
        //float clampedY = Mathf.Clamp(lookRotation.eulerAngles.y, -maxLookDownAngle, maxLookUpAngle);

        // Create a new rotation with the updated X component
        Quaternion newRotation = Quaternion.Euler(clampedX, lookRotation.eulerAngles.y, originalZ);
        // Apply the new rotation to the gun's transform
        ballJoint.rotation = newRotation;
    }
    void ShootCoroutine()
    {
        ShootAtEnemy(enemy.transform);
        // Stop shooting if the tower no longer has an enemy in sight
        if (enemy == null)
        {
            CancelInvoke("ShootCoroutine");
        }
    }
    private void KeepPositionZero()
    {
        ballJoint.localPosition = new Vector3(0, 1.16f, 0);
    }
    void ShootAtEnemy(Transform enemyTransform)
    {
        GameObject bullet = Instantiate(bulletPrefab, placeToShootFrom.transform.position, Quaternion.identity);
        Quaternion gunRotation = ballJoint.rotation;
        Vector3 direction = gunRotation * Vector3.forward;
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = direction * bulletSpeed;
    }
    void AddEnemiesToRange()
    {
        List<GameObject> list = enemySpawner.gameObject.GetComponent<EnemySpawner>().spawnedEnemies;
        foreach(GameObject obj in list)
        {
            if(obj != null)
            {
                if (Vector3.Distance(transform.position, obj.transform.position) < range && obj != null)
                {
                    if (!enemiesInRange.Contains(obj))
                    {
                        enemiesInRange.Add(obj);
                    }
                }
            }
        }
    }
    void RemoveEnemiesFromRange()
    {
        List<GameObject> list = enemySpawner.gameObject.GetComponent<EnemySpawner>().spawnedEnemies;
        foreach (GameObject obj in list)
        {
            if(obj != null)
            {
                if (Vector3.Distance(transform.position, obj.transform.position) > range)
                {
                    if (enemiesInRange.Contains(obj))
                    {
                        enemiesInRange.Remove(obj);
                    }
                }
            }
        }
    }
}
