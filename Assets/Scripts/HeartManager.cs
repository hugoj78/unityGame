// not code by us and code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    private void Update()
    {
        UpdateHearts();
    }

    // Initialise le nombre de Container de coeur
    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.initialValue; i ++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    // Verifie si le container doit etre plein, a motié plein ou vide selon la vie du joueur
    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHealth.RuntimeValue / 2;

        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            if (i <= tempHealth-1)
            {
                // Full Heart
                hearts[i].sprite = fullHeart;
            }
            else if (i >= tempHealth)
            {
                // emptyHeart
                hearts[i].sprite = emptyHeart;
            } else
            {
                // half full heart
                hearts[i].sprite = halfFullHeart;
            }
        }
    }
}
