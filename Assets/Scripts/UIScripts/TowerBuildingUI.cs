using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerBuildingUI : MonoBehaviour
{
    //this is for when the choosing a tower screen is open
    [SerializeField] PlayerInputHandler input;
    [SerializeField] GameObject basicTowerPrefab;
    [SerializeField] GameObject player;
    GameObject towerToBuildAt;
    public void CloseScreen()//button
    {
        input.UnPause();
        
    }
    public void BuildBasicTower()//button
    {
        input.GetCurrentPlatform();
        if(towerToBuildAt != null)
        {
            if(Vector3.Distance(towerToBuildAt.transform.position, player.transform.position) < 4)
            {
                Instantiate(basicTowerPrefab, towerToBuildAt.transform.position, towerToBuildAt.transform.rotation);
            }
        }
        
        input.UnPause();
        
    }
    public void SetTower(GameObject currentTower)
    {
        towerToBuildAt = currentTower;
    }
}