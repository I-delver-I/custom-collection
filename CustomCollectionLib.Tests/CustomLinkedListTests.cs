namespace CustomCollectionLib.Tests;

public class CustomLinkedListTests
{
    [Fact]
    public void RemoveLast_RemovesCorrectly()
    {
        var list = new CustomLinkedList<int>();
        const int firstNodeValue = 1;
        list.AddLast(firstNodeValue);
        list.AddLast(2);
        
        list.RemoveLast();

        var actual = list.Last!.Value;
        Assert.Equal(firstNodeValue, actual);
    }
    
    [Fact]
    public void RemoveLast_HeadPreviousIsNull_ThrowsInvalidOperationException()
    {
        var list = new CustomLinkedList<int>();
        list.AddLast(1);
        list.Head!.Previous = null;

        Assert.Throws<InvalidOperationException>(() => list.RemoveLast());
    }
    
    [Fact]
    public void RemoveLast_HeadIsNull_ThrowsInvalidOperationException()
    {
        var list = new CustomLinkedList<int>();

        Assert.Throws<InvalidOperationException>(() => list.RemoveLast());
    }
    
    [Fact]
    public void RemoveFirst_RemovesCorrectly()
    {
        var list = new CustomLinkedList<int>();
        const int secondNodeValue = 2;
        list.AddLast(1);
        list.AddLast(secondNodeValue);
        
        list.RemoveFirst();

        var actual = list.First!.Value;
        Assert.Equal(secondNodeValue, actual);
    }
    
    [Fact]
    public void RemoveFirst_HeadIsNull_ThrowsInvalidOperationException()
    {
        var list = new CustomLinkedList<int>();

        Assert.Throws<InvalidOperationException>(() => list.RemoveFirst());
    }
    
    [Fact]
    public void Remove_RemovesSuccessfully()
    {
        var list = new CustomLinkedList<int>();
        const int nodeValueToRemove = 2;
        list.AddLast(nodeValueToRemove);

        var removeResult = list.Remove(nodeValueToRemove);

        var removedNode = list.Find(nodeValueToRemove);
        Assert.Null(removedNode);
        Assert.True(removeResult);
    }
    
    [Fact]
    public void Remove_HeadIsNull_ThrowsInvalidOperationException()
    {
        var list = new CustomLinkedList<int>();
        const int nodeValueToRemove = 10;

        var removeResult = list.Remove(nodeValueToRemove);
        
        Assert.False(removeResult);
    }
    
    [Fact]
    public void Remove_NodeToRemoveIsNull_ReturnsFalse()
    {
        var list = new CustomLinkedList<int>();
        const int nodeValueToRemove = 10;

        var removeResult = list.Remove(nodeValueToRemove);
        
        Assert.False(removeResult);
    }
    
    [Theory]
    [InlineData(0, new []{1, 2, 3, 0, 0})]
    [InlineData(1, new []{0, 1, 2, 3, 0})]
    public void CopyTo_CopiesCorrectly(int index, int[] expected)
    {
        var list = new CustomLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);
        var destination = new int[expected.Length];

        list.CopyTo(destination, index);

        Assert.Equal(expected, destination);
    }
    
    [Fact]
    public void CopyTo_HeadIsNull_InterruptsCopying()
    {
        var list = new CustomLinkedList<int>();
        var expected = new int[5];
        var destination = new int[expected.Length];
        const int index = 0;
        
        list.CopyTo(destination, index);

        Assert.Equal(expected, destination);
    }
    
    [Fact]
    public void CopyTo_IndexIsEqualToArrayLength_ThrowsArgumentException()
    {
        var list = new CustomLinkedList<int>();
        list.AddLast(1);
        var destination = new int[list.Count];
        var index = destination.Length;

        Assert.Throws<ArgumentException>(() => list.CopyTo(destination, index));
    }
    
    [Fact]
    public void CopyTo_IndexIsBiggerThanArrayLength_ThrowsArgumentOutOfRangeException()
    {
        var list = new CustomLinkedList<int>();
        list.AddLast(1);
        var destination = new int[list.Count];
        var index = destination.Length + 1;

        Assert.Throws<ArgumentOutOfRangeException>(() => list.CopyTo(destination, index));
    }
    
    [Fact]
    public void CopyTo_IndexIsLessThanZero_ThrowsArgumentOutOfRangeException()
    {
        var list = new CustomLinkedList<int>();
        list.AddLast(1);
        var destination = new int[list.Count];
        const int index = -5;

        Assert.Throws<ArgumentOutOfRangeException>(() => list.CopyTo(destination, index));
    }
    
    [Fact]
    public void FindLast_SearchesCorrectly()
    {
        var list = new CustomLinkedList<int>();
        list.AddLast(1);
        const int nodeValueToFind = 3;
        list.AddLast(nodeValueToFind);
        var expected = list.AddLast(nodeValueToFind);
        list.AddLast(4);

        var actual = list.FindLast(nodeValueToFind);
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void FindLast_HeadIsNull_ReturnsNull()
    {
        var list = new CustomLinkedList<int>();
        const int nodeValueToFind = 10;

        var foundNode = list.FindLast(nodeValueToFind);
        
        Assert.Null(foundNode);
    }
    
    [Fact]
    public void Clear_ClearsCorrectly()
    {
        var list = new CustomLinkedList<int>();
        list.AddLast(10);
        list.AddLast(20);
        
        list.Clear();
        
        Assert.Null(list.Head);
    }
    
    [Fact]
    public void AddFirst_HeadIsNull_AddsCorrectly()
    {
        var list = new CustomLinkedList<int>();
        const int nodeValueToAdd = 10;

        var actual = list.AddFirst(nodeValueToAdd);

        var expected = list.Head;
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void AddFirst_AddsNodeAsHead()
    {
        var list = new CustomLinkedList<int>();
        var lastNode = list.AddLast(1);
        const int nodeValueToAdd = 10;

        var actual = list.AddFirst(nodeValueToAdd);

        var expected = list.Head;
        Assert.Equal(expected, actual);
        Assert.Equal(list.Last, lastNode);
    }
    
    [Fact]
    public void AddBefore_AddsBeforeHeadCorrectly()
    {
        var list = new CustomLinkedList<int>();
        var nodeToAddBefore = list.AddLast(1);
        const int valueToAddBefore = 10;
        
        list.AddBefore(nodeToAddBefore, valueToAddBefore);

        var actual = list.Find(valueToAddBefore);
        var expected = list.Head;
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void AddBefore_AddsBeforeExistingNodeCorrectly()
    {
        var list = new CustomLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        var nodeToAddBefore = list.AddLast(3);
        const int nodeValueToAdd = 10;
        
        list.AddBefore(nodeToAddBefore, nodeValueToAdd);

        var actual = list.Find(nodeValueToAdd);
        var expected = nodeToAddBefore.Previous;
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Constructor_AddCollectionElementsToLast()
    {
        var list = new List<int> { 1, 2, 3 };

        var customList = new CustomLinkedList<int>(list);

        Assert.All(list, item => Assert.Contains(item, customList));
    }
    
    [Fact]
    public void Constructor_PassedNullCollection_ThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new CustomLinkedList<int>(null!));
    }
    
    [Fact]
    public void Find_NodeNotFound_ReturnsNull()
    {
        var list = new CustomLinkedList<int?>();
        list.AddLast(1);

        var actual = list.Find(null);

        Assert.Null(actual);
    }
    
    [Fact]
    public void Find_SearchedValueIsNull_ReturnsNodeWithNullValue()
    {
        var list = new CustomLinkedList<int?>();
        list.AddLast(1);
        var expected = list.AddLast(null);

        var actual = list.Find(null);

        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Find_ListIsEmpty_ReturnsNull()
    {
        var list = new CustomLinkedList<int>();
        const int nodeValueToFind = 10;

        var actual = list.Find(nodeValueToFind);

        Assert.Null(actual);
    }
    
    [Fact]
    public void AddAfter_AddsAfterExistingNodeCorrectly()
    {
        var list = new CustomLinkedList<int>();
        var nodeToAddAfter = list.AddLast(1);
        list.AddLast(2);
        const int nodeValueToAdd = 10;
            
        list.AddAfter(nodeToAddAfter, nodeValueToAdd);
        
        var actual = list.Find(nodeValueToAdd);
        var expected = nodeToAddAfter.Next;
        Assert.Equal(expected, actual);
    }
}