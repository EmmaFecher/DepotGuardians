using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningTowersScript : MonoBehaviour
{
    [SerializeField] PlayerInputHandler input;
    [SerializeField] GameObject playerObject;
    [SerializeField] float range;
    public bool hasATower;
    private void Awake() {
        if(input == null){
            input = GameObject.FindGameObjectWithTag("InputManager").GetComponent<PlayerInputHandler>();
        }
    }
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
            input.CloseOptionForBuildingScreen();
        }
        if(hasATower){
            input.CloseOptionForBuildingScreen();
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
