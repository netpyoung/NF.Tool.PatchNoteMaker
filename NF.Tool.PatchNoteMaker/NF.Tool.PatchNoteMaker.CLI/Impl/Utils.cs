﻿using NF.Tool.PatchNoteMaker.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Tomlyn;
using Tomlyn.Syntax;

namespace NF.Tool.PatchNoteMaker.CLI.Impl
{
    internal static class Utils
    {
        public static string ExtractResourceText(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string tempFilePath = Path.GetTempFileName();
            using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName)!)
            {
                Debug.Assert(resourceStream != null);
                using (FileStream fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                {
                    resourceStream.CopyTo(fileStream);
                }
            }
            return tempFilePath;
        }

        public static Exception? GetConfig(string directory, string configPath, out string baseDirectory, out PatchNoteConfig config)
        {
            // TODO(pyoung): refactoring without throw
            if (string.IsNullOrEmpty(configPath))
            {
                return TraverseToParentForConfig(directory, out baseDirectory, out config);
            }

            string configFpath = Path.GetFullPath(configPath);
            if (!string.IsNullOrEmpty(directory))
            {
                baseDirectory = Path.GetFullPath(directory);
            }
            else
            {
                baseDirectory = Path.GetDirectoryName(configFpath)!;
            }

            if (!File.Exists(configFpath))
            {
                config = new PatchNoteConfig();
                return new PatchNoteMakerException($"Configuration file '{configFpath}' not found.");
            }

            (Exception? exOrNull, config) = LoadConfigFromFile(configFpath);
            return exOrNull;
        }

        private static Exception? TraverseToParentForConfig(string path, out string directoryFpath, out PatchNoteConfig config)
        {
            string startDirectoryFpath;
            if (!string.IsNullOrEmpty(path))
            {
                startDirectoryFpath = Path.GetFullPath(path);
            }
            else
            {
                startDirectoryFpath = Directory.GetCurrentDirectory();
            }

            directoryFpath = startDirectoryFpath;
            while (true)
            {
                string configFpath = Path.Combine(directoryFpath, Const.DEFAULT_CONFIG_FILENAME);
                if (File.Exists(configFpath))
                {
                    (Exception? exOrNull, config) = LoadConfigFromFile(configFpath);
                    return exOrNull;
                }

                DirectoryInfo? parentOrNull = Directory.GetParent(directoryFpath);
                if (parentOrNull == null)
                {
                    config = new PatchNoteConfig();
                    return new PatchNoteMakerException($"No configuration file found. Looked back from: {startDirectoryFpath}");
                }
                directoryFpath = parentOrNull.FullName;
            }
        }

        private static (Exception? exOrNull, PatchNoteConfig config) LoadConfigFromFile(string configFpath)
        {
            string configText = File.ReadAllText(configFpath);
            TomlModelOptions option = new TomlModelOptions();
            option.ConvertFieldName = StringIdentity;
            option.ConvertPropertyName = StringIdentity;

            bool isSuccess = Toml.TryToModel(configText, out TomlModel? modelOrNull, out DiagnosticsBag? diagostics, options: option);
            if (!isSuccess)
            {
                Console.Error.WriteLine($"configFpath: {configFpath}");
                foreach (DiagnosticMessage x in diagostics!)
                {
                    Console.Error.WriteLine(x);
                }
                Environment.Exit(1);
            }

            TomlModel model = modelOrNull!;
            PatchNoteConfig patchNoteConfig = model.PatchNote;
            return (null, patchNoteConfig);
        }

        private static string StringIdentity(string x)
        {
            return x;
        }
    }
}
