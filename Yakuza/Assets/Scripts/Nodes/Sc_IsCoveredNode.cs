using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_IsCoveredNode : Sc_Node
{
    private Transform target;
    private Transform origin;

    public Sc_IsCoveredNode(Transform target, Transform origin)
    {
        this.target = target;
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        RaycastHit2D hit = Physics2D.Raycast(origin.position, target.position - origin.position);
        if (hit)
        {
            if (hit.collider.transform != target)
            {
                return NodeState.SUCCESS;
            }
        }
        return NodeState.FAILURE;
    }
}
