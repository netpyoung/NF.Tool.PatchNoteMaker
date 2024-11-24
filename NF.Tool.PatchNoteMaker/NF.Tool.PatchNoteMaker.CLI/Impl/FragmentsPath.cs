﻿using NF.Tool.PatchNoteMaker.Common.Config;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace NF.Tool.PatchNoteMaker.CLI.Impl
{
    public record struct FragmentsPath
    {
        private readonly string _baseDirectory;
        private readonly string _appendDirectory;
        private readonly PatchNoteConfig _config;

        public static FragmentsPath Get(string baseDirectory, [NotNull] PatchNoteConfig config)
        {
            return new FragmentsPath(baseDirectory, config);
        }

        [Obsolete("Do not use constructor", error: true)]
        public FragmentsPath()
        {
            throw new InvalidOperationException();
        }

        private FragmentsPath(string baseDirectory, PatchNoteConfig config)
        {
            _config = config;
            if (!string.IsNullOrEmpty(config.Maker.Directory))
            {
                _baseDirectory = Path.Combine(baseDirectory, config.Maker.Directory);
                _appendDirectory = string.Empty;
            }
            else
            {
                _baseDirectory = baseDirectory;
                _appendDirectory = "newsfragments";
            }
        }

        public string Resolve(string section)
        {
            return Path.Combine(_baseDirectory, section);
        }

        public string GetDirectory(string sectionPath)
        {
            PatchNoteSection? sectionOrNull = _config.Sections.Find(x => Utils.IsSameIgnoreCase(x.Path, sectionPath))!;
            Debug.Assert(sectionOrNull != null, $"sectionOrNull != null | sectionPath: {sectionPath}");

            string dir = Path.Combine(_baseDirectory, sectionOrNull.Path, _appendDirectory);
            return dir;
        }
    }
}