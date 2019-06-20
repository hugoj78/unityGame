// Code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;

    void Update()
    {
        // Si le joueur est dans la range du png pr le dialog et appuie sur le bouton adequate
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            // Si le dialog est deja actif on la desactive
            // Sinon on affiache le dialog
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            } else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

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
}
