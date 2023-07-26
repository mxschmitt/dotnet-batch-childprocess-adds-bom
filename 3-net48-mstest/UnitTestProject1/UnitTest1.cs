using System.Diagnostics;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.IO;

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
                StartInfo = new ProcessStartInfo(@"C:\Program Files\nodejs\node.exe")
                {
                    Arguments = @"C:\Users\maxschmitt\development\tmp\xunitconsole-workspace\assets\index.js",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = false,
                    UseShellExecute = false,
                },
            };

            process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
            process.Start();

            process.BeginOutputReadLine();

            StreamWriter writer = new StreamWriter(process.StandardInput.BaseStream, new UTF8Encoding(false));
            writer.Write("hello q");
            writer.Flush();

            process.WaitForExit();
        }
    }
}
