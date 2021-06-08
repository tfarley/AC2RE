using System;
using System.Collections.Generic;
using System.IO;

namespace AC2RE.PacketTool.UI {

    internal static class PacketUtil {

        public static void processAllPcaps(string filePath, Action<string, NetBlobCollection> netBlobProcessor) {
            List<string> allPcapFileNames = new();

            allPcapFileNames.AddRange(Directory.GetFiles(filePath, "*.pcap", SearchOption.AllDirectories));
            allPcapFileNames.AddRange(Directory.GetFiles(filePath, "*.pcapng", SearchOption.AllDirectories));

            foreach (string pcapFileName in allPcapFileNames) {
                using (FileStream pcapFileStream = File.Open(pcapFileName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                    NetBlobCollection netBlobCollection = PcapReader.read(pcapFileStream);
                    netBlobProcessor.Invoke(pcapFileName, netBlobCollection);
                }
            }
        }
    }
}
