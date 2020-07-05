using AC2E.Def;
using AC2E.Interp;
using AC2E.Utils;
using System.Collections.Generic;
using System.IO;

namespace AC2E {

    public static class BinaryWriterExtensions {

        private static readonly byte UNINITIALIZED_DATA = 0xCD;

        public static void Pack(this BinaryWriter writer, IPackage value) {
            List<IPackage> references = new List<IPackage>();
            references.Add(value);

            MemoryStream buffer = new MemoryStream();
            using (BinaryWriter data = new BinaryWriter(buffer)) {
                for (int i = 0; i < references.Count; i++) {
                    IPackage reference = references[i];
                    if (reference != null) {
                        writePackage(data, reference, references);
                    }
                }
            }

            for (int i = references.Count - 1; i >= 0; i--) {
                if (references[i] == null) {
                    references.RemoveAt(i);
                }
            }

            writer.Write((uint)PackTag.PACKAGE);
            writer.Write((uint)references.Count);
            foreach (IPackage reference in references) {
                writer.Write(reference.id);
            }
            writer.Write(buffer.ToArray());
        }

        private static void writePackage(BinaryWriter writer, IPackage value, List<IPackage> references) {
            int startReferencesCount = references.Count;

            writer.Write(value.referenceMeta.id);

            if (value.referenceMeta.isSingleton) {
                value.write(writer, references);
                return;
            }

            writer.Write((uint)0);
            if (value.nativeType != NativeType.UNDEF) {
                writer.Write((ushort)value.nativeType);
                writer.Write((ushort)0xFFFF);

                value.write(writer, references);
            } else {
                writer.Write((ushort)0);
                writer.Write((ushort)value.packageType);
                // Placeholder for length
                writer.Write((uint)0);

                long contentStart = writer.BaseStream.Position;
                value.write(writer, references);
                long contentEnd = writer.BaseStream.Position;
                long contentLength = contentEnd - contentStart;
                writer.BaseStream.Seek(-contentLength - 4, SeekOrigin.Current);
                writer.Write((uint)contentLength / 4);
                writer.BaseStream.Seek(contentEnd, SeekOrigin.Begin);
            }

            // Write out field descriptions
            if (value.packageType != PackageType.UNDEF && references.Count > startReferencesCount) {
                foreach (FieldDesc fieldDesc in InterpMeta.getFieldDescs(value.GetType())) {
                    writer.Write((byte)fieldDesc.fieldType);
                    if (fieldDesc.numWords == 2) {
                        writer.Write(UNINITIALIZED_DATA);
                    }
                }
                // TODO: Should this align occur even if there are no references?
                writer.Align(4);
            } else {
                // TODO: Is this needed/correct?
                writer.Write((uint)0);
            }
        }

        public static void Write(this BinaryWriter writer, IPackage value, List<IPackage> references) {
            writer.Write(value != null ? value.id : IPackage.NULL);
            references.Add(value);
        }
    }
}
