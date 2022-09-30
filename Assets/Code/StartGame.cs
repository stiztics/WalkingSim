using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartGame : MonoBehaviour
{

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None; 
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Scene1");
    }
}
