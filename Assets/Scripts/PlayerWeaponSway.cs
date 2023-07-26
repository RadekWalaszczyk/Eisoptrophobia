using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSway : MonoBehaviour
{
    [SerializeField] float smooth;
    [SerializeField] float swayMultiplayer;
    [SerializeField] Transform weaponToSway;

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplayer;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplayer;

        Quaternion rotationX = Quaternion.AngleAxis(mouseY, Vector3.left);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);
        Quaternion targetRotation = rotationX * rotationY;

        weaponToSway.localRotation = Quaternion.Slerp(weaponToSway.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}