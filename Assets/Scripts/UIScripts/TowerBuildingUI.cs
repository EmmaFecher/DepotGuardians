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
        //when it gets clicked on/run
        //Instansiate BasicTowerprefab at 
        if(Vector3.Distance(towerToBuildAt.transform.position, player.transform.position) < 3)
        {
            Instantiate(basicTowerPrefab, towerToBuildAt.transform.position, towerToBuildAt.transform.rotation);
        }
        input.UnPause();
        
    }
    // WIP
    /*
    Buttons for choosing tower
        Click button -> spawn tower -> CloseScreen();
    */
    public void SetTower(GameObject currentTower)
    {
        towerToBuildAt = currentTower;
    }
}