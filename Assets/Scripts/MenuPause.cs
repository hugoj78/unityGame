// Code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public static bool GameIsPaused;

    public GameObject pauseMenuUI;

    private void Start()
    {
        GameIsPaused = false;
    }

    void Update()
    {
        // Si le joueur appuie sur echap
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si le jeux est deja en pause relance le jeu
            // sinon mets en pause
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    // desactive le menu de pause
    // relance le timescale a 1f pour que le jeu se relance
    // et passe le bool GameIsPaused a faux
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // active le menu de pause
    // timescale a 0f pour que le jeu se mette en pause (figer les enemies, etc...)
    // et passe le bool GameIsPaused a vrai
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
