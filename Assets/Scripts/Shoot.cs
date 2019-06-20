// Code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public GameObject Spawner;
    public float Power;
    public Vector3 directionShoot;

    void Start()
    {
        // on attend 2.0f puis toutes les .5f on invoque la fonction ShootArrow 
        InvokeRepeating("ShootArrow", 2.0f, 0.5f);
    }

    void ShootArrow()
    {
        // on créer le game object, on lui ajoute un force puis on le detruit apres un certain temps
        GameObject g = Instantiate(ProjectilePrefab, Spawner.GetComponent<Transform>().position, Quaternion.identity);
        g.GetComponent<Rigidbody2D>().AddForce(directionShoot * Power);
        Destroy(g, .7f);
    }
}
