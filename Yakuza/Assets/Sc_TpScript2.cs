using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_TpScript2 : MonoBehaviour
{
    private GameObject tpPos;
    private GameObject player;
    private Sc_PlayerInputs inputManager;
    private Sc_CameraRelated cameraRelated;

    void Start()
    {
        tpPos = transform.Find("Pos1").gameObject;
        player = GameObject.FindWithTag("Player");
        inputManager = Sc_PlayerInputs.Instance;
        cameraRelated = Sc_CameraRelated.CameraInstance;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inputManager.SetLockPlayer();
            cameraRelated.FadeOut(1f, false);
            Invoke("TpPlayer", 1.1f);
            Invoke("ResetMobility", 2);
        }
    }

    void TpPlayer()
    {
        player.transform.position = tpPos.transform.position;
    }
        
    void ResetMobility()
    {
        inputManager.SetLockPlayer();
        cameraRelated.FadeIn(1f, false);
    }
}
