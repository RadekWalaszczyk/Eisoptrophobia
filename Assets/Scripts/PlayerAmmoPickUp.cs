using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoPickUp : MonoBehaviour
{
    [SerializeField] int AmmoAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.inst.PlaySoundByName("PickUpAmmo");
            PlayerController.inst.AddAmmo(AmmoAmount);
            Destroy(gameObject);
        }
    }
}
