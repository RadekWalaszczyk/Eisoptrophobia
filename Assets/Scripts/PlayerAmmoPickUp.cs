using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoPickUp : MonoBehaviour
{
    [SerializeField] int AmmoAmount;
    [SerializeField] bool isKey;
    bool canPickUp = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isKey)
            {
                if (!canPickUp) return;
                canPickUp = false;

                AudioManager.inst.PlaySoundByName("PickUpKey");
                DoorController.inst.AddKey(gameObject);
                gameObject.SetActive(false);
            }
            else
            {
                AudioManager.inst.PlaySoundByName("PickUpAmmo");
                PlayerController.inst.AddAmmo(AmmoAmount);
                Destroy(gameObject);
            }

        }
    }
}
