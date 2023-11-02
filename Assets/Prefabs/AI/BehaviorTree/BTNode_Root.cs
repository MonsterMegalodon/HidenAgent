using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNode_Root : BTNode, IBTNodeParent
{
    [SerializeField]
    [HideInInspector]
    BTNode child;

    public override void End()
    {
        base.End();
        child.End();
    }

    public override BTNodePortType GetInputPortType()
    {
        return BTNodePortType.None;
    }

    public override BTNodePortType GetOutputPortType()
    {
        return BTNodePortType.Single;
    }

    public void AddChild(BTNode childToAdd)
    {
        child = childToAdd;
    }

    public List<BTNode> GetChildren()
    {
        if (child == null)
        {
            return new List<BTNode>();
        }

        return new List<BTNode> { child };
    }

    public void RemoveChild(BTNode childToRemove)
    {
        if(child == childToRemove)
        {
            child = null;
        }
    }

    public void SetChildren(List<BTNode> newChildren)
    {
        if(newChildren.Count!=0)
        {
            child = newChildren[0];
        }
    }

    protected override BTNodeResult Execute()
    {
        return BTNodeResult.InProgress;
    }

    protected override BTNodeResult Update()
    {
        return child.UpdateNode();
    }

    public override bool Contains(BTNode node)
    {
        if(child.Contains(node))
        {
            return true;
        }

        return base.Contains(node);
    }

    public void SortChildren()
    {

    }

    public override BTNode CloneNode()
    {
        BTNode_Root selfClone = Instantiate(this);
        selfClone.child = child.CloneNode();
        return selfClone;
    }
}
