using RingBuffer;
using Tests;
using Xunit;

namespace Methods
{
  public class AddMultiple
  {
    [Theory]
    [ClassData(typeof(EmptyRingBuffers))]
    public void IncrementsCount(RingBuffer<int> buffer)
    {
      var oldCount = buffer.Count;
      var added = buffer.Add(new int[] { 4, 5 });
      Assert.Equal(oldCount + 2, buffer.Count);
      Assert.Equal(2, added);
    }

    [Theory]
    [ClassData(typeof(NotFullRingBuffers))]
    public void PariallyIncrementsCount(RingBuffer<int> buffer)
    {
      var added = buffer.Add(new int[] { 4, 5 });
      Assert.Equal(3, buffer.Count); // full capacity
      Assert.Equal(2, added);
    }

    [Theory]
    [ClassData(typeof(FullRingBuffers))]
    [ClassData(typeof(OverfilledRingBuffers))]
    public void FullDoesntIncrementCount(RingBuffer<int> buffer)
    {
      var oldCount = buffer.Count;
      buffer.Add(new int[] { 4, 5 });
      Assert.Equal(oldCount, buffer.Count);
    }

    [Theory]
    [ClassData(typeof(AllRingBuffers))]
    public void StoresItems(RingBuffer<int> buffer)
    {
      buffer.Add(new int[] { 4, 5 });
      Assert.Equal(4, buffer[-2]);
      Assert.Equal(5, buffer[-1]);
    }

    [Theory]
    [ClassData(typeof(AllRingBuffers))]
    public void IncrementsTotalCount(RingBuffer<int> buffer)
    {
      var oldTotalCount = buffer.TotalCount;
      buffer.Add(new int[] { 4, 5 });
      Assert.Equal(oldTotalCount + 2, buffer.TotalCount);
    }
  }
}
