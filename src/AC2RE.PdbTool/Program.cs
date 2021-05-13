using System;

namespace AC2RE.PdbTool {

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
