using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Sc_PlayerController : MonoBehaviour
{
    private Sc_DialogueTrigger cutsceneDialogue;
    private Sc_PlayerInputs inputManager;
    private Sc_PlayerStats playerStats;
    private GameObject aimGunEndPoint;
    private GameObject bulletPrefab;
    public bool needToReload;
    private Vector3 rotationNeeded;
    private Vector3 targetPosition;
    private Vector3 aimDirection;
    [SerializeField] private LayerMask _layerMask;
    private SpriteRenderer reloadButton;
    public Texture2D cursorTextureBase;
    public Texture2D cursorTextureShoot;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private float cursorShooterTime = 0.2f;
    private float cursorShooterTimer = 0f;
    private bool cursorShooting;

    public event EventHandler<ShootingEventArgs> Shooting;

    public class ShootingEventArgs : EventArgs
    {
        public Vector3 gunPointPosition;
        public Vector3 shootPosition;
    }
    
    void Start()
    {
        reloadButton = GameObject.FindGameObjectWithTag("UI").transform.Find("Healthbar+Gun").transform.Find("Reload_Button").GetComponent<SpriteRenderer>();
        inputManager = Sc_PlayerInputs.Instance;
        aimGunEndPoint = GameObject.Find("GunEndPointPosition");
        playerStats = Sc_PlayerStats.StatsInstance;

        reloadButton.enabled = false;
        Cursor.SetCursor(cursorTextureBase, hotSpot, cursorMode);
    }

    
    void Update()
    {
        if (!InCutscene())
        {
            HandleMovement();
            HandleRotation();
            HandleShooting();
            HandleReloading();
            if (cursorShooting)
            {
                cursorShooterTimer -= Time.deltaTime;
                if (cursorShooterTimer <= 0)
                {
                    Cursor.SetCursor(cursorTextureBase, hotSpot, cursorMode);
                    cursorShooterTimer = cursorShooterTime;
                    cursorShooting = false;
                }
            }
        }
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
            if (raycastHit.collider.CompareTag("Untagged"))
            {
                transform.position = targetMoveToPosition;
            }
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
            Cursor.SetCursor(cursorTextureShoot, hotSpot, cursorMode);
            cursorShooting = true;
            cursorShooterTimer = cursorShooterTime;
            if (playerStats.currentAmmoInMagazine > 0)
            {
                playerStats.currentAmmoInMagazine--;
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
                if (playerStats.currentAmmoInMagazine == 0)
                {
                    needToReload = true;
                }
                else
                {
                    needToReload = false;
                }
            }
        }
    }

    private void HandleReloading()
    {
        if (needToReload)
        {
            reloadButton.enabled = true;
            if (inputManager.GetReloading() && playerStats.currentMagazine > 0)
            {
                needToReload = false;
                reloadButton.enabled = false;
                playerStats.currentAmmoInMagazine = playerStats.maxAmmoInMagazine;
                playerStats.currentMagazine--;
                //Play Reloading sound
            }
        }
    }



    private bool InCutscene()
    {
        if (cutsceneDialogue != null)
            return cutsceneDialogue.DialogueActive();
        else
            return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "ConsumableHP")
        {
            Destroy(collision.gameObject);

            if (playerStats.currentHealth < 8)
            {
                playerStats.currentHealth++;
            }
          
        }

        if (collision.gameObject.tag == "ConsumableBullet")
        {
            playerStats.currentMagazine++;
            Destroy(collision.gameObject);
        }


        if (collision.gameObject.tag == "Cutscene")
        {
            cutsceneDialogue = collision.gameObject.GetComponent<Sc_DialogueTrigger>();

            cutsceneDialogue.ActivateDialogue();    
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cutscene")
        {
            cutsceneDialogue = null;
        }
    }
}
