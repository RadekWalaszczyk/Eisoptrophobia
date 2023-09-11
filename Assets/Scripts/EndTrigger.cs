using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.inst.PlaySoundByName("Ending");
        Invoke("BlackScreen", 7f);
    }

    void BlackScreen()
    {
        PlayerController.inst.BlackScreenEnding();
        Invoke("CloseGame", 15f);
    }

    void CloseGame()
    {
        Application.Quit();
    }
}
