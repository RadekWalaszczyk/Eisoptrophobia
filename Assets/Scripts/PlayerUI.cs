using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Health;
    [SerializeField] TextMeshProUGUI Ammo;
    [SerializeField] Material Sanity;

    public void UpdateHealth(int currentHealht, int maxHealth)
    {
        float currHealth = Mathf.InverseLerp(0, maxHealth, currentHealht) * 100f;
        var sanityRadius = Mathf.InverseLerp(0, maxHealth, currentHealht);
        Sanity.SetFloat("_VignetteRadius", Mathf.Clamp01(1 - sanityRadius + 0.2f));

        if (currentHealht <= 0)
            Dead();

        Health.text = $"{currHealth.ToString("00")}%";
    }

    public void UpdateAmmo(int currentAmmo, int ammo)
    {
        Ammo.text = $"{currentAmmo}/{ammo}";
    }

    public void Dead()
    {
        Sanity.SetFloat("_VignetteRadius", 0f);
    }
}
