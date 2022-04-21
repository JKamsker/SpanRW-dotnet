
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpanRW_dotnet
{
    public static class AlternativeSpanWriter
    {
        /// <summary>
        /// Copied from <see cref="BinaryWriter.Write(long)"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MMWriteInt64(this ref Span<byte> input, long value)
        {
            MemoryMarshal.Write(SpanWriterExtensions.Advance<long>(ref input), ref value);
        }


        /// <summary>
        /// Copied from <see cref="BinaryWriter.Write(ushort)"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MMWriteUShort(this ref Span<byte> input, ushort value)
        {
            MemoryMarshal.Write(SpanWriterExtensions.Advance<ushort>(ref input), ref value);
        }

        /// <summary>
        /// Copied from <see cref="BinaryWriter.Write(int)"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MMWriteInt32(this ref Span<byte> input, int value)
        {
            MemoryMarshal.Write(SpanWriterExtensions.Advance<int>(ref input), ref value);
        }
    }

    public static class UnsafeAfSpanWriter
    {
        /// <summary>
        /// Copied from <see cref="BinaryWriter.Write(long)"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MMXWriteInt64(this ref Span<byte> input, long value)
        {
            Unsafe.WriteUnaligned(ref Unsafe.Add(ref MemoryMarshal.GetReference(SpanWriterExtensions.Advance<long>(ref input)), 0), value);
        }


        /// <summary>
        /// Copied from <see cref="BinaryWriter.Write(ushort)"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MMXWriteUShort(this ref Span<byte> input, ushort value)
        {
            Unsafe.WriteUnaligned(ref Unsafe.Add(ref MemoryMarshal.GetReference(SpanWriterExtensions.Advance<ushort>(ref input)), 0), value);
        }

        /// <summary>
        /// Copied from <see cref="BinaryWriter.Write(int)"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MMXWriteInt32(this ref Span<byte> input, int value)
        {
            Unsafe.WriteUnaligned(ref Unsafe.Add(ref MemoryMarshal.GetReference(SpanWriterExtensions.Advance<int>(ref input)), 0), value);
        }
    }
}
