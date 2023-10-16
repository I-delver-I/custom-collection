using CustomCollectionLib;
using CustomCollectionTestingApp;

var stack = new CustomStack<string>();
using var messageService = new StackMessageService<string>(stack);

stack.Push("K");
stack.Push("P");
stack.Push("I");

stack.Pop();

Console.WriteLine("\nStack content:");
foreach (var item in stack)
{
    Console.WriteLine(item);
}

Console.WriteLine($"Stack count: {stack.Count}");
stack.Clear();
Console.WriteLine($"Stack count: {stack.Count}");