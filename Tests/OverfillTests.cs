using RingBuffer;
using System;
using Xunit;

namespace RingBufferTests
{
  public class OverfillTests : ContentTests
  {
    protected int[] bigArray = new int[] { 40, 41, 42, 43, 44 };
    protected int[] slicedArray = new int[] { 41, 42, 43, 44 };

    public OverfillTests()
    {
      subject.Clear();
      subject.Add(bigArray);
      truth = slicedArray;
    }

    [Fact]
    public void AddsItem()
    {
      subject.Clear();
      subject.Add(42);
    }
  }
}
