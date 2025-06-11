using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAP : MonoBehaviour
{
    public GameObject mapa;
    public bool MostrandoMapa = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (MostrandoMapa)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Reanudar()
    {
        mapa.SetActive(false);
        Time.timeScale = 1.0f;
        MostrandoMapa = false;
    }

    public void Pausar()
    {
        mapa.SetActive(true);
        Time.timeScale = 0;
        MostrandoMapa = true;
    }

}
