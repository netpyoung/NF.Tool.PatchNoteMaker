﻿using NF.Tool.ReleaseNoteMaker.Common.Config;
using System;
using System.Diagnostics.CodeAnalysis;

namespace NF.Tool.ReleaseNoteMaker.Common.Template
{
    public sealed class VersionData
    {
        public string Name { get; init; } = string.Empty;
        public string Version { get; init; } = string.Empty;
        public string Date { get; init; } = string.Empty;

        private VersionData()
        {
        }

        public VersionData(string name, string version, string date)
        {
            Name = name;
            Version = version;
            Date = date;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return $"{Version} ({Date})";
            }
            return $"{Name} {Version} ({Date})";
        }

        public static (Exception? exOrNull, VersionData versionData) GetVersionData([NotNull] ReleaseNoteConfig config, (string ProjectName, string ProjectVersion, string ProjectDate) setting)
        {
            string projectVersion;
            if (!string.IsNullOrEmpty(setting.ProjectVersion))
            {
                projectVersion = setting.ProjectVersion;
            }
            else if (!string.IsNullOrEmpty(config.Maker.Version))
            {
                projectVersion = config.Maker.Version;
            }
            else
            {
                ReleaseNoteMakerException ex = new ReleaseNoteMakerException("'--version'[/] is required since the config file does not contain 'version.");
                return (ex, new VersionData(string.Empty, string.Empty, string.Empty));
            }

            string projectName;
            if (!string.IsNullOrEmpty(setting.ProjectName))
            {
                projectName = setting.ProjectName;
            }
            else if (!string.IsNullOrEmpty(config.Maker.Name))
            {
                projectName = config.Maker.Name;
            }
            else
            {
                projectName = string.Empty;
            }

            string projectDate;
            if (!string.IsNullOrEmpty(setting.ProjectDate))
            {
                projectDate = setting.ProjectDate;
            }
            else
            {
                projectDate = DateTime.Today.ToString("yyyy-MM-dd");
            }

            VersionData versionData = new VersionData(projectName, projectVersion, projectDate);
            return (null, versionData);
        }
    }
}
