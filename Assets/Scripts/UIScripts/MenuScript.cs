using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    [SerializeField] string LevelName;
    [SerializeField] GameObject settingsMenu;
    public void StartGame()
    {
        SceneManager.LoadScene(LevelName);
    }
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }
    public void Exit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
