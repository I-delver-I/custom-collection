namespace CustomCollectionLib.Tests;

public class CustomLinkedListNodeTests
{
    [Fact]
    public void Constructor_InitializesListCorrectly()
    {
        var initialList = new CustomLinkedList<int>();
        
        var node = new CustomLinkedListNode<int>(initialList, default);
        
        var actual = node.List;
        Assert.Equal(initialList, actual);
    }
    
    [Fact]
    public void Constructor_InitializesValueCorrectly()
    {
        var initialList = new CustomLinkedList<int>();
        const int initialValue = 10;
        
        var node = new CustomLinkedListNode<int>(initialList, initialValue);
        
        var actual = node.Value;
        Assert.Equal(initialValue, actual);
    }

    [Fact]
    public void Clear_SetsReferencesToNull()
    {
        var list = new CustomLinkedList<int>();
        var node = new CustomLinkedListNode<int>(list, default);
        node.Previous = node;
        node.Next = node;
        
        node.Clear();
        
        Assert.Null(node.List);
        Assert.Null(node.Next);
        Assert.Null(node.Previous);
    }

    [Fact]
    public void Value_SetsCorrectly()
    {
        var list = new CustomLinkedList<int>();
        var node = new CustomLinkedListNode<int>(list, default);
        const int expected = 10;
        
        node.Value = expected;
        
        var actual = node.Value;
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void ValueRef_ReturnsCorrectly()
    {
        var list = new CustomLinkedList<object?>();
        var node = new CustomLinkedListNode<object?>(list, default);
        var expected = new object();
        
        node.ValueRef = expected;
        
        var actual = node.ValueRef;
        Assert.Equal(expected, actual);
    }
}