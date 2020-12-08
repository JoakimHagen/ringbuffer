using RingBuffer;
using Tests;
using Xunit;

namespace Methods
{
  public class Add
  {
    [Theory]
    [ClassData(typeof(EmptyRingBuffers))]
    [ClassData(typeof(NotFullRingBuffers))]
    public void IncrementsCount(RingBuffer<int> buffer)
    {
      var oldCount = buffer.Count;
      buffer.Add(11);
      Assert.Equal(oldCount + 1, buffer.Count);
    }

    [Theory]
    [ClassData(typeof(FullRingBuffers))]
    [ClassData(typeof(OverfilledRingBuffers))]
    public void FullDoesntIncrementCount(RingBuffer<int> buffer)
    {
      var oldCount = buffer.Count;
      buffer.Add(11);
      Assert.Equal(oldCount, buffer.Count);
    }

    [Theory]
    [ClassData(typeof(AllRingBuffers))]
    public void StoresItem(RingBuffer<int> buffer)
    {
      buffer.Add(11);
      Assert.Equal(11, buffer[-1]);
    }

    [Theory]
    [ClassData(typeof(AllRingBuffers))]
    public void IncrementsTotalCount(RingBuffer<int> buffer)
    {
      var oldTotalCount = buffer.TotalCount;
      buffer.Add(11);
      Assert.Equal(oldTotalCount + 1, buffer.TotalCount);
    }
  }
}
