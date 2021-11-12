using System;

namespace AC2RE.PdbTool {

    // NOTE: In order for build to succeed, run as Admin: regsvr32 "C:\Program Files\Microsoft Visual Studio\2022\Community\DIA SDK\bin\amd64\msdia140.dll"
    public class Program {

        public static void Main(string[] args) {
            if (args.Length < 1 || "help".Equals(args[0], StringComparison.OrdinalIgnoreCase)) {
                Console.WriteLine("Usage: pdbtool pdbfilepath");
                return;
            }

            string pdbFilePath = args[0];

            PdbParser pdbParser = new(pdbFilePath);
            PdbPrinter.print(pdbParser);
        }
    }
}
