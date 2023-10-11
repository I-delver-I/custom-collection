using System.Collections;

namespace CustomCollectionLib;

public class CustomStack<T> : IEnumerable<T>
{
    private readonly CustomLinkedList<T> _list = new();

    public bool TryPeek(out T result)
    {
        if (_list.Count == 0)
        {
            result = default!;
            return false;
        }

        result = _list.Last!.Value;
        return true;
    }
    
    /// <exception cref="InvalidOperationException"></exception>
    public T Peek()
    {
        if (_list.Count == 0)
            throw new InvalidOperationException("Stack empty");
        return _list.Last!.Value;
    }
    
    public void CopyTo(T[] array, int arrayIndex)
    {
        _list.CopyTo(array, arrayIndex);
    }
    
    public bool Contains(T value)
    {
        return _list.Contains(value);
    }
    
    public void Clear()
    {
        _list.Clear();
    }

    public bool TryPop(out T result)
    {
        if (_list.Count == 0)
        {
            result = default!;
            return false;
        }

        result = _list.Last!.Value;
        _list.RemoveLast();
        return true;
    }
    
    /// <exception cref="InvalidOperationException"></exception>
    public T Pop()
    {
        if (_list.Count == 0)
            throw new InvalidOperationException("Stack empty");
        
        var elementToRemove = _list.Last!.Value;
        _list.RemoveLast();
        return elementToRemove;
    }

    public void Push(T value)
    {
        _list.AddLast(value);
    }

    public int Count => _list.Count;

    public IEnumerator<T> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}