﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NF.Tool.PatchNoteMaker.Common
{
    public sealed class PatchNoteConfig
    {
        public PatchNoteMaker Maker { get; private set; } = new PatchNoteMaker();

        [DataMember(Name = "Type")]
        public List<PatchNoteType> Types { get; private set; } = new List<PatchNoteType>(20);

        [DataMember(Name = "Section")]
        public List<PatchNoteSection> Sections { get; private set; } = new List<PatchNoteSection>(20) {
            new PatchNoteSection { Name= "", Path="" },
            new PatchNoteSection { Name= "A", Path="A" },
            new PatchNoteSection { Name= "B", Path="B" },
        };

        public sealed class PatchNoteMaker
        {
            public string Directory { get; set; } = string.Empty;
            public string OutputFileName { get; set; } = string.Empty;
            public string Version { get; set; } = string.Empty;
            public string Package { get; set; } = string.Empty;
            public string TemplateFilePath { get; set; } = string.Empty;
            public List<string> Ignores { get; set; } = new List<string>();
            public string OrphanPrefix { get; set; } = "+";
            public string IssuePattern { get; set; } = string.Empty;
        }

        public sealed class PatchNoteSection
        {
            public string Name { get; set; } = string.Empty;
            public string Path { get; set; } = string.Empty;
        }

        public sealed class PatchNoteType
        {
            public string Name { get; set; } = string.Empty;
            public bool IsShowcontent { get; set; }
            public string Directory { get; set; } = string.Empty;
        }
    }
}
