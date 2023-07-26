using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class NewUnityEvent : UnityEvent<object>
{
    public object value;
}

public class HealthController : MonoBehaviour
{
    [SerializeField] int health = 0;
    [SerializeField] Renderer mat;

    [Space(10)]
    [Header("____ EVENTY ____")]
    [SerializeField] OnHealthChanged onHealthChanged;
    [SerializeField] OnHealthChanged onGetDamage;
    [SerializeField] UnityEvent OnDead;

    [System.Serializable]
    class OnHealthChanged : UnityEvent<int> { };
    class OnGetDamage : UnityEvent<int> { };

    private void Start()
    {
        onHealthChanged?.Invoke(health);
    }

    public void GetDamage(int damage)
    {
        health -= damage;

        StartCoroutine(MarkDamage());

        onHealthChanged?.Invoke(health);
        onGetDamage?.Invoke(health);

        if (health <= 0)
            OnDead?.Invoke();
    }

    public void SetHealth(int currHealth)
    {
        health = currHealth;
        onHealthChanged?.Invoke(health);
    }

    public IEnumerator MarkDamage()
    {
        if (mat == null) yield break;

        mat.material.SetColor("_BaseColor", Color.red);
        yield return new WaitForSeconds(0.5f);
        mat.material.SetColor("_BaseColor", Color.white);
    }
}
