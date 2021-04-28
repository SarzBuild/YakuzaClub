using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sc_ChaseNode : Sc_Node
{
    private Transform target;
    private NavMeshAgent agent;
    private Sc_EnemyAI1 ai;
    private Vector3 direction;

    public Sc_ChaseNode(Transform target, NavMeshAgent agent, Sc_EnemyAI1 ai)
    {
        this.target = target;
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        direction = (agent.destination - ai.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        ai.transform.eulerAngles = new Vector3(0, 0, angle);
        
        float distance = Vector3.Distance(target.position, agent.transform.position);
        if (distance > 0.2f)
        {
            Debug.Log("Agent is moving");
            agent.isStopped = false;
            agent.SetDestination(target.position);
            return NodeState.RUNNING;
        }
        else
        {
            Debug.Log("Agent is stopped");
            agent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }
}
