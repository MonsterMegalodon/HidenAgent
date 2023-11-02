using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardDecorator : Decorator
{
    //only determins if the node should run or not when it is this node's turn
    public enum RunCondition
    {
        KeyExists,
        KeyNotExist
    }

    //when to notify?
    public enum NotifyRule
    { 
        Runcondition,
        KeyValueChange
    }

    //what to terminate when you notify.
    public enum NotifyAbort
    {
        None,
        Self,
        Lower,
        Both
    }

    [SerializeField] string keyName;
    [SerializeField] RunCondition runCondition;
    [SerializeField] NotifyRule notifyRule;
    [SerializeField] NotifyAbort notifyAbort;

    object rawData;
    Blackboard blackboard;
    protected override BTNodeResult Execute()
    {
        if (!(GetBehaviorTree())) return BTNodeResult.Failure;

        blackboard = GetBehaviorTree().GetBlackBoard();
        if(!blackboard)
            return BTNodeResult.Failure;

        if (!CheckRunCondition())
            return BTNodeResult.Failure;
    }

    private bool CheckRunCondition()
    {
        rawData = blackboard.GetBlackboarRawData(keyName);
        if(runCondition == RunCondition.KeyExists)
        {
            return rawData != null;
        }
        else
        {
            return rawData == null;
        }
    }
}
