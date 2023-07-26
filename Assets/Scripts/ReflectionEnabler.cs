using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ReflectionEnabler : MonoBehaviour
{
    [SerializeField] List<ReflectionProbe> probes = new List<ReflectionProbe>();
    List<ReflectionProbe> otherProbes = new List<ReflectionProbe>();

    private void Awake()
    {
        otherProbes = FindObjectsOfType<ReflectionProbe>().ToList();
        foreach (var item in otherProbes.ToList())
        {
            if (probes.Contains(item))
            {
                otherProbes.Remove(item);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var probe in probes)
                probe.gameObject.SetActive(true);

            foreach (var probe in otherProbes)
                probe.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var probe in probes)
                probe.gameObject.SetActive(false);

        }
    }
}
