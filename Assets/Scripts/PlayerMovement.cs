using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("--- MOVEMENT ---")]
    [SerializeField] Camera playerCamera;
    [SerializeField] float walkSpeed = 6f;
    [SerializeField] float runSpeed = 12f;
    [SerializeField] float jumpPower = 7f;
    [SerializeField] float gravity = 10f;

    [SerializeField] float lookSpeed = 2f;
    [SerializeField] float lookXLimit = 45f;
    [SerializeField] AudioSource TupTup;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [SerializeField] bool canMove = true;

    CharacterController characterController;

    [Space(5)]
    [Header("--- WEAPONS ---")]
    [SerializeField] Animator anim;
    public int ammo = 6;
    [SerializeField] int currentAmmo = 6;
    [SerializeField] ParticleSystem MuzzleFlesh;
    [SerializeField] OnAmmoChanged onAmmoChanged;
    [System.Serializable]
    class OnAmmoChanged : UnityEvent<int, int> { };

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        onAmmoChanged?.Invoke(currentAmmo, ammo);
    }

    void Update()
    {
        if (dead) return;

        Movment();
        Shooting();
        Reload();
    }

    void Movment()
    {
        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Mathf.Abs(curSpeedX) > 0 || Mathf.Abs(curSpeedY) > 0)
            TupTup.enabled = true;
        else
            TupTup.enabled = false;

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            if (moveDirection.y > 0)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
            else
            {
                moveDirection.y -= gravity * Time.deltaTime * 1.2f;
            }

        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion
    }


    bool holdRMB = false;
    void Shooting()
    {
        if (Input.GetButtonDown("Fire2"))
            holdRMB = true;

        if (Input.GetButtonUp("Fire2"))
            holdRMB = false;

        if (!holdRMB && !anim.GetBool("Shoot") && !anim.GetBool("Reload") && anim.GetBool("Zoom"))
        {
            anim.SetBool("Zoom", false);
        }

        if (holdRMB && !anim.GetBool("Shoot") && !anim.GetBool("Reload") && !anim.GetBool("Zoom"))
        {
            anim.SetBool("Zoom", true);
        }

        if (Input.GetButtonDown("Fire1") && !anim.GetBool("Shoot") && !anim.GetBool("Reload"))
        {
            if (currentAmmo > 0)
            {
                MuzzleFlash();
                AudioManager.inst.PlaySoundByName("Shoot", 0.1f);
                anim.SetBool("Shoot", true);

                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                RaycastHit hit;

                currentAmmo -= 1;
                PlayerRecoil.inst.SetRecoil(-15f, 15f, 2f, 6f, 2f);

                if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Enemy", "Default"), QueryTriggerInteraction.Ignore))
                {
                    var health = hit.collider.gameObject.GetComponent<HealthController>();
                    if (health != null)
                        health.GetDamage(1);
                }
            }
            else
            {
                AudioManager.inst.PlaySoundByName("Lock");
                anim.SetTrigger("ShootEmpty");
            }

            onAmmoChanged?.Invoke(currentAmmo, ammo);
        }
    }

    public void AddAmmo(int ammoAmount)
    {
        ammo += ammoAmount;
        onAmmoChanged?.Invoke(currentAmmo, ammo);
    }

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !anim.GetBool("Shoot") && !anim.GetBool("Reload") && !anim.GetBool("Zoom"))
        {
            if (ammo > 0 && currentAmmo < 6)
            {
                AudioManager.inst.PlaySoundByName("Reload", 0.1f);
                anim.SetTrigger("Reload");
                int ammoHold = Mathf.Clamp(ammo, 0, 6 - currentAmmo);
                currentAmmo += ammoHold;
                ammo -= ammoHold;
            }
            else
            {
                AudioManager.inst.PlaySoundByName("Lock");
            }
            onAmmoChanged?.Invoke(currentAmmo, ammo);
        }
    }

    bool dead = false;
    public void Dead()
    {
        if (dead) return;
        dead = true;
        anim.SetTrigger("Suicide");
    }

    public void MuzzleFlash()
    {
        MuzzleFlesh.Play();
    }
}