namespace CustomCollectionLib.Tests;

public class CustomLinkedListEnumeratorTests
{
    [Fact]
    public void MoveNext_ReturnsFalseForEmptyList()
    {
        var list = new CustomLinkedList<int>();
        var enumerator = new CustomLinkedListEnumerator<int>(list);

        var movingResult = enumerator.MoveNext();
        
        Assert.False(movingResult);
    }
    
    [Fact]
    public void MoveNext_IteratesOverList()
    {
        var list = new CustomLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);
        var enumerator = new CustomLinkedListEnumerator<int>(list);
        var elementsCount = 0;

        while (enumerator.MoveNext())
        {
            elementsCount++;
        }

        var actual = list.Count;
        Assert.Equal(elementsCount, actual);
    }
    
    [Fact]
    public void Reset_ResetsEnumerator()
    {
        var list = new CustomLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        var enumerator = new CustomLinkedListEnumerator<int>(list);
        
        enumerator.MoveNext();
        enumerator.Reset();
        
        const int expected = 0;
        var actual = enumerator.Current;
        Assert.Equal(expected, actual);
    }
}