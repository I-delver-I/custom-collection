using CustomCollectionLib;

namespace CustomCollectionTestingApp;

public class StackMessageService<T> : IDisposable
{
    private readonly CustomStack<T> _stack;

    public StackMessageService(CustomStack<T> stack)
    {
        _stack = stack;
        stack.StackCleared += OnStackCleared;
        stack.ItemPushed += OnItemPushed;
        stack.ItemPopped += OnItemPopped;
    }
    
    private static void OnItemPopped(object? source, T value)
    {
        Console.WriteLine($"The item \"{value}\" was popped from the stack!");
    }

    private static void OnItemPushed(object? source, T value)
    {
        Console.WriteLine($"The item \"{value}\" was pushed to the stack!");
    }

    private static void OnStackCleared(object? source, EventArgs e)
    {
        Console.WriteLine("The stack was cleared!");
    }

    public void Dispose()
    {
        _stack.ItemPopped -= OnItemPopped;
        _stack.StackCleared -= OnStackCleared;
        _stack.ItemPushed -= OnItemPushed;
    }
}