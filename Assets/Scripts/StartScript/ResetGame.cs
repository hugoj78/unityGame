// Code by us
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public BoolValue GBL1;
    public BoolValue GBL2;
    public BoolValue GBL3;
    public BoolValue GBL4;

    public Vector2 playerPosition;
    public VectorValue playerStorage;

    public float playerHeal;
    public FloatValue playerHealStorage;


    public void ReStart()
    {
        GBL1.RuntimeValue = GBL1.initialValue;
        GBL2.RuntimeValue = GBL2.initialValue;
        GBL3.RuntimeValue = GBL3.initialValue;
        GBL4.RuntimeValue = GBL4.initialValue;

        playerStorage.DefaultValue = playerPosition;

        playerHealStorage.RuntimeValue = playerHeal;

        SceneManager.LoadScene("HomeHouseInterior");
    }
}
