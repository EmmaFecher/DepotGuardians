using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float Health;
    [SerializeField] int CoinsToDrop;
    BaseScripts baseScript;
    private void Awake() {
        baseScript = GameObject.FindGameObjectWithTag("Base").GetComponent<BaseScripts>();
    }

    private void Update()
    {
        if(Health <= 0){
            baseScript.IncreaseCoins(CoinsToDrop);
            Destroy(gameObject);
        }
    }
    public void DecreaseHealth(float damage)
    {
        Health -= damage;
    }
}
