using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerBuildingUI : MonoBehaviour
{
    //this is for when the choosing a tower screen is open
    
    // [SerializeField] int basicTowerBuildPrice = 5;
    [SerializeField] PlayerInputHandler input;
    [SerializeField] BaseScripts baseScript;
    [SerializeField] GameObject basicTowerPrefab;
    [SerializeField] GameObject player;
    GameObject towerToBuildAt;

    public void CloseScreen()//button
    {
        input.UnPause();
    }
    public void BuildTower(GameObject tower)//button
    {
        int towerbuildPrice = tower.GetComponent<TowerData>().GetCostTobuild();
        input.GetCurrentPlatform();
        if(towerToBuildAt != null)
        {
            if(Vector3.Distance(towerToBuildAt.transform.position, player.transform.position) < 4)
            {
                if(towerToBuildAt.GetComponent<SpawningTowersScript>().GetIfTowerPlaced() == false)
                {
                    if(baseScript.GetCoins() >= towerbuildPrice)
                        {
                            baseScript.DecreaseCoins(towerbuildPrice);
                            Instantiate(tower, towerToBuildAt.transform.position, towerToBuildAt.transform.rotation);
                            towerToBuildAt.GetComponent<SpawningTowersScript>().SetIfTowerPlaced(true);
                            input.UnPause();
                        }
                }
            }
        }
    }
    public void SetTower(GameObject currentTower)
    {
        towerToBuildAt = currentTower;
    }
}