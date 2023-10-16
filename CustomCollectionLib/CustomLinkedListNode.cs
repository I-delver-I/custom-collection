namespace CustomCollectionLib;

public sealed class CustomLinkedListNode<T>
{
    private T _value;

    public CustomLinkedListNode(CustomLinkedList<T> list, T value)
    {
        List = list ?? throw new ArgumentNullException(nameof(list));
        _value = value;
    }

    public CustomLinkedList<T>? List { get; private set; }

    public CustomLinkedListNode<T>? Next { get; set; }

    public CustomLinkedListNode<T>? Previous { get; set; }

    public T Value
    {
        get => _value;
        set => _value = value;
    }

    public ref T ValueRef => ref _value;

    public void Clear()
    {
        List = null;
        Next = null;
        Previous = null;
    }
}
