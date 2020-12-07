using RingBuffer;

namespace RingBufferTests
{
  public class ClearTests : EmptyTests
  {
    public ClearTests()
    {
      subject = new RingBuffer<int>(4);
      subject.Add(SAMPLE);
      subject.Clear();
    }
  }
}
