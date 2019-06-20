// Code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject EndScene;

    // Update is called once per frame
    void Update()
    {
        // Ici on veut lancer la fonction EndGameScene() quand GameObject est desactiver
        if (!EndScene.activeSelf)
        {
            EndGameScene();
        }
    }

    // On change de scene
    public void EndGameScene()
    {
        SceneManager.LoadSceneAsync("EndScene");
    }

}
