// not code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : log
{
public override void CheckDistance()
    {
        // si la distance entre sa target et lui est inferieur ou egale a la distance de poursuite definie
        // et que la distance est superieur a sa portée d'attaque
        // il se depalce vers sa cible
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            // tant que son etat est different de stagger
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                // on le depalce vers sa target avec une certaine vitesse
                Vector3 temp = Vector3.MoveTowards(transform.position,
                    target.position,
                    moveSpeed * Time.deltaTime);

                // On calcul vers quelle direction il va pour changer l'animation
                changeAnim(temp - transform.position);
                myRigidBody.MovePosition(temp);
                // on change l'etat en walk
                ChangeState(EnemyState.walk);
            }
        }

        // si la distance entre sa target et lui est inferieur ou egale a la distance de poursuite definie
        // et que la distance est inferieur ou egale a sa portée d'attaque
        // Alors on lance la coroutine attackCO
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius
          && Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if (currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                StartCoroutine(AttackCO());
            }
        }
    }

    public IEnumerator AttackCO()
    {
        // on change son etat en attack
        currentState = EnemyState.attack;
        // on active l'animation d'attaque
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(1f);
        // puis on rechange son etat
        currentState = EnemyState.walk;
        // on desactive l'animation d'attaques
        anim.SetBool("Attack", false);
    }
}
