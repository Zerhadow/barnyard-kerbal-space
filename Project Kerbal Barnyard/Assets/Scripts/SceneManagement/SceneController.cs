using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject mainMenuObj;
    public GameObject optionsMenuObj;
    public string startSong;
    
    public static void LoadScene(string sceneName)
    {
        Time.timeScale = 1;
        Loader.Load(sceneName);
    }

    public void OptionsMenu() {
        optionsMenuObj.SetActive(true);
        mainMenuObj.SetActive(false);
    }

    public void OptionsBackBtn() {
        optionsMenuObj.SetActive(false);
        mainMenuObj.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}