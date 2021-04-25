using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Inverter : Sc_Node
{
    protected Sc_Node node;

    public Sc_Inverter(Sc_Node node)
    {
        this.node = node;
    }
    
    public override NodeState Evaluate()
    {
        switch (node.Evaluate())
        {
            case NodeState.RUNNING:
                _nodeState = NodeState.RUNNING;
                break;
            case NodeState.SUCCESS:
                _nodeState = NodeState.FAILURE;
                break;
            case NodeState.FAILURE:
                _nodeState = NodeState.SUCCESS;
                break;
            default:
                break;
        }
        return _nodeState;
    }
}