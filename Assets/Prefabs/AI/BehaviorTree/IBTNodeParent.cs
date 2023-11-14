using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inteface can be think of as an abstact class that has all abstract fucntions and no data memember
public interface IBTNodeParent
{
    void SetChildren(List<BTNode> newChildren);
    List<BTNode> GetChildren();
    void RemoveChild(BTNode childToRemove);
    void AddChild(BTNode childToAdd);
    void SortChildren();
}
