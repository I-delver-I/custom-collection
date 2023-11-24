namespace CustomCollectionLib.Tests;

public class CustomStackTests
{
    [Fact]
    public void Push_PushesValueToEmptyStackCorrectly()
    {
        var stack = new CustomStack<int>();
        const int valueToPush = 1;

        stack.Push(valueToPush);

        var actualPushedValue = stack.Peek();
        Assert.Equal(valueToPush, actualPushedValue);
    }
    
    [Fact]
    public void Pop_RaisesItemPoppedEvent()
    {
        var stack = new CustomStack<int>();
        stack.Push(1);
        var eventRaised = false;
        stack.ItemPopped += (_, _) => eventRaised = true;

        stack.Pop();

        Assert.True(eventRaised, "The ItemPopped event should be raised.");
    }
    
    [Fact]
    public void Push_RaisesItemPushedEvent()
    {
        var stack = new CustomStack<int>();
        var eventRaised = false;
        stack.ItemPushed += (_, _) => eventRaised = true;
        
        stack.Push(1);

        Assert.True(eventRaised, "The ItemPushed event should be raised.");
    }
        
    [Fact]
    public void Clear_RaisesStackClearedEvent()
    {
        var stack = new CustomStack<int>();
        stack.Push(1);
        stack.Push(2);
        var eventRaised = false;
        stack.StackCleared += (_, _) => eventRaised = true;

        stack.Clear();

        Assert.True(eventRaised, "The StackCleared event should be raised.");
    }
    
    [Fact]
    public void GetEnumerator_IteratesOverStack()
    {
        var stack = new CustomStack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        using var enumerator = stack.GetEnumerator();
        var result = new List<int>();
        
        while (enumerator.MoveNext())
        {
            result.Add(enumerator.Current);
        }

        var expected = new[] { 1, 2, 3 };
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void TryPop_ReturnsTrueAndCorrectValue()
    {
        var stack = new CustomStack<int>();
        const int valueToPop = 1;
        stack.Push(valueToPop);

        var isPopped = stack.TryPop(out var poppedValue);

        Assert.Equal(valueToPop, poppedValue);
        Assert.True(isPopped);
    }
    
    [Fact]
    public void TryPop_StackIsEmpty_ReturnsFalseAndDefaultValue()
    {
        var stack = new CustomStack<int>();

        var isPopped = stack.TryPop(out var poppedValue);
        
        const int expectedPoppedValue = default;
        Assert.Equal(expectedPoppedValue, poppedValue);
        Assert.False(isPopped);
    }

    [Fact]
    public void Pop_StackIsEmpty_ThrowsInvalidOperationException()
    {
        var stack = new CustomStack<int>();

        Assert.Throws<InvalidOperationException>(() => stack.Pop());
    }

    [Fact]
    public void Pop_PopsLastElementCorrectly()
    {
        var stack = new CustomStack<int>();
        const int expectedPoppedValue = 3;
        stack.Push(expectedPoppedValue);
        
        var actualPoppedValue = stack.Pop();
        
        const int expectedElementsCount = 0;
        var actualElementsCount = stack.Count;
        Assert.Equal(expectedElementsCount, actualElementsCount);
        Assert.Equal(expectedPoppedValue, actualPoppedValue);
    }
    
    [Fact]
    public void Pop_PopsCorrectly()
    {
        var stack = new CustomStack<int>();
        const int expectedPoppedValue = 3;
        stack.Push(1);
        stack.Push(expectedPoppedValue);

        var poppedValue = stack.Pop();
        
        const int expectedElementsCount = 1;
        var actualElementsCount = stack.Count;
        Assert.Equal(expectedElementsCount, actualElementsCount);
        Assert.Equal(expectedPoppedValue, poppedValue);
    }
    
    [Fact]
    public void Clear_ClearsCorrectly()
    {
        var stack = new CustomStack<int>();
        stack.Push(1);
        stack.Push(3);
        
        stack.Clear();

        const int expected = 0;
        var actual = stack.Count;
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Contains_WorksCorrectly()
    {
        var stack = new CustomStack<int>();
        const int elementValueToCheck = 2;
        stack.Push(1);
        stack.Push(elementValueToCheck);

        var elementIsFound = stack.Contains(elementValueToCheck);
        
        Assert.True(elementIsFound);
    }
    
    [Theory]
    [InlineData(0, new []{1, 2, 3, 0, 0})]
    [InlineData(1, new []{0, 1, 2, 3, 0})]
    public void CopyTo_CopiesCorrectly(int index, int[] expected)
    {
        var stack = new CustomStack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        var destination = new int[expected.Length];

        stack.CopyTo(destination, index);

        Assert.Equal(expected, destination);
    }
    
    [Fact]
    public void Peek_PeeksCorrectly()
    {
        var stack = new CustomStack<int>();
        const int pushedValue = 1;
        stack.Push(pushedValue);

        var peekedValue = stack.Peek();

        Assert.Equal(pushedValue, peekedValue);
    }
    
    [Fact]
    public void Peek_StackIsEmpty_ThrowsInvalidOperationException()
    {
        var stack = new CustomStack<int>();

        Assert.Throws<InvalidOperationException>(() => stack.Peek());
    }
    
    [Fact]
    public void TryPeek_PeeksCorrectlyAndReturnsTrue()
    {
        var stack = new CustomStack<int>();
        const int pushedValue = 1;
        stack.Push(pushedValue);

        var isPicked = stack.TryPeek(out var pickedValue);
        
        Assert.True(isPicked);
        Assert.Equal(pushedValue, pickedValue);
    }
    
    [Fact]
    public void TryPeek_StackIsEmpty_PeeksDefaultAndReturnsFalse()
    {
        var stack = new CustomStack<int>();

        var isPicked = stack.TryPeek(out var pickedValue);
        
        Assert.False(isPicked);
        Assert.Equal(default, pickedValue);
    }
}