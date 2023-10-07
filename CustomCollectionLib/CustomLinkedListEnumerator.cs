using System.Collections;

namespace CustomCollectionLib;

public class CustomLinkedListEnumerator<T> : IEnumerator<T>
{
    private readonly CustomLinkedList<T> _list;
    private CustomLinkedListNode<T>? _currentNode;
    private T? _currentValue;

    public T Current => _currentValue!;

    object? IEnumerator.Current => _currentValue;

    public CustomLinkedListEnumerator(CustomLinkedList<T> list)
    {
        _list = list;
        _currentNode = list.Head;
        
        if (list.Count != 0)
        {
            _currentValue = _currentNode!.Value;
        }
    }
    
    public bool MoveNext()
    {
        if (_currentNode is null)
        {
            return false;
        }

        _currentValue = _currentNode.Value;
        _currentNode = _currentNode.Next;

        if (_currentNode == _list.Head)
        {
            _currentNode = null;
        }

        return true;
    }

    public void Reset()
    {
        _currentValue = default;
        _currentNode = _list.Head;
    }

    public void Dispose()
    {
    }
}