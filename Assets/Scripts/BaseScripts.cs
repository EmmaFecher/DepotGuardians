using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScripts : MonoBehaviour
{
    [SerializeField] private int Coins;
    [SerializeField] private int Health;
    [SerializeField] private PlayerInputHandler input;
    private void Update() 
    {
        if(Health <= 0)
        {
            Debug.Log("Base dead, LOOOSE");
            input.GameDone(false);
        }
    }
    public void SetCoins(int setAmount)
    {
        Coins = setAmount;
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
    public void IncreaseHealth(int increaseAmount)
    {
        Health += increaseAmount;
    }
    public void IncreaseCoins(int increaseAmount)
    {
        Coins += increaseAmount;
    }
    public void DecreaseCoins(int decreaseAmount)
    {
        Coins -= decreaseAmount;
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
