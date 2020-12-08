using RingBuffer;
using System;
using Xunit;

namespace Tests
{
  public class TimingTests
  {
    protected readonly int[] SAMPLE = new int[] { 40, 41, 42 };

    protected int[] targetArray = new int[3];

    protected RingBuffer<int> subject = new RingBuffer<int>(4);

    protected object item = null;

    public TimingTests()
    {
      subject.Add(SAMPLE);
    }

    [Fact]
    public void AddSingle()
    {
      for (var i = 0; i < 1_000_000; i++)
      {
        subject.Add(41);
      }
    }

    [Fact]
    public void AddSpan()
    {
      for (var i = 0; i < 1_000_000; i++)
      {
        subject.Add(SAMPLE);
      }
    }

    [Fact]
    public void CopyTo()
    {
      for (var i = 0; i < 1_000_000; i++) {
        subject.CopyTo(targetArray);
      }
    }

    [Fact]
    public void RandomAccess()
    {
      for (var i = 0; i < 1_000_000; i++)
      {
        for (var j = 0; j < subject.Count; j++)
        {
          var item = subject[j];
          subject[j] = item;
        }
      }
    }

    [Fact]
    public void IEnumerator()
    {
      for (var i = 0; i < 1_000_000; i++)
      {
        foreach (var x in subject)
        {
          item = x;
        }
      }
    }
  }
}