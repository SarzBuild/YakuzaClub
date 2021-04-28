using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Fork;

public class Sc_PlayerShooting : MonoBehaviour
{
    [SerializeField] private Sc_PlayerController playerController;
    [SerializeField] private Material weaponTracerMaterial;
    private Material material;
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;
    private Mesh mesh;
    //private float timer = .1f;
    private Sc_WorldMesh.World_Mesh worldMesh;
    
    void Start()
    {
        playerController.Shooting += PlayerController_Shooting;
    }

    private void PlayerController_Shooting(object sender, Sc_PlayerController.ShootingEventArgs e)
    {
        CreateWeaponTracer(e.gunPointPosition, e.shootPosition);
    }

    private void CreateWeaponTracer(Vector3 fromPosition, Vector3 targetPosition)
    {
        Vector3 dir = (targetPosition - fromPosition).normalized;
        float eulerZ = GetAngleFromVectroFloat(dir) - 90;
        float distance = Vector3.Distance(fromPosition, targetPosition);
        Vector3 tracerSpawnPosition = fromPosition + dir * distance * .5f;
        Material tempWeaponMaterial = new Material(weaponTracerMaterial);
        tempWeaponMaterial.SetTextureScale("_MainTex", new Vector2( 1f, distance / 8));
        Sc_WorldMesh.World_Mesh worldMesh = Sc_WorldMesh.World_Mesh.Create(tracerSpawnPosition, eulerZ, 1f, distance, tempWeaponMaterial, null, 10000);

        int frame = 0;
        float framerate = .016f;
        float timer = framerate;
        worldMesh.SetUVCoords(new Sc_WorldMesh.World_Mesh.UVCoords(0, 0, 8, 75));
        Sc_FunctionUpdater.FunctionUpdater.Create(() =>
        {
            timer -= Time.deltaTime;
            if (worldMesh.gameObject.GetComponent<BoxCollider2D>() == null)
            {
                worldMesh.gameObject.AddComponent<BoxCollider2D>();
                worldMesh.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }

            if (worldMesh.gameObject.GetComponent<Sc_PlayerBullet>() == null)
                worldMesh.gameObject.AddComponent<Sc_PlayerBullet>();

            if (timer <= 0)
            {
                frame++;
                timer += framerate;
                if (frame >= 4)
                {
                    worldMesh.DestroySelf();
                    return true;
                }
                else
                {
                    worldMesh.SetUVCoords(new Sc_WorldMesh.World_Mesh.UVCoords(8 * frame, 0, 8, 75));
                }
            }
            return false;
        });
    }

    public float GetAngleFromVectroFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
    
}