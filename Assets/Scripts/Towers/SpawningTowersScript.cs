using UnityEngine;

public class SpawningTowersScript : MonoBehaviour
{
    [SerializeField] PlayerInputHandler input;
    [SerializeField] GameObject playerObject;
    private bool ifPlayerHasBuildAbility;
    private void Update() 
    {
        ifPlayerHasBuildAbility = CheckIfPlayerCanBuild();
        if(ifPlayerHasBuildAbility && Time.timeScale == 1.0f)
        {
            input.OpenOptionForBuildingScreen(gameObject);
        }
        else
        {
            input.CloseOptionForBuildingScreen(gameObject);
        }
    }
    bool CheckIfPlayerCanBuild()
    {
        if (Vector3.Distance(playerObject.transform.position, transform.position) < 4)
        {
            return true;
        }
       return false; 
    }
}
