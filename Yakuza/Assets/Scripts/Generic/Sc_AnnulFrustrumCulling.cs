using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_AnnulFrustrumCulling : MonoBehaviour
{
    public Camera _camera;

    void Start()
    {
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update ()
    {
        
        Vector3 camPosition = _camera.transform.position;
        Vector3 normCamForward = Vector3.Normalize(_camera.transform.forward);
        float boundsDistance = (_camera.farClipPlane - _camera.nearClipPlane) / 2 + _camera.nearClipPlane;
        Vector3 boundsTarget = camPosition + (normCamForward * boundsDistance);
        
        Vector3 realtiveBoundsTarget = this.transform.InverseTransformPoint(boundsTarget);
        
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.bounds = new Bounds(realtiveBoundsTarget, Vector3.one);
    }
}
