using System;
using Xunit;
using Tests;
using RingBuffer;

namespace Methods
{
  public class IndexProperty
  {
    [Theory]
    [ClassData(typeof(EmptyRingBuffers))]
    public void EmptyGetIndexingFails(RingBuffer<int> buffer)
    {
      Assert.Throws<IndexOutOfRangeException>(() => buffer[0]);
    }

    [Theory]
    [ClassData(typeof(EmptyRingBuffers))]
    public void EmptySetIndexingFails(RingBuffer<int> buffer)
    {
      Assert.Throws<IndexOutOfRangeException>(() => buffer[0] = 42);
    }

    [Theory]
    [ClassData(typeof(NotFullRingBuffers))]
    [ClassData(typeof(FullRingBuffers))]
    [ClassData(typeof(OverfilledRingBuffers))]
    public void GetIndexingFirst(RingBuffer<int> buffer)
    {
      Assert.Equal(1, buffer[0]);
    }

    [Theory]
    [ClassData(typeof(NotFullRingBuffers))]
    [ClassData(typeof(FullRingBuffers))]
    [ClassData(typeof(OverfilledRingBuffers))]
    public void GetIndexingLast(RingBuffer<int> buffer)
    {
      Assert.Equal(3, buffer[-1]);
    }
  }
}
