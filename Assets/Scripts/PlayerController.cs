
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public static PlayerController Instance;
    public Camera playerCamera;

    [Header ("General")]
    public float gravityScale = -20f;

    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    [Header("Rotation")]
    public float rotationSensibility = 10f;

    [Header("Jump")]
    public float jumpHeight = 1.9f;

    [Header("Waepons")]
    public GameObject bullet;
    public Transform firePoint;

    public GunController activeGun;
    public List<GunController> allGuns = new List<GunController>();
    public List<GunController> unlockableGuns = new List<GunController>();
    public int currentGun;

    public Animator anim;
    private float cameraVerticalAngle;
    Vector3 moveInput = Vector3.zero;
    Vector3 rotationInput = Vector3.zero;
    
    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Instance = this;
    }
    private void Start()
    {
        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);
    }
    private void Update()
    {
        Move();
        Look();
        Shoot();
    }

    private void Move()
    {
        if (characterController.isGrounded)
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);
            if (Input.GetButton("Sprint"))
            {
                moveInput = transform.TransformDirection(moveInput) * runSpeed;
            }
            else
            {
                moveInput = transform.TransformDirection(moveInput) * walkSpeed;
            }

            if (Input.GetButtonDown("Jump"))
            {
                moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
            }
        }

        moveInput.y += gravityScale * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);

        anim.SetFloat("walkSpeed", moveInput.magnitude);

    }

    private void Look()
    {
        rotationInput.x = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse Y") * rotationSensibility * Time.deltaTime;

        cameraVerticalAngle += rotationInput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);

        transform.Rotate(Vector3.up * rotationInput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0f, 0f);
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
        {
            RaycastHit hit;
            if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 50f))
            {
                if (Vector3.Distance(playerCamera.transform.position, hit.point) > 2f)
                {
                    firePoint.LookAt(hit.point);
                }
            }
            else
            {
                firePoint.LookAt(playerCamera.transform.position + (playerCamera.transform.forward * 30f));
            }
            FireShot();
        }
        if(Input.GetMouseButton(0) && activeGun.canAutoFire)
        {
            if(activeGun.fireCounter <= 0)
            {
                FireShot();
            }
        }

        if(Input.GetButtonDown("Switch Gun"))
        {
            SwitchGun();
        }
    }

    public void FireShot()
    {
        if (activeGun.currentAmmo > 0)
        {
            activeGun.currentAmmo--;
            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);
            activeGun.fireCounter = activeGun.fireRate;
        }
    }

    public void SwitchGun()
    {
        activeGun.gameObject.SetActive(false);
        currentGun++;
        if(currentGun >= allGuns.Count)
        {
            currentGun = 0;
        }
        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);
    }

    public void AddGun(string gunToAdd)
    {
        bool gunUnlocked = false;
        if(unlockableGuns.Count > 0)
        {
            for(int i = 0; i < unlockableGuns.Count; i++)
            {
                gunUnlocked= true;
                allGuns.Add(unlockableGuns[i]);
                unlockableGuns.RemoveAt(i);
                i = unlockableGuns.Count;
            }
        }
        if(gunUnlocked)
        {
            currentGun = allGuns.Count - 2;
            SwitchGun();
        }
    }
}
