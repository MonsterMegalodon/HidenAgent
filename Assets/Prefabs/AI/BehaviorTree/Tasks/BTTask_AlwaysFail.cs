using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_AlwaysFail : BTNode
{
    protected override BTNodeResult Execute()
    {
        return BTNodeResult.Failure;
    }
}
