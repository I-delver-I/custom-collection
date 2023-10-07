namespace CustomCollectionLib;

public sealed class CustomLinkedListNode<T>
{
    private CustomLinkedListNode<T>? _next;
    private CustomLinkedListNode<T>? _previous;
    private T _value;

    public CustomLinkedListNode(CustomLinkedList<T> list, T value)
    {
        List = list ?? throw new ArgumentNullException(nameof(list));
        _value = value;
    }

    public CustomLinkedList<T>? List { get; private set; }

    public CustomLinkedListNode<T>? Next
    {
        get => _next != null && _next != List!.Head ? _next : null;
        set => _next = value;
    }

    public CustomLinkedListNode<T>? Previous
    {
        get => _previous != null && this != List!.Head ? _previous : null;
        set => _previous = value;
    }

    public T Value
    {
        get => _value;
        set => _value = value;
    }

    public ref T ValueRef => ref _value;

    public void Clear()
    {
        List = null;
        _next = null;
        _previous = null;
    }
}
