using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Running;

using SpanRW_dotnet;

[MemoryDiagnoser]
[InliningDiagnoser(true, true)]
[TailCallDiagnoser]
[EtwProfiler]
[DisassemblyDiagnoser]
//[ConcurrencyVisualizerProfiler]
//[NativeMemoryProfiler]
//[ThreadingDiagnoser]
public class SpanWriterTests
{
    private const int N = 100;
    private readonly byte[] data;



    public SpanWriterTests()
    {
        data = new byte[N * 15];
        new Random(42).NextBytes(data);
    }


    [Benchmark]
    public void TestSpanWriter()
    {
        var writer = new SpanWriter(data);
        for (int i = 0; i < N; i++)
        {
            writer.WriteUShort(ushort.MaxValue); //2
            writer.WriteBoolean(true); // 1
            writer.WriteInt32(123); // 4 
            writer.WriteInt64(456); // 8
        }
    }

    [Benchmark]
    public void TestUnsafeSpanWriter()
    {
        var writer = new UnsafeSpanWriter(data);
        for (int i = 0; i < N; i++)
        {
            writer.Write(ushort.MaxValue); //2
            writer.Write(true); // 1
            writer.Write((int)123); // 4 
            writer.Write((long)456); // 8
        }
    }

    [Benchmark]
    public void TestExtensions()
    {
        var writer = data.AsSpan();
        for (int i = 0; i < N; i++)
        {
            writer.WriteUShort(ushort.MaxValue); //2
            writer.WriteBoolean(true); // 1
            writer.WriteInt32(123); // 4 
            writer.WriteInt64(456); // 8
        }
    }

    [Benchmark]
    public void TestMemoryMarshalExtensions()
    {
        var writer = data.AsSpan();
        for (int i = 0; i < N; i++)
        {
            writer.MMWriteUShort(ushort.MaxValue); //2
            writer.WriteBoolean(true); // 1
            writer.MMWriteInt32(123); // 4 
            writer.MMWriteInt64(456); // 8
        }
    }

    [Benchmark]
    public void TestMemoryStream()
    {
        var memoryStream = new MemoryStream(data);
        memoryStream.Position = 0;
        var writer = new BinaryWriter(memoryStream);
        for (int i = 0; i < N; i++)
        {
            writer.Write(ushort.MaxValue); //2
            writer.Write(true); // 1
            writer.Write((int)123); // 4 
            writer.Write((long)456); // 8
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
    }
}