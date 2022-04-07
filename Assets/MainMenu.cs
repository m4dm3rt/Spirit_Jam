using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        SceneManager.LoadScene("1 Level");
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Options ()
    {
        SceneManager.LoadScene("Option Menu");
    }

    public void Main ()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
