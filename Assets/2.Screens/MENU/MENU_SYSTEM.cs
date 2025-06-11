using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MENU_SYSTEM : MonoBehaviour
{
    public void PRINCIPAL()
    {
        SceneManager.LoadScene(0);

    }

    public void CREDITOS()
    {
        SceneManager.LoadScene(1);

    }

    public void Jugar()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(2);

    }

    public void LORE()
    {
        SceneManager.LoadScene(3);

    }

    public void Exit()
    {

        Application.Quit();
    }

}
