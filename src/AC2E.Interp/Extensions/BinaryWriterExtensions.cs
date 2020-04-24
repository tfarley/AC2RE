using AC2E.Def.Enums;
using AC2E.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp.Extensions {

    public static class BinaryWriterExtensions {

        private static readonly byte UNINITIALIZED_DATA = 0xCD;

        public static void Pack(this BinaryWriter writer, IPackage value) {
            List<IPackage> nonNullReferences = new List<IPackage>();
            foreach (IPackage reference in value.references) {
                if (reference != null) {
                    nonNullReferences.Add(reference);
                }
            }

            writer.Write((uint)PackTag.PACKAGE);
            writer.Write((uint)nonNullReferences.Count + 1);
            writer.Write(value.id);
            foreach (IPackage reference in nonNullReferences) {
                writer.Write(reference.id);
            }
            packPackage(writer, value, nonNullReferences);
            foreach (IPackage reference in nonNullReferences) {
                // TODO: Need to keep nesting?
                packPackage(writer, reference, new List<IPackage>());
            }
        }

        private static void packPackage(BinaryWriter writer, IPackage value, List<IPackage> references) {
            writer.Write(value.reference);
            if (value.reference.isSingleton) {
                value.write(writer);
                return;
            }
            writer.Write((uint)0);
            if (value.nativeType != NativeType.UNDEF) {
                writer.Write((ushort)value.nativeType);
                writer.Write((ushort)0xFFFF);

                value.write(writer);
            } else {
                writer.Write((ushort)0);
                writer.Write((ushort)value.packageType);
                // Placeholder for length
                writer.Write((uint)0);

                long contentStart = writer.BaseStream.Position;
                value.write(writer);
                long contentEnd = writer.BaseStream.Position;
                long contentLength = contentEnd - contentStart;
                writer.BaseStream.Seek(-contentLength - 4, SeekOrigin.Current);
                writer.Write((uint)contentLength / 4);
                writer.BaseStream.Seek(contentEnd, SeekOrigin.Begin);
            }

            if (references.Count > 0) {
                foreach (FieldDesc fieldDesc in value.fieldDescs) {
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
    }
}
