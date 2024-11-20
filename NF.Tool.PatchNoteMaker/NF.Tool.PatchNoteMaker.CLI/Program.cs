﻿using NF.Tool.PatchNoteMaker.CLI.Commands;
using NF.Tool.PatchNoteMaker.CLI.Impl;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Threading.Tasks;

namespace NF.Tool.PatchNoteMaker.CLI
{
    internal sealed class Program
    {
        static async Task<int> Main(string[] args)
        {
            args = "create --help".Split(" ");
            //args = "create".Split(" ");

            CommandApp app = new CommandApp();

            app.Configure(config =>
            {
                config.PropagateExceptions();

                config.AddCommand<Command_Init>("init")
                    .WithExample("init")
                    .WithExample("init", "--file", Const.DEFAULT_CONFIG_FILENAME);
                config.AddCommand<Command_Create>("create")
                    .WithExample("create", "--edit")
                    .WithExample("create", "--content", @"""Hello World""", "1.added.md");
                config.AddCommand<Command_Build>("build")
                   .WithExample("build");
            });

            try
            {
                return await app.RunAsync(args);
            }
            catch (Exception ex)
            {
                app.Run(["--help"]);
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                return 1;
            }
        }
    }
}
