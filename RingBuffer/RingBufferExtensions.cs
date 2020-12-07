using System;

namespace RingBuffer
{
  public static class RingBufferExtensions
  {
    /// <summary>
    /// Create a new array and copy the current data
    /// </summary>
    public static T[] ToArray<T>(this RingBuffer<T> self)
    {
      var array = new T[self.Count];
      self.CopyTo(array);
      return array;
    }

    /// <summary>
    /// Gets a read-only-span from the beginning of the buffer and decrements its count
    /// </summary>
    public static ReadOnlySpan<T> ConsumeSpan<T>(this RingBuffer<T> self, int maxLength)
    {
      var span = self.ReadSpan(maxLength);
      self.DecrementCount(span.Length);
      return span;
    }

    public static T PopFirst<T>(this RingBuffer<T> self)
    {
      var first = self[0];
      self.DecrementCount(1);
      return first;
    }
  }
}
