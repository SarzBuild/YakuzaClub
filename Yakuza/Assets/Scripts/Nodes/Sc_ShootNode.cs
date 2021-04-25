using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;
using Fork;

public class Sc_ShootNode : Sc_Node
{
    
    public static bool canShoot = true;
    private Vector3 aimDirection;
    private float timer;
    
    
    private NavMeshAgent agent;
    private Sc_EnemyAI1 ai;
    private Transform target;

    private Vector2 currentVelocity;
    private float smoothDamp;

    public Sc_ShootNode(NavMeshAgent agent, Sc_EnemyAI1 ai, Transform target)
    {
        this.agent = agent;
        this.ai = ai;
        this.target = target;
    }

    public override NodeState Evaluate()
    {
        agent.isStopped = true;
        aimDirection = (target.position - ai.transform.position).normalized;
        if (canShoot)
        {
            ai.EnemyHandleShooting();
            timer = 5f;
            Sc_FunctionUpdater.FunctionUpdater.Create(() =>
            {
                canShoot = false;
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    canShoot = true;
                    return true;
                }
                return false;
            });
        }
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        ai.transform.eulerAngles = new Vector3(0, 0, angle);
        return NodeState.RUNNING;
    }
}
