using System.Diagnostics;
using System.Text;

var process = new Process
{
    StartInfo = new ProcessStartInfo(@"C:\Program Files\nodejs\node.exe")
{
        Arguments = @"C:\Users\maxschmitt\development\tmp\xunitconsole-workspace\assets\index.js",
        RedirectStandardInput = true,
        RedirectStandardOutput = false,
        RedirectStandardError = false,
    },
};

process.Start();

var bytes = Encoding.UTF8.GetBytes("hello q");
await process.StandardInput.BaseStream.WriteAsync(bytes, 0, bytes.Length);
await process.StandardInput.BaseStream.FlushAsync();
await process.WaitForExitAsync();
