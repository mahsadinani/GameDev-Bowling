using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ui_Handler : MonoBehaviour
{

    public GameObject setting_panel;

    void Start()
    {
        
    }

    void Update()
    {

    }


    public void Play_Butten()
    {
        SceneManager.LoadScene("Level 1");
    }


    public void Setting_Butten()
    {
        setting_panel.SetActive(!setting_panel.activeInHierarchy);
    }

    public void Exit_Buuten()
    {
        Application.Quit();
    }
}
