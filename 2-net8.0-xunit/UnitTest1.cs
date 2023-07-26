using System.Diagnostics;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace xunittests;

public class UnitTest1
{
    private readonly ITestOutputHelper output;

    public UnitTest1(ITestOutputHelper output)
    {
        this.output = output;
    }
    [Fact]
    public async Task Test1()
    {
        output.WriteLine("Hello World!");
        var process = new Process
        {
            StartInfo = new ProcessStartInfo(@"C:\Users\maxschmitt\development\tmp\xunitconsole-workspace\assets\entrypoint.bat")
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = false,
                WorkingDirectory = @"C:\Users\maxschmitt\development\tmp\xunitconsole-workspace\assets",
            },
        };

        process.OutputDataReceived += (sender, args) => this.output.WriteLine(args.Data);
        process.Start();

        process.BeginOutputReadLine();

        var bytes = Encoding.UTF8.GetBytes("hello q");
        await process.StandardInput.BaseStream.WriteAsync(bytes, 0, bytes.Length);
        await process.StandardInput.BaseStream.FlushAsync();
        await process.WaitForExitAsync();
    }
}