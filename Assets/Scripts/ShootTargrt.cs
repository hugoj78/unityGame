// Code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTargrt : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public GameObject Spawner;
    public float Power;
    public Transform Target;
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        // on attend 2.0f puis toutes les 3f on invoque la fonction ShootArrow 
        InvokeRepeating("ShootArrow", 2.0f, 3f);
    }

    void ShootArrow()
    {
        ShootAnim();
        // on créer le game object, on lui ajoute un force puis on le detruit apres un certain temps
        Vector2 difference = Target.transform.position - transform.position;
        GameObject g = Instantiate(ProjectilePrefab, Spawner.GetComponent<Transform>().position, Quaternion.identity);
        g.GetComponent<Rigidbody2D>().AddForce(difference.normalized * Power);
        Destroy(g, 2f);
    }

    void ShootAnim()
    {
        StartCoroutine(ShootCo());
        
    }

    public IEnumerator ShootCo()
    {
        anim.SetBool("Fire", true);
        yield return new WaitForSeconds(.05f);
        anim.SetBool("Fire", false);
    }
}
