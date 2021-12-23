using System;

namespace AC2RE.PdbTool;

// NOTE: If this does not build due to: "Warning MSB3284" then see PdbParser.cs for instructions
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
