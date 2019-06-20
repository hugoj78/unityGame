// Code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardianDialog : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject question;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;

    public GameObject blocker;
    public BoolValue boolBlocker;

    private void Start()
    {
        // Au lancement verifie si le blocker est actif ou non
        InitBlocker();
    }

    void Update()
    {
        // On verifie si le joueur a debloquer ou non le blocker
        UpdateBlocker();

        // Si le joueur est dans la range du png pr le dialog et appuie sur le bouton adequate
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            // Si le dialog est deja actif on la desactive
            // Sinon on affiache le dialog et on passe le boolblock en vrai pour signaler
            // que le joueur a effectuer la tache requise pr le blocker
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
                boolBlocker.RuntimeValue = true;
            }
        }
    }

    //
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si c'est le gameobject avec le collider 2D avec le tag "Player"
        // alors playerInRange est vraie
        playerInRange |= other.CompareTag("Player");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Si le gameobject avec le tag Player sors du collider trigger
        // Alors PlayerInRange est faux et on desactive le dialog
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }

    public void InitBlocker()
    {
        if(boolBlocker.initialValue)
        {
            // Desactive le blocker dans le jeu
            blocker.SetActive(false);
            // Desactiver le logo "?" au dessus du png 
            question.GetComponent<question>().See();
        }
        else
        {
            blocker.SetActive(true);
        }
    }

    public void UpdateBlocker()
    {
        if (boolBlocker.RuntimeValue)
        {
            // Desactive le blocker dans le jeu
            blocker.SetActive(false);
            // Desactiver le logo "?" au dessus du png 
            question.GetComponent<question>().See();
        }
        else
        {
            blocker.SetActive(true);
        }
    }
}
