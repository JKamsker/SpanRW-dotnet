using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpanRW_dotnet
{
    public unsafe ref struct UnsafeSpanWriter
    {
        private readonly Span<byte> span;
        private nint pos;

        public readonly int Position => (int)pos;

        public UnsafeSpanWriter(Span<byte> buff)
        {
            span = buff;
            pos = default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write<T>(T val) where T : unmanaged
        {
            Unsafe.WriteUnaligned(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), pos), val);
            Advance<T>();
        }

        private void Advance<T>() where T : unmanaged
        {
            pos += sizeof(T);
        }
    }
}
