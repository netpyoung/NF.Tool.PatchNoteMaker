# NF.Tool.ReleaseNoteMaker

[![GitHub](https://img.shields.io/badge/GitHub-%23121011.svg?logo=github&logoColor=white)](https://github.com/netpyoung/NF.Tool.ReleaseNoteMaker)
[![.NET Test Workflow](https://github.com/netpyoung/NF.Tool.ReleaseNoteMaker/actions/workflows/dotnet-test.yml/badge.svg)](https://github.com/netpyoung/NF.Tool.ReleaseNoteMaker/actions/workflows/dotnet-test.yml)
[![Document](https://img.shields.io/badge/document-docfx-blue)](https://netpyoung.github.io/NF.Tool.ReleaseNoteMaker/)
[![License](https://img.shields.io/badge/license-MIT-C06524)](https://github.com/netpyoung/NF.Tool.ReleaseNoteMaker/blob/main/LICENSE.md)
[![NuGet](https://img.shields.io/nuget/v/dotnet-release-note.svg?style=flat&label=NuGet%3A%20dotnet-release-note)](https://www.nuget.org/packages/dotnet-release-note/)

wip

based on python [TownCrier](https://github.com/twisted/towncrier)

- [doc](https://netpyoung.github.io/NF.Tool.ReleaseNoteMaker/)

## Install

``` bash
dotnet tool install --global dotnet-release-note
```

## used

- use [Toml format](https://toml.io/en/) and [xoofx/Tomlyn library](https://github.com/xoofx/Tomlyn) for Config file.
- use [T4 template](https://learn.microsoft.com/en-us/visualstudio/modeling/code-generation-and-t4-text-templates) and [mono/t4 library](https://github.com/mono/t4).
- use [Spectre.Console](https://spectreconsole.net/) for console output.
- use [Spectre.Console.Cli](https://spectreconsole.net/cli/) for parse args.
- use [SmartFormat](https://github.com/axuno/SmartFormat) for format string.