using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Cover : MonoBehaviour
{
    [SerializeField] private Transform[] coverPoints;

    public Transform[] GetCoverPoints( )
    {
        return coverPoints;
    }
}
