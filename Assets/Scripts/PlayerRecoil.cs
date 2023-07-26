using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecoil : MonoBehaviour
{
    float snapiness;
    float returnSpeed;
    Vector3 targetRotation;
    Vector3 currentRotation;

    public static PlayerRecoil inst;
    private void Awake()
    {
        if (inst != null)
            Destroy(gameObject);
        else
            inst = this;
    }

    private void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snapiness * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void SetRecoil(float recoilX, float recoilY, float recoilZ, float this_snapiness, float this_returnSpeed)
    {
        snapiness = this_snapiness;
        returnSpeed = this_returnSpeed;
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
}
