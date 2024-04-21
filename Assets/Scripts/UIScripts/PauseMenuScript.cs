using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] PlayerInputHandler input;
    [SerializeField] string MenuSceneName;
    [SerializeField] GameObject HUDMenu;
    [SerializeField] GameObject PauseMenu;

    public void Resume()
    {
        Time.timeScale = 1.0f;
        input.UnPause();
        HUDMenu.SetActive(true);
        PauseMenu.SetActive(false);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(MenuSceneName);
    }
}
