using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_HealthNode : Sc_Node
{
    private Sc_EnemyAI1 ai;
    private float threshold;

    public Sc_HealthNode(Sc_EnemyAI1 ai, float threshold)
    {
        this.ai = ai;
        this.threshold = threshold;
    }
    public override NodeState Evaluate()
    {
        return ai.currentHealth <= threshold ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
