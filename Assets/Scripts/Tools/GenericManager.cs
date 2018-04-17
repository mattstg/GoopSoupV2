using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericManager<T> where T : IUpdatable  {
    //Second level should be a singleton!, this level cannot be one, otherwise it would cause the base layer to be returned

    List<T> managingList;
    Stack<T> toRemoveStack;
    Stack<T> toAddStack;

    public GenericManager()
    {
        managingList = new List<T>();
        toRemoveStack = new Stack<T>();
        toAddStack = new Stack<T>();
    }

    public virtual void Initialize()
    {

    }

    public virtual void UpdateManager(float dt)
    {
        UpdateStackElements();
        foreach (T t in managingList)
            t.IUpdate(dt);
    }

    private void UpdateStackElements()
    {
        //ObjectPool.Instance.AddToPool(toRemove.name, toRemove);

        while (toRemoveStack.Count > 0)
            managingList.Remove(toRemoveStack.Pop());

        while (toAddStack.Count > 0)
            managingList.Add(toAddStack.Pop());
    }

    public void AddItem(T toAdd)
    {
        toAddStack.Push(toAdd);
    }

    public void RemoveItem(T toRemove)
    {
        toRemoveStack.Push(toRemove);
    }
}
