﻿using AC2E.Def;
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
                    IPackage referencedPackage = references[i];
                    if (referencedPackage != null) {
                        writePackage(data, referencedPackage, references);
                    }
                }
            }

            for (int i = references.Count - 1; i >= 0; i--) {
                if (references[i] == null) {
                    references.RemoveAt(i);
                }
            }

            writer.Write((uint)PackTag.PACKAGE);
            writer.Write(references, v => writer.Write(PackageManager.registry.getId(v)));
            writer.Write(buffer.ToArray());
        }

        private static void writePackage(BinaryWriter writer, IPackage value, List<IPackage> references) {
            InterpReferenceMeta referenceMeta = PackageManager.registry.getMeta(value).referenceMeta;

            writer.Write(referenceMeta.id);

            if (referenceMeta.isSingleton) {
                value.write(writer, references);
                return;
            }

            writer.Write((uint)0);
            writer.Write((ushort)value.nativeType);
            writer.Write(value.nativeType != NativeType.UNDEF ? (ushort)0xFFFF : (ushort)value.packageType);
            if (value.nativeType != NativeType.UNDEF) {
                value.write(writer, references);
            } else {
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

            // TODO: Still not sure this is the correct condition for whether there are references or not
            // Write out field descriptions
            if (value.nativeType == NativeType.UNDEF) {
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

        public static void Write<T>(this BinaryWriter writer, T value, List<IPackage> references) where T : IPackage {
            if (value != null) {
                writer.Write(PackageManager.registry.getId(value));
                references.Add(value);
            } else {
                writer.Write(PackageId.NULL);
            }
        }
    }
}
