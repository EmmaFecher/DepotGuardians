using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    /*
    figure out type of scene - level or not.
        if not, tell inputmanager it is not, and dont do anything.
        if is, tell input manager to find its peices and play the level.
    */
    public bool SceneIsALevelScene()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        return activeSceneName.Contains("Level");
    }
}
