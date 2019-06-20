// not code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log : Enemy
{

    public Rigidbody2D myRigidBody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
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
                // on change son etat
                ChangeState(EnemyState.walk);
                // Tant qu'il est dans la range du joueur il n'est pas possible de lancer l'animation sleep
                // donc il est wakeUp 
                anim.SetBool("wakeUp", true);
            }
        } else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            // Sinon lorsque le cible n'est plus dans sa range alors on passe le bool wakeUp en false
            // pour que l'animation de sleep puisse s'effectuer
            anim.SetBool("wakeUp", false);
        }
    }


    // selon la fonction changeAnim definie la valeur de moveX et moveY pour changer l'animation
    public void setAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    public void changeAnim(Vector2 direction)
    {
        // si la valeur absolue de x est superieur a y cela signifie que le gameobject se deplace
        // a l'horizontal
        // Si x est positif alors le game object se deplace vers la droite et inversement 
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                setAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                setAnimFloat(Vector2.left);
            }
        }
        // si la valeur absolue de y est superieur a x cela signifie que le gameoject se deplace
        // a la vertical
        // Si y est positif alors le game object se deplace vers le haut et inversement 
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                setAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                setAnimFloat(Vector2.down);
            }
        }
    }

    // fonction changement d'etat
    public void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }
}
