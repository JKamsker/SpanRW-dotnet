
using System.Runtime.CompilerServices;
using System.Text;

namespace SpanRW_dotnet;

public ref struct SpanWriter
{
    private Span<byte> _span;

    public SpanWriter(Span<byte> span)
    {
        _span = span;
    }

    public SpanWriter(ref Span<byte> span)
    {
        _span = span;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteBoolean(bool value)
    {
        var original = Advance<byte>();
        original[0] = (byte)(value ? 1 : 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteByte(byte value)
    {
        var original = Advance<byte>();
        original[0] = value;
    }


    /// <summary>
    /// Copied from <see cref="BinaryWriter.Write(uint)"/>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteUInt32(uint value)
    {
        var original = Advance<uint>();
        original[0] = (byte)value;
        original[1] = (byte)(value >> 8);
        original[2] = (byte)(value >> 16);
        original[3] = (byte)(value >> 24);
    }

    /// <summary>
    /// Copied from <see cref="BinaryWriter.Write(float)"/>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe void WriteSingle(float value) => WriteUInt32(*(uint*)&value);

    /// <summary>
    /// Copied from <see cref="BinaryWriter.Write(ushort)"/>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteUInt16(ushort value)
    {
        var original = Advance<ushort>();
        original[0] = (byte)value;
        original[1] = (byte)(value >> 8);
    }

    /// <summary>
    /// Copied from <see cref="BinaryWriter.Write(ulong)"/>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteUInt64(ulong value)
    {
        var original = Advance<ulong>();
        original[0] = (byte)value;
        original[1] = (byte)(value >> 8);
        original[2] = (byte)(value >> 16);
        original[3] = (byte)(value >> 24);
        original[4] = (byte)(value >> 32);
        original[5] = (byte)(value >> 40);
        original[6] = (byte)(value >> 48);
        original[7] = (byte)(value >> 56);
    }

    /// <summary>
    /// Copied from <see cref="BinaryWriter.Write(long)"/>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteInt64(long value)
    {
        var original = Advance<long>();
        original[0] = (byte)value;
        original[1] = (byte)(value >> 8);
        original[2] = (byte)(value >> 16);
        original[3] = (byte)(value >> 24);
        original[4] = (byte)(value >> 32);
        original[5] = (byte)(value >> 40);
        original[6] = (byte)(value >> 48);
        original[7] = (byte)(value >> 56);
    }

    /// <summary>
    /// Copied from <see cref="BinaryWriter.Write(ushort)"/>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteUShort(ushort value)
    {
        var original = Advance<ushort>();
        original[0] = (byte)value;
        original[1] = (byte)(value >> 8);
    }

    /// <summary>
    /// Copied from <see cref="BinaryWriter.Write(int)"/>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteInt32(int value)
    {
        var original = Advance<int>();
        original[0] = (byte)value;
        original[1] = (byte)(value >> 8);
        original[2] = (byte)(value >> 16);
        original[3] = (byte)(value >> 24);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteNullableInt32(int? value)
    {
        WriteBoolean(value.HasValue);
        if (!value.HasValue)
        {
            return;
        }

        WriteInt32(value.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteString(string value, Encoding encoding = null)
    {
        encoding ??= Encoding.UTF8;

        var byteCount = encoding.GetByteCount(value.AsSpan());
        WriteInt32(byteCount);

        encoding.GetBytes(value, _span);
        _span = _span[byteCount..];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private unsafe Span<byte> Advance<T>() where T : unmanaged
    {
        return Advance(sizeof(T));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private unsafe Span<byte> Advance(int size)
    {
        var original = _span;
        var slized = original[size..];
        _span = slized;
        return original;
    }
}

