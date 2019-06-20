// Code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class question : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void See()
    {
        // si fonction See appelé
        // change le bool see qui change l'animation de l'object et lance coroutine breakCo qui desactive l'object apres un certain temps
        anim.SetBool("see", true);

        if (gameObject.activeInHierarchy == true)
        {
            StartCoroutine(breakCO());
        }

    }

    IEnumerator breakCO()
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);

    }
}
