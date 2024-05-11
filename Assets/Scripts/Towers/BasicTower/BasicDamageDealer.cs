using UnityEngine;

public class BasicDamageDealer : MonoBehaviour
{
    [SerializeField] float BulletDamage;
    [SerializeField] private float timeBeforeDestroyed;
    private void Update() 
    {
        Destroy(gameObject, timeBeforeDestroyed);
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().DecreaseHealth(BulletDamage);
            Destroy(gameObject);
        }
    }
    
}
