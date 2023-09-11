using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] Animation anim;
    [SerializeField] GameObject tooltip;
    [SerializeField] List<Transform> KeysHole = new List<Transform>();
    [SerializeField] List<GameObject> Keys = new List<GameObject>();
    bool canInteract = false;
    bool doorOpen = false;

    public static DoorController inst;
    private void Awake()
    {
        if (inst != null)
            Destroy(gameObject);
        else
            inst = this;
    }

    public void AddKey(GameObject keyToAdd)
    {
        Keys.Add(keyToAdd);
    }

    private void OnMouseOver()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E) && !doorOpen)
            {
                for (int i = 0; i < Keys.Count; i++)
                {
                    Keys[i].SetActive(true);
                    Keys[i].transform.SetParent(transform);
                    Keys[i].transform.position = KeysHole[i].position;
                    Keys[i].transform.rotation = KeysHole[i].rotation;
                }

                OpenDoor();

                AudioManager.inst.PlaySoundByName("PutKey");
            }

            tooltip.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        tooltip.SetActive(false);
    }

    void OpenDoor()
    {
        if (Keys.Count == 2 && transform.childCount == 4)
        {
            anim.Play();
            doorOpen = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        canInteract = other.CompareTag("Player");
    }
}
