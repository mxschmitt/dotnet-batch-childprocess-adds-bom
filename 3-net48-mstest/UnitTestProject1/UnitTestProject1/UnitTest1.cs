using System.Diagnostics;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(@"C:\Users\maxschmitt\development\tmp\xunitconsole-workspace\assets\entrypoint.bat")
                {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = false,
                    UseShellExecute = false,
                    WorkingDirectory = @"C:\Users\maxschmitt\development\tmp\xunitconsole-workspace\assets",
                },
            };

            process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
            process.Start();

            process.BeginOutputReadLine();

            var bytes = Encoding.UTF8.GetBytes("hello q");
            await process.StandardInput.BaseStream.WriteAsync(bytes, 0, bytes.Length);
            await process.StandardInput.BaseStream.FlushAsync();
            process.WaitForExit();
        }
    }
}
