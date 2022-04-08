using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuIngame : MonoBehaviour
{
    public void Options()
    {
        SceneManager.LoadScene("Option Menu");
    }
}
