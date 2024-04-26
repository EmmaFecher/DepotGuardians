using UnityEngine;

public class EnemyMeleeDamage : MonoBehaviour
{
    [SerializeField] private int Damage;
    public int GetDamage()
    {
        return Damage;
    }
}
