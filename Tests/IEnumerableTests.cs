using System;
using Xunit;
using RingBuffer;

namespace RingBufferTests
{
  public class IEnumerableTests
  {
    protected readonly int[] SAMPLE = new int[] { 40, 41, 42 };

    protected RingBuffer<int> subject = new RingBuffer<int>(4);

    [Fact]
    public void EnumeratorValuesAreEqual()
    {
      subject.Add(SAMPLE);

      int i = 0;
      foreach (var item in subject)
      {
        Assert.Equal(SAMPLE[i++], item);
      }
    }

    [Fact]
    public void EmptyEnumeratorStops()
    {
      foreach (var _ in subject)
      {
        Assert.False(true);
      }
    }

    [Fact]
    public void EnumeratorGetsAddedItems()
    {
      subject.Add(SAMPLE);

      int i = 0;
      foreach (var item in subject)
      {
        if (i == 2)
        {
          subject.Add(77);
        }
        if (i < SAMPLE.Length)
        {
          Assert.Equal(SAMPLE[i++], item);
        }
        else
        {
          Assert.Equal(77, item);
        }
      }
    }
  }
}
