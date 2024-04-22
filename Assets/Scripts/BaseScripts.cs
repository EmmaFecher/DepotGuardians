using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScripts : MonoBehaviour
{
    [SerializeField] private int Coins;
    [SerializeField] private int Health;
    private void Update() 
    {
        if(Health <= 0)
        {
            //WIP - trigger a lose
            Destroy(gameObject);
        }
    }
    public void SetCoins(int amountToAdd)
    {
        Coins += amountToAdd;
    }
    public void SetHealth(int setAmount)
    {
        Health = setAmount;
    }
    public int GetCoins()
    {
        return Coins;
    }
    public int GetHealth()
    {
        return Health;
    }
    public void DecreaseHealth(int decreaseAmount)
    {
        Health -= decreaseAmount;
    }
    public void IncreaseCoins(int increaseAmount)
    {
        Coins += increaseAmount;
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            DecreaseHealth(other.gameObject.GetComponent<EnemyMeleeDamage>().GetDamage());
            Destroy(other.gameObject);
        } 
    }

}
