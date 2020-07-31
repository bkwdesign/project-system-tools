﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.VisualStudio.ProjectSystem.Tools.BuildLogging.Model.Backend
{
    /// <summary>
    /// Immutable Type
    /// </summary>
    public sealed class BuildSummary : IBuildSummary
    {
        public int BuildID { get; }

        public BuildType BuildType { get; }

        public IEnumerable<string> Dimensions { get; }

        public IEnumerable<string> Targets { get; }

        public DateTime StartTime { get; }

        public TimeSpan Elapsed { get; }

        public BuildStatus Status { get; }

        public string ProjectPath { get; }

        public BuildSummary(int buildID, string projectPath, IEnumerable<string> dimensions, IEnumerable<string> targets, BuildType buildType, DateTime startTime)
        {
            BuildID = buildID;
            ProjectPath = projectPath;
            Dimensions = dimensions.ToArray();
            Targets = targets?.ToArray() ?? Enumerable.Empty<string>();
            BuildType = buildType;
            StartTime = startTime;
            Status = BuildStatus.Running;
        }
        public BuildSummary(IBuildSummary other, BuildStatus status, TimeSpan elapsed) {
            BuildID = other.BuildID;
            BuildType = other.BuildType;
            // TODO: Do the IEnumerable types need deep copying?
            Dimensions = other.Dimensions;
            Targets = other.Targets;

            StartTime = other.StartTime;
            ProjectPath = other.ProjectPath;

            Elapsed = elapsed;
            Status = status;
        }

        public int CompareTo(IBuildSummary other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (other is null)
            {
                return 1;
            }

            var startComparison = StartTime.CompareTo(other.StartTime);
            return startComparison != 0 ? startComparison : string.Compare(ProjectPath, other.ProjectPath, StringComparison.Ordinal);
        }
    }
}
