using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] PlayerInputHandler input;
    [SerializeField] string MenuSceneName;
    private void Awake() {
        if(input == null){
            input = GameObject.FindGameObjectWithTag("InputManager").GetComponent<PlayerInputHandler>();
        }
    }
    public void Resume()
    {
        input.UnPause();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(MenuSceneName);
    }
}
