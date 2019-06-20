// Code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : MonoBehaviour
{
    public BoolValue Spawner;

    private void Update()
    {
        // Si le BoolValue Spawner est vrai cela signifie que le Heart a été utilise donc on le desactive
        // Pour ne pas se soigner a l'infinie
        if (Spawner.RuntimeValue)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si le gameobject avec le tag Player est dans la portée du collider trigger
        // On verifie que son etat est different de stagger
        // et qu'il n'est pas full vie.
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger
                && !other.GetComponent<PlayerMovement>().currentHealth.RuntimeValue.Equals(10) 
                && !other.GetComponent<PlayerMovement>().currentHealth.RuntimeValue.Equals(11))
            {
                // On le soigne
                StartCoroutine(HealCo(other));
            }
        }
    }

    private IEnumerator HealCo(Collider2D other)
    {
        // On appel la fonction Heal dans PlayerMovement
        other.GetComponent<PlayerMovement>().Heal(2);
        // On desactive l'objet
        this.gameObject.SetActive(false);
        // On passe le BoolValue Spawner en true
        Spawner.RuntimeValue = true;
        yield return null;
    }
}
