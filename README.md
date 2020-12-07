# ringbuffer
A C#, random-access mutable buffer with fixed length that will wrap around and overlap when written to.

### Features
- Generic typed content
- Random-access read and write
- Negative indexing counting from the last item
- ReadOnlySpan reference to internal storage
- TotalCount property to track relative index changes under mutation
- IEnumerable<> interface, remaining iterable under mutation
- Adding multiple items at once
- Removing items from either end
- Separate read-only and mutable interface types