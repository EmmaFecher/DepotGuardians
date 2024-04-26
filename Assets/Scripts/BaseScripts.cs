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
