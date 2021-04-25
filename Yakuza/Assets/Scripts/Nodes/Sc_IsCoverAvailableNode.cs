using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Sc_IsCoverAvailableNode : Sc_Node
{
    private Sc_Cover[] availableCovers;
    private Transform target;
    private Sc_EnemyAI1 ai;

    public Sc_IsCoverAvailableNode(Sc_Cover[] availableCovers, Transform target, Sc_EnemyAI1 ai)
    {
        this.availableCovers = availableCovers;
        this.target = target;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Transform bestPoint = FindBestCoverPoint();
        ai.SetBestCoverPoint(bestPoint);
        return bestPoint != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    private Transform FindBestCoverPoint()
    {
        if (ai.GetBestCoverPoint() != null)
        {
            if (CheckIfCoverPointIsValid(ai.GetBestCoverPoint()))
            {
                return ai.GetBestCoverPoint();
            }
        }
        float minAngle = 90;
        Transform bestPoint = null;
        for (int i = 0; i < availableCovers.Length; i++)
        {
            Transform bestPointInCover = FindBestPointInCover(availableCovers[i], ref minAngle);
            if (bestPointInCover != null)
            {
                bestPoint = bestPointInCover;
            }
        }
        return bestPoint;
    }

    private Transform FindBestPointInCover(Sc_Cover cover, ref float minAngle)
    {
        Transform[] availablePoints = cover.GetCoverPoints();
        Transform bestPoint = null;
        for (int i = 0; i < availablePoints.Length; i++)
        {
            Vector3 direction = target.position - availablePoints[i].position;
            if (CheckIfCoverPointIsValid(availablePoints[i]))
            {
                float angle = Vector2.Angle(availablePoints[i].forward, direction);
                if (angle < minAngle)
                {
                    minAngle = angle;
                    bestPoint = availablePoints[i];
                }
            }
        }

        return bestPoint;
    }

    private bool CheckIfCoverPointIsValid(Transform point)
    {
        Vector3 direction = target.position - point.position;
        RaycastHit2D hit = Physics2D.Raycast(point.position, direction);
        if (hit.collider.transform != target)
        {
            return true;
        }

        return false;
    }
}
