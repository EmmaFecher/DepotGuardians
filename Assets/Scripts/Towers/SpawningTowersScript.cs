using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningTowersScript : MonoBehaviour
{
    [SerializeField] PlayerInputHandler input;
    [SerializeField] GameObject playerObject;
    [SerializeField] float range;
    public bool hasATower;
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(hasATower == false)
            {
                input.OpenOptionForBuildingScreen(gameObject);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(hasATower == false)
            {
                input.OpenOptionForBuildingScreen(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            input.CloseOptionForBuildingScreen(gameObject);
        }
        if(hasATower){
            input.CloseOptionForBuildingScreen(gameObject);
        }
    }
    public void SetIfTowerPlaced(bool tIfPlacingFIfRemoving)
    {
        hasATower = tIfPlacingFIfRemoving;
    }
    public bool GetIfTowerPlaced()
    {
        return hasATower;
    }
}
