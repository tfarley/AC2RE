﻿using System.Collections.Generic;
using System.IO;
using static AC2RE.Definitions.ByteStream;

namespace AC2RE.Definitions {

    public static class Dump {

        public static void dumpPackages(StreamWriter data, ByteStream byteStream) {
            Dictionary<string, ExportData> packageNameToExport = new();
            foreach (ExportData export in byteStream.exports) {
                packageNameToExport.Add(export.args.name, export);
            }
            foreach (FrameDebugInfo frame in byteStream.frames) {
                if (frame.type == FrameDebugInfo.FrameType.PACKAGE) {
                    ExportData export = packageNameToExport[frame.name];
                    data.WriteLine();
                    data.Write($"{export.args.packageType} package {frame.name}");
                    if (export.args.parentIndex != -1) {
                        data.Write($" : {export.args.baseName} - 0x{export.args.parentIndex:X8}");
                    }
                    data.Write($" size {export.args.size} checksum {export.args.checksum} [{export.args.flags}]");
                    data.WriteLine();

                    foreach (FrameMemberDebugInfo member in frame.members) {
                        data.WriteLine($"    {member.type} ({member.typeName}) {member.name}");
                    }
                }
            }
        }
    }
}