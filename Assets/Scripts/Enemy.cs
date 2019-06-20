// Code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// different etat de Enemy
// idle : moment inactif
// walk : quand il marche
// attack : quand il attaque
// stagger : lorsqu'il est sous l'effet du knockback
public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{

    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;


    // On definie la vie de l'enemie
    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    // Fonction Damage
    private void TakeDamage(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Fonction qui permet d'appeler le coroutine pour le knocback et la fonction damage
    // Pour simplifier : on associe le knockback et les degats pour ne pas avoir plusieur scripts
    public void Knock(Rigidbody2D myRigidBody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidBody, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidBody,float knockTime)
    {
        if (myRigidBody != null)
        {
            // Apres le knockback, doit s'arreter et change d'etat
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }
}
