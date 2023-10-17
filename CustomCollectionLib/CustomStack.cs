using System.Collections;

namespace CustomCollectionLib;

public class CustomStack<T> : IEnumerable<T>
{
    private readonly CustomLinkedList<T> _list = new();

    public event EventHandler? StackCleared;
    public event EventHandler<T>? ItemPushed;
    public event EventHandler<T>? ItemPopped;

    protected virtual void OnItemPopped(T value)
    {
        ItemPopped?.Invoke(this, value);
    }
    
    protected virtual void OnStackCleared()
    {
        StackCleared?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnItemPushed(T value)
    {
        ItemPushed?.Invoke(this, value);
    }

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
        OnStackCleared();
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
        
        OnItemPopped(elementToRemove);
        return elementToRemove;
    }

    public void Push(T value)
    {
        _list.AddLast(value);
        OnItemPushed(value);
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