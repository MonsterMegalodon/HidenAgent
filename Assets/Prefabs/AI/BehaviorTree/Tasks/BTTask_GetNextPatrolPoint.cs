using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_GetNextPatrolPoint : BTNode
{
    [SerializeField] string patrolPointKeyName;

    protected override BTNodeResult Execute()
    {
        if (!GetBehaviorTree()) return BTNodeResult.Failure;

        Blackboard blackboard = GetBehaviorTree().GetBlackBoard();
        if(!blackboard) return BTNodeResult.Failure;

        if(!blackboard.GetBlackboardData("owner", out GameObject owner))
            return BTNodeResult.Failure;

        PatrollingComponent patrollingComp = owner.GetComponent<PatrollingComponent>();
        if(!patrollingComp || !patrollingComp.GetNextPatrollingPos(out Vector3 pos)) 
            return BTNodeResult.Failure;

        blackboard.SetBloackboardData(patrolPointKeyName, pos);
        return BTNodeResult.Success;
    }
}
