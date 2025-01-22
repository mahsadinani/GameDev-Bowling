using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ui_Handler : MonoBehaviour
{
     
    public void Level1Loader()
    {
        SceneManager.LoadScene("Level 1");
    }

    
    public void Level2Loader()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void exit()
    {
        Application.Quit();
    }

}
