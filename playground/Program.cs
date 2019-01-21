using NLog;
using System;
using System.Collections.Generic;
//using CommandLine;

namespace Playground
{
    class Program
    {
        //class CliArgs
        //{
        //    [Option('r', "read", Required = true, HelpText = "Input files to be processed.")]
        //    public IEnumerable<string> InputFiles { get; set; }

        //    // Omitting long name, defaults to name of property, ie "--verbose"
        //    [Option(Default = false, HelpText = "Prints all messages to standard output.")]
        //    public bool Verbose { get; set; }

        //    [Option("stdin", Default = false, HelpText = "Read from stdin")]
        //    public bool Stdin { get; set; }

        //    [Value(0, MetaName = "offset", HelpText = "File offset.")]
        //    public long? Offset { get; set; }
        //}

        static void Main(string[] args)
        {
            //Parser.Default.ParseArguments<CliArgs>(args)
            //    .WithParsed(RunAndReturnExitCode)
            //    .WithNotParsed(HandleParseError);

            Foo();
            Console.ReadKey();
        }

        private static void Foo()
        {
            //MappedDiagnosticsContext.SetScoped
        }

        //private static void RunAndReturnExitCode(CliArgs cli)
        //{
        //}

        //private static void HandleParseError(IEnumerable<Error> argParseErrors)
        //{
        //}
    }
}
