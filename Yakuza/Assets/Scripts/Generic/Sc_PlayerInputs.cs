using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PlayerInputs : MonoBehaviour
{
    public Camera mainCamera;
    private bool lockPlayer;
    private bool lockRightArm;
    private bool lockLeftArm;
    private bool cursorVisibility;
    private static Sc_PlayerInputs _instance;

    public static Sc_PlayerInputs Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        //Cursor.visible = false;
    }

    public bool GetMovingUp()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                return true;
            }
        }
        return false;
    }
    public bool GetMovingRight()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                return true;
            }
        }
        return false;
    }
    public bool GetMovingLeft()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                return true;
            }
        }
        return false;
    }
    public bool GetMovingDown()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                return true;
            }
        }
        return false;
    }

    public Vector3 GetMousePos()
    {
        if (!lockPlayer)
        {
            if (!cursorVisibility)
            {
                Vector3 vec = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                vec.z = 0f;
                return vec;
            }
        }
        //Return middle of the view, player position
        var nullableVector3 = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        nullableVector3.z = 0f;
        return nullableVector3;
    }

    public bool FireArmRight()
    {
        if (!lockPlayer)
        {
            if (!lockRightArm)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool FireArmLeft()
    {
        if (!lockPlayer)
        {
            if (!lockLeftArm)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool GetReloading()
    {
        if (!lockPlayer)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                return true;
            }
        }
        return false;
    }

    public void SetLockPlayer()
    {
        lockPlayer = !lockPlayer;
    }

    public void SetLockPlayerRightArm()
    {
        lockRightArm = !lockRightArm;
    }
    public void SetLockPlayerLeftArm()
    {
        lockLeftArm = !lockLeftArm;
    }
    public void SetLockPlayerCursorVisibility()
    {
        cursorVisibility = !cursorVisibility;
    }

}
