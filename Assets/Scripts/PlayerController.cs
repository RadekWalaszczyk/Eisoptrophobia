using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMovement PlayerMovement;
    [SerializeField] GameObject blackScreen;
    [SerializeField] GameObject blackScreenEnding;
    public HealthController Health;

    public static PlayerController inst;
    private void Awake()
    {
        if (inst != null)
            Destroy(gameObject);
        else
            inst = this;
    }

    public void AddAmmo(int ammoAmount)
    {
        PlayerMovement.AddAmmo(ammoAmount);
    }

    public void BlackScreen()
    {
        blackScreen.SetActive(true);
    }

    public void BlackScreenEnding()
    {
        blackScreenEnding.SetActive(true);
    }
}
