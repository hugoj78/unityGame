// not code by us and code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// different etat du joueur
public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;

    public VectorValue startingPosition;

    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.position = startingPosition.DefaultValue;
    }

    // Code by us 
    void Update()
    {
        // si le joueur appuie sur LeftShift augmente vitesse du joueur
        if (Input.GetKey(KeyCode.LeftShift) && currentState != PlayerState.attack
            && currentState != PlayerState.stagger)
        {
            speed = 8;
        }
        else
        {
            speed = 4;
        }

        // definie l'axe de deplacement du joueur
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        // si le joueur appuie sur le bouton attack (ici "x" definie dans project setting--> Input)
        // et qu'il n'est pas deja en train d'attaquer et qu'il n'est pas sous l'effet du knock back
        // on lance la coroutine AttackCo()
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack 
            && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        // si le joueur est dans l'etat idle ou walk on lance la focntion UpdateAnimationAndMove()
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        // lance l'animation d'attaque
        animator.SetBool("attacking", true);
        // change son etat
        currentState = PlayerState.attack;
        yield return null;
        // desactive l'animation d'attaque
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        // rechange d'etat apres un certain temps
        currentState = PlayerState.walk;
    }

    // selon l'axe de deplacement definie l'animation approprier au deplacement
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    // fonction permetant de rentre realiste le movement du player et definie sa vitesse
    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime);
    }

    // fonction knock
    public void Knock(float knockTime, float damage)
    {
        // descend la vie du joueur selon l'attaque de l'enemy
        currentHealth.RuntimeValue -= damage;
        // on update l'affichage des coeurs
        playerHealthSignal.Raise();

        // si le joueur ne meurts pas
        if (currentHealth.RuntimeValue > 0)
        {
            // lance coroutine knockCo
            StartCoroutine(KnockCo(knockTime));
        }
        // Sinon desactive le joueur et on passe a la scene de game over
        else
        {
            this.gameObject.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }
    }
    // not code by us
    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidbody != null)
        {
            // Apres le knockback, doit s'arreter et change d'etat
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    // Code by us
    public void Heal(float heal)
    {
        // fonction de heal
        currentHealth.RuntimeValue += heal;
        // un coeur permet de se heal de 2, or si le player a 9 en vie il se retrouve a 11 alors
        // Que sa vie max doit etre de 10 donc on change sa vie a 10 dans le cas ou il se retrouve
        // a 10 hp
        if (currentHealth.RuntimeValue == 11)
        {
            currentHealth.RuntimeValue = 10;
        }
        // on update l'affichage des coeurs
        playerHealthSignal.Raise();
    }
}
