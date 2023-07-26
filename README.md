# .NET child processes BOM (Byte Order Mark) gets added

## net8.0 console app

Issue does not reproduce there

Repro steps:

```bash
chcp 65001
cd 1-net8.0-console-app
dotnet run Program.cs
```

Expected: No BOM
Actual: No BOM (as expected, a BOM is if there are bytes before the 'hello' string)

## net8.0 XUnit tests

Issue does not reproduce there

Repro steps:

Open a `Developer Command Prompt for VS 2022`:

```bash
chcp 65001
cd 2-net8.0-xunit
dotnet build
vstest.console bin\Debug\net8.0\xunittests.dll /Logger:trx
```

Open the `trx` file and observe, that there is no BOM.

Expected: No BOM
Actual: No BOM (as expected, a BOM is if there are bytes before the 'hello' string)

## net48 xunit tests

Issue does reproduce there

Repro steps:

Open a `Developer Command Prompt for VS 2022`:

```bash
chcp 65001
cd 3-net48-mstest
dotnet build
vstest.console UnitTestProject1\bin\Debug\UnitTestProject1.dll /Logger:trx
```

Open the `trx` file and observe, that there is a BOM:

```
<StdOut>C:\Users\maxschmitt\development\tmp\xunitconsole-workspace\assets&gt;node "C:\Users\maxschmitt\development\tmp\xunitconsole-workspace\assets\index.js" &#xD;
hello from Node.js&#xD;
Received 10 bytes of data.&#xD;
Received 239 (ï)&#xD; <--
Received 187 (»)&#xD; <--
Received 191 (¿)&#xD; <--
Received 104 (h)&#xD;
Received 101 (e)&#xD;
Received 108 (l)&#xD;
Received 108 (l)&#xD;
Received 111 (o)&#xD;
Received 32 ( )&#xD;
Received 113 (q)&#xD;
Exiting</StdOut>
```

Expected: No BOM
Actual: BOM (there are bytes before the 'hello' string)

## Summary

Related to [this](https://stackoverflow.com/questions/2855675/encoding-problem-of-process-standardinput-or-application-executed-from-c-sharp-c) StackOverflow post, so it can be solved by using `chcp` or by setting `Console.InputEncoding = new UTF8Encoding(false);` before the process gets started.
