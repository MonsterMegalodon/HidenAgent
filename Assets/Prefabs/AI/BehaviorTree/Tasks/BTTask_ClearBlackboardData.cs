using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_ClearBlackboardData : BTNode
{
    [SerializeField] string keyName;

    protected override BTNodeResult Execute()
    {
        if (!GetBehaviorTree()) return BTNodeResult.Failure;
        Blackboard blackboard = GetBehaviorTree().GetBlackBoard();
        if(!blackboard) return BTNodeResult.Failure;

        blackboard.ClearBlackboardData(keyName);
        return BTNodeResult.Success;
    }
}
