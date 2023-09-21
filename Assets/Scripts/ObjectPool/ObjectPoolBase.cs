using UnityEngine;
using System;
using System.Collections.Generic;

public class ObjectPoolBase<T>: MonoBehaviour
{
    private readonly Func<T> _preloadFunc;
    private readonly Action<T> _getAction;
    private readonly Action<T> _returnAction;

    private Queue<T> _pool = new Queue<T>();
    private List<T> _active = new List<T>();

    public ObjectPoolBase(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
    {
        _preloadFunc = preloadFunc;
        _getAction = getAction;
        _returnAction = returnAction;

        if(preloadFunc == null)
        {
            Debug.LogError("Preload Function is null");
            return;
        }

        for (int i = 0; i < preloadCount; i++)
            Return(preloadFunc());
    }

    public T Get()
    {
        T item = _pool.Count > 0 ? _pool.Dequeue() : _preloadFunc();
        _getAction(item);
        _active.Add(item);
        return item;
    }

    public void Return(T item)
    {
        _returnAction(item);
        _pool.Enqueue(item);
        _active.Remove(item);
    }

    public void ReturnAll()
    {
        foreach (T item in _active.ToArray())
            Return(item);
    }
}
