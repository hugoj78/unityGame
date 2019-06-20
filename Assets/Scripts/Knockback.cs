// not code by us and code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Pour les pots : lance la fonction smash si le joueur attaque dans la zone du trigger
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<pot>().Smash();
        }

        // Si le gameobject a la tag Enemy ou Player
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();

            if (hit != null)
            {
                // Donne un vecteur dans la direction de rigibody2d hit depuis l'attaquant
                Vector2 difference = hit.transform.position - transform.position;
                // normalized permet de rendre realiste le deplacement en diagonal
                // thrust c'est la force
                difference = difference.normalized * thrust;
                // On applique une force sur le rigibody2d pour effectuer le knockback
                hit.AddForce(difference, ForceMode2D.Impulse);

                // si la cible a pour tag Enemy
                if (other.gameObject.CompareTag("Enemy") && other.isTrigger)
                {
                    // on change l'etat de l'enemy en stagger
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    // on appelle la fonction knock pour qu'il prenne les degats
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }

                // si la cible a pour tag Player
                if (other.gameObject.CompareTag("Player"))
                {
                    // si il n'est pas deja en knockback
                    // (sert a eviter que le joueur se fasse continuelle attaquer pendant l'effet knockback
                    if (other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        // on change son etat
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        // On appelle la fonction knock dans playermovement pour q'il prenne les degats
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                }
            }
        }
    }
}
