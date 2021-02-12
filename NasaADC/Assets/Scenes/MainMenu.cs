using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ 
    public void OptionsPage()
    {
        SceneManager.LoadScene(1);
    }

    public void CreditsPage()
    {
        SceneManager.LoadScene(2);
    }

    public void ControlsPage()
    {
        SceneManager.LoadScene(3);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(4);
    }
}
