using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sc_PlayerController : MonoBehaviour
{
    private Sc_PlayerInputs inputManager;
    private Sc_PlayerStats playerStats;
    private GameObject aimGunEndPoint;
    private GameObject bulletPrefab;
    private bool needToReload;
    private Vector3 rotationNeeded;
    private Vector3 targetPosition;
    private Vector3 aimDirection;
    [SerializeField] private LayerMask _layerMask;

    public event EventHandler<ShootingEventArgs> Shooting;

    public class ShootingEventArgs : EventArgs
    {
        public Vector3 gunPointPosition;
        public Vector3 shootPosition;
    }
    
    void Start()
    {
        inputManager = Sc_PlayerInputs.Instance;
        aimGunEndPoint = GameObject.Find("GunEndPointPosition");
        playerStats = Sc_PlayerStats.StatsInstance;
    }

    
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleShooting();
        HandleReloading();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = new Vector3(0, 0);
        if (inputManager.GetMovingUp()) 
            moveDirection.y = +1;
        if (inputManager.GetMovingDown()) 
            moveDirection.y = -1;
        if (inputManager.GetMovingLeft()) 
            moveDirection.x = -1;
        if (inputManager.GetMovingRight()) 
            moveDirection.x = +1;
        moveDirection.Normalize();

        Vector3 targetMoveToPosition = transform.position + moveDirection * playerStats.walkSpeed * Time.deltaTime;
        RaycastHit2D raycastHit = Physics2D.CircleCast(transform.position, .3f, transform.forward, 0f);
        //RaycastHit2D raycastHit = Physics2D.Raycast(transform.position + moveDirection * .25f, moveDir, Vector3.Distance(transform.position, targetMoveToPosition));
        if (raycastHit.collider != null) 
        {
        } 
        else
        {
            transform.position = targetMoveToPosition;
        }
    }
    private void HandleRotation()
    {
        Vector3 mousePosition = inputManager.GetMousePos();

        aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        rotationNeeded = new Vector3(0, 0, angle);
        transform.eulerAngles = rotationNeeded;
    }

    private void HandleShooting()
    {
        if (inputManager.FireArmLeft())
        {
            if (playerStats.maxAmmoInMagazine <= -110)
            {
                needToReload = true;
            }
            else
            {
                needToReload = false;
                playerStats.maxAmmoInMagazine--;
                RaycastHit2D raycastHit2D = Physics2D.Raycast(aimGunEndPoint.transform.position, aimDirection , Vector3.Distance(aimGunEndPoint.transform.position, inputManager.GetMousePos()), _layerMask);
                if (raycastHit2D.collider == null)
                {
                    targetPosition = inputManager.GetMousePos();
                }
                else
                {
                    targetPosition = raycastHit2D.point;
                }
                if (Shooting != null)
                    Shooting(this,
                        new ShootingEventArgs
                        {
                            gunPointPosition = aimGunEndPoint.transform.position,
                            shootPosition = targetPosition,
                        });
                
            }
        }
    }

    private void HandleReloading()
    {
        if (needToReload)
        {
            if (inputManager.GetReloading())
            {
                //Show R in Screen
                //Reset Mag to 10
                //Remove one total mag from inv
                //Play Reloading sound
            }
        }
    }
}
