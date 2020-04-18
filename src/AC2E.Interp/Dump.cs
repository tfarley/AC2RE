using System.Collections.Generic;
using System.IO;
using static AC2E.Interp.ByteStream;

namespace AC2E.Interp {

    public static class Dump {

        public static void dumpPackages(StreamWriter data, ByteStream byteStream) {
            Dictionary<string, ExportData> packageNameToExport = new Dictionary<string, ExportData>();
            foreach (ExportData export in byteStream.exports) {
                packageNameToExport.Add(export.args.name, export);
            }
            foreach (FrameDebugInfo frame in byteStream.frames) {
                if (frame.type == FrameDebugInfo.FrameType.PACKAGE) {
                    data.WriteLine();
                    data.Write($"package {frame.name}");
                    string baseName = packageNameToExport[frame.name].args.baseName;
                    if (baseName.Length > 0) {
                        data.Write($" : {baseName}");
                    }
                    data.WriteLine();

                    foreach (FrameMemberDebugInfo member in frame.members) {
                        data.WriteLine($"    {member.type} ({member.typeName}) {member.name}");
                    }
                }
            }
        }
    }
}
