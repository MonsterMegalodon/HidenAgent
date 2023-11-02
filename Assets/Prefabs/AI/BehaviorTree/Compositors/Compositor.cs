using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Compositor : BTNode, IBTNodeParent
{
    [SerializeField]
    [HideInInspector]
    List<BTNode> children = new List<BTNode>();

    int currentChildIndex = -1;
     
    public override BTNodePortType GetOutputPortType()
    {
        return BTNodePortType.Multi;
    }

    //move to the next avaliable node, return true if there is one.
    protected bool Next()
    { 
        if(currentChildIndex < children.Count - 1)
        {
            ++currentChildIndex;
            return true;
        }
        return false;
    }

    protected override BTNodeResult Execute()
    {
        if(children.Count == 0) return BTNodeResult.Success;

        currentChildIndex = 0;
        return BTNodeResult.InProgress;
    }

    protected BTNodeResult UpdateCurrent()
    {
        return children[currentChildIndex].UpdateNode();
    }

    public override void End()
    {
        base.End();
        foreach(BTNode child in children)
        {
            child.End();
        }
    }

    public void AddChild(BTNode childToAdd)
    {
        children.Add(childToAdd);
    }

    public List<BTNode> GetChildren()
    {
        return children.ToList();
    }

    public void RemoveChild(BTNode childToRemove)
    {
        children.Remove(childToRemove);
    }

    public void SetChildren(List<BTNode> newChildren)
    {
        children = newChildren;
    }

    public override bool Contains(BTNode node)
    {
        foreach(BTNode child in children)
        {
            if(child.Contains(node))
            {
                return true;
            }
        }

        return base.Contains(node);
    }

    public void SortChildren()
    {
        children.Sort(SortAlgrithm);
    }

    private int SortAlgrithm(BTNode x, BTNode y)
    {
        if (x.GetGraphPosition().x > y.GetGraphPosition().x)
        {
            return 1;
        }

        if (x.GetGraphPosition().x < y.GetGraphPosition().x)
        {
            return -1;
        }

        return 0;
    }

    public override BTNode CloneNode()
    {
        Compositor compositorClone = Instantiate(this);
        compositorClone.children = new List<BTNode>();

        foreach(BTNode child in children)
        {
            compositorClone.children.Add(child.CloneNode());
        }

        return compositorClone;
    }
}
