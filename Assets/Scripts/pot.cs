// Code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour
{

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // si fonction smash appelé
    // lance animation smash et lance coroutine breakCo qui desactive l'object apres un certain temps
    public void Smash()
    {
        anim.SetBool("smash", true);
        StartCoroutine(breakCO());
    }

    IEnumerator breakCO()
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);

    }
}
