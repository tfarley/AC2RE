﻿using System.Text;

namespace AC2RE.Definitions {

    public class WPString : IPackage {

        public NativeType nativeType => NativeType.wpstring;

        public string contents;

        public WPString() {

        }

        public WPString(AC2Reader data) {
            contents = data.ReadString(Encoding.Unicode);
        }

        public void write(AC2Writer data) {
            data.Write(contents, Encoding.Unicode);
        }

        public override string ToString() {
            return contents;
        }
    }
}
