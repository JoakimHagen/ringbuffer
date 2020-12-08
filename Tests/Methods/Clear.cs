using RingBuffer;
using Tests;
using Xunit;

namespace Methods
{
  public class Clear
  {
    [Theory]
    [ClassData(typeof(AllRingBuffers))]
    public void IsEmpty(RingBuffer<int> buffer)
    {
      buffer.Clear();
      Assert.Equal(0, buffer.Count);
    }
  }
}
