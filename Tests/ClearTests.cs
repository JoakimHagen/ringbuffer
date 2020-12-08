using RingBuffer;

namespace Tests
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
