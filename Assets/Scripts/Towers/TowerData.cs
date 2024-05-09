using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerData : MonoBehaviour
{
    [SerializeField] private int costToBuild;
    public int GetCostTobuild()
    {
        return costToBuild;
    }
}
