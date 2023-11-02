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

        blackboard.onBlackboardValueChanged -= BlackboardValueChanged;
        blackboard.onBlackboardValueChanged += BlackboardValueChanged;
        
        if (!CheckRunCondition())
            return BTNodeResult.Failure;

        return BTNodeResult.InProgress;
    }

    private void BlackboardValueChanged(BlackboardEntry entry)
    {
        if(entry.GetKeyName() == keyName)
        {
            object newRawData = entry.GetRawValue();
            if(notifyRule == NotifyRule.Runcondition)
            {
                if(newRawData == null && rawData != null)
                {
                    Notify();
                }else if(newRawData != null && rawData == null)
                {
                    Notify();
                }
            }else if(notifyRule == NotifyRule.KeyValueChange)
            {
                if(newRawData != rawData)
                {
                    Notify();
                }
            }
            rawData = newRawData;
        }
    }

    private void Notify()
    {
        switch (notifyAbort)
        {
            case NotifyAbort.None:
                return;
            case NotifyAbort.Self:
                End();
                break;
            case NotifyAbort.Lower:
                GetBehaviorTree().AbortIfCurrentIsLower(GetPriority());
                break;
            case NotifyAbort.Both:
                End();
                GetBehaviorTree().AbortIfCurrentIsLower(GetPriority());
                break;
        }
    }

    protected override BTNodeResult Update()
    {
        return UpdateChild();
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
