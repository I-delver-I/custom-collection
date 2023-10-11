using System.Collections;

namespace CustomCollectionLib;

public class CustomLinkedList<T> : ICollection<T>
{
    public CustomLinkedListNode<T>? Head { get; private set; }
    public int Count { get; private set; }

    public CustomLinkedList()
    {
    }

    public CustomLinkedList(ICollection<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        foreach (var item in collection)
        {
            AddLast(item);
        }
    }
    
    public bool IsReadOnly => false;

    public CustomLinkedListNode<T>? First => Head;

    public CustomLinkedListNode<T>? Last => Head?.Previous;

    public IEnumerator<T> GetEnumerator()
    {
        return new CustomLinkedListEnumerator<T>(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    /// <exception cref="InvalidOperationException"></exception>
    private void ValidateNode(CustomLinkedListNode<T> node)
    {
        ArgumentNullException.ThrowIfNull(node);

        if (node.List != this)
        {
            throw new InvalidOperationException("The list node does not belong to current list");
        }
    }

    void ICollection<T>.Add(T item)
    {
        AddLast(item);
    }

    public void AddAfter(CustomLinkedListNode<T> existingNode, T value)
    {
        ValidateNode(existingNode);
        
        var nodeToAdd = new CustomLinkedListNode<T>(this, value);
        InsertNodeBefore(existingNode.Next!, nodeToAdd);
    }

    public void AddBefore(CustomLinkedListNode<T> existingNode, T value)
    {
        var nodeToAdd = new CustomLinkedListNode<T>(this, value);
        InsertNodeBefore(existingNode, nodeToAdd);

        if (existingNode == Head)
        {
            Head = nodeToAdd;
        }
    }

    public CustomLinkedListNode<T> AddFirst(T value)
    {
        var result = new CustomLinkedListNode<T>(this, value);

        if (Head is null)
        {
            AddNodeToEmptyList(result);
        }
        else
        {
            InsertNodeBefore(Head, result);
            Head = result;
        }

        return result;
    }

    public CustomLinkedListNode<T> AddLast(T value)
    {
        var result = new CustomLinkedListNode<T>(this, value);

        if (Head is null)
        {
            AddNodeToEmptyList(result);
        }
        else
        {
            InsertNodeBefore(Head, result);
        }

        return result;
    }

    /// <exception cref="InvalidOperationException"></exception>
    private void AddNodeToEmptyList(CustomLinkedListNode<T> newNode)
    {
        if (Head is not null && Count != 0)
        {
            throw new InvalidOperationException("List must be empty when this method is called!");
        }
        
        newNode.Next = newNode;
        newNode.Previous = newNode;
        Head = newNode;
        Count++;
    }

    private void InsertNodeBefore(CustomLinkedListNode<T> existingNode, CustomLinkedListNode<T> newNode)
    {
        ValidateNode(existingNode);
        
        newNode.Next = existingNode;
        newNode.Previous = existingNode.Previous;
        existingNode.Previous!.Next = newNode;
        existingNode.Previous = newNode;
        
        Count++;
    }

    public void Clear()
    {
        var currentNode = Head;
        while (currentNode is not null)
        {
            var tempNode = currentNode;
            currentNode = currentNode.Next;
            tempNode.Clear();
        }

        Head = null;
        Count = 0;
    }

    public bool Contains(T value) => Find(value) is not null;

    public CustomLinkedListNode<T>? FindLast(T value)
    {
        if (Head is null) return null;
        
        var lastNode = Head.Previous;
        var currentNode = lastNode;

        if (currentNode is not null)
        {
            do
            {
                var comparer = EqualityComparer<T>.Default;
                if (comparer.Equals(currentNode!.Value, value))
                {
                    return currentNode;
                }

                currentNode = currentNode.Previous;
            } while (currentNode != Head);
        }
        else
        {
            do
            {
                if (currentNode!.Value is null)
                {
                    return currentNode;
                }

                currentNode = currentNode.Previous;
            } while (currentNode != lastNode);
        }

        return null;
    }
    
    public CustomLinkedListNode<T>? Find(T value)
    {
        var currentNode = Head;
        if (currentNode is null) return null;
        
        if (value is not null)
        {
            do
            {
                var comparer = EqualityComparer<T>.Default;
                if (comparer.Equals(currentNode!.Value, value))
                {
                    return currentNode;
                }
                currentNode = currentNode.Next;
            } while (currentNode != Head);
        }
        else
        {
            do
            {
                if (currentNode!.Value == null)
                {
                    return currentNode;
                }
                currentNode = currentNode.Next;
            } while (currentNode != Head);
        }
        
        return null;
    }

    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public void CopyTo(T[] array, int index)
    {
        ArgumentNullException.ThrowIfNull(array);

        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index), index, "Non-negative number required");
        }

        if (index > array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index), index, 
                "Must be less than or equal to the size of the collection");
        }

        if (array.Length - index < Count)
        {
            throw new ArgumentException("Insufficient space in the target location to copy the information");
        }

        var tempNode = Head;
        if (tempNode is null) return;
        
        do
        {
            array[index++] = tempNode!.Value;
            tempNode = tempNode.Next;
        } while (tempNode != Head);
    }

    public bool Remove(T value)
    {
        var nodeToRemove = Find(value);
        if (nodeToRemove is null) return false;
        
        RemoveNode(nodeToRemove);
        return true;
    }
    
    public void RemoveFirst()
    {
        if (Head is null) throw new InvalidOperationException("This method shouldn't be called on empty list!");
        RemoveNode(Head);
    }

    public void RemoveLast()
    {
        if (Head is null) throw new InvalidOperationException("This method shouldn't be called on empty list!");
        if (Head.Previous is null) throw new InvalidOperationException("The node doesn't exist");
        RemoveNode(Head.Previous);
    }

    /// <exception cref="InvalidOperationException"></exception>
    private void RemoveNode(CustomLinkedListNode<T> nodeToRemove)
    {
        ValidateNode(nodeToRemove);

        if (Head is null)
        {
            throw new InvalidOperationException("This method shouldn't be called on empty list!");
        }
        
        if (nodeToRemove.Next == nodeToRemove)
        {
            if (Head != nodeToRemove && Count != 1)
            {
                throw new InvalidOperationException("This should only be true for a list with only one node");
            }
            
            Head = null;
        }
        else
        {
            nodeToRemove.Next!.Previous = nodeToRemove.Previous;
            nodeToRemove.Previous!.Next = nodeToRemove.Next;

            if (Head == nodeToRemove)
            {
                Head = nodeToRemove.Next;
            }
        }
        
        nodeToRemove.Clear();
        Count--;
    }
}