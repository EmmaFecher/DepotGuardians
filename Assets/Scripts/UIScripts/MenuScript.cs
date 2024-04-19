using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    [SerializeField] string LevelName;
    public void StartGame()
    {
        SceneManager.LoadScene(LevelName);
    }
    public void Exit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
