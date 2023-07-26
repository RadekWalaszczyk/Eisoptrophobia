using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetShoot()
    {
        anim.SetBool("Shoot", false);
    }

    public void SetReload()
    {
        anim.SetBool("Reload", false);
    }

    public void PlayShoot()
    {
        PlayerController.inst.BlackScreen();
        AudioManager.inst.PlaySoundByName("Shoot", 0.1f);
    }

    public void Suicide()
    {
        Application.Quit();
    }
}
