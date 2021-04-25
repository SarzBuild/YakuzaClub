using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sc_GoToCoverNode : Sc_Node
{
    private NavMeshAgent agent;
    private Sc_EnemyAI1 ai;

    public Sc_GoToCoverNode(NavMeshAgent agent, Sc_EnemyAI1 ai)
    {
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        Transform coverPoint = ai.GetBestCoverPoint();
        if (coverPoint == null)
            return NodeState.FAILURE;
        ai.SetColor(Color.blue);
        float distance = Vector3.Distance(coverPoint.position, agent.transform.position);
        if (distance > 0.2f)
        {
            agent.isStopped = false;
            agent.SetDestination(coverPoint.position);
            return NodeState.RUNNING;
        }
        else
        {
            agent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }
}