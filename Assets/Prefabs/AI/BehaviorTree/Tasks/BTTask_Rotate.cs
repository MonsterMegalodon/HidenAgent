using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Rotate : BTNode
{
    [SerializeField] float angleDegrees;
    [SerializeField] float acceptableOffset;
    [SerializeField] float turnSpeed;
    GameObject owner;

    Quaternion goalRotation;

    protected override BTNodeResult Execute()
    {
        if (!GetBehaviorTree()) return BTNodeResult.Failure;

        Blackboard blackboard = GetBehaviorTree().GetBlackBoard();
        if(!blackboard) return BTNodeResult.Failure;

        if(!blackboard.GetBlackboardData("owner", out owner))
            return BTNodeResult.Failure;


        goalRotation = Quaternion.AngleAxis(angleDegrees, Vector3.up) * owner.transform.rotation;

        if(IsInAcceptableAngle())
        {
            return BTNodeResult.Success;
        }

        return BTNodeResult.InProgress;
    }

    protected override BTNodeResult Update()
    {
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, goalRotation, turnSpeed * Time.deltaTime);
        if(IsInAcceptableAngle())
        {
            return BTNodeResult.Success;
        }
        return BTNodeResult.InProgress;
    }

    private bool IsInAcceptableAngle()
    {
        return Quaternion.Angle(goalRotation, owner.transform.rotation) < acceptableOffset;
    }
}
