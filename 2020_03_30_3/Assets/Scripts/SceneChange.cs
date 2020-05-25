using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public string sceneName = "CharacterSelection";
    public string sceneName1 = "Game";
    public string sceneName2 = "Start";

    public void ClickStart()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ClickPlay()
    {
        SceneManager.LoadScene(sceneName1);
    }

    public void ClickBack()
    {
        SceneManager.LoadScene(sceneName2);
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
