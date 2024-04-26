using UnityEngine;

public class GameManager : MonoBehaviour
{
    //This will have the save and load stuff
    public static GameManager Instance;
    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
