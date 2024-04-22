using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float Health;
    [SerializeField] int CoinsToDrop;
    private void Update()
    {
        if(Health <= 0){
            Debug.Log("ADD COINS");
            Destroy(gameObject);
        }
    }
    public void DecreaseHealth(float damage)
    {
        Health -= damage;
    }
}
