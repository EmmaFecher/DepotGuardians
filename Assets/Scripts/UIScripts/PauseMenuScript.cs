using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] PlayerInputHandler input;
    [SerializeField] string MenuSceneName;

    public void Resume()
    {
        input.UnPause();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(MenuSceneName);
    }
}
