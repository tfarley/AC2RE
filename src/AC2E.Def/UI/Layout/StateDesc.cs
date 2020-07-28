using System.Collections.Generic;

namespace AC2E.Def {

    public class StateDesc {

        public uint id; // elementID
        public byte flags; // flags
        public ushort x0; // x0
        public ushort y0; // y0
        public ushort x1; // x1
        public ushort y1; // y1
        public ushort zLevel; // zLevel
        public List<ElementDesc> children; // children
        public List<AttributeDesc> attributes; // attributes
        public List<MediaDesc> media; // media
        public List<TransitionDesc> transitions; // transition

        public StateDesc() {

        }

        public StateDesc(AC2Reader data) {
            id = data.ReadUInt32();
            flags = data.ReadByte();
            x0 = data.ReadUInt16();
            y0 = data.ReadUInt16();
            x1 = data.ReadUInt16();
            y1 = data.ReadUInt16();
            zLevel = data.ReadUInt16();
            data.Align(4);
            attributes = data.ReadList(() => new AttributeDesc(data));
            media = data.ReadList(() => new MediaDesc(data));
            transitions = data.ReadList(() => new TransitionDesc(data));
            children = data.ReadList(() => new ElementDesc(data));
        }

        public void write(AC2Writer data) {
            data.Write(id);
            data.Write(flags);
            data.Write(x0);
            data.Write(y0);
            data.Write(x1);
            data.Write(y1);
            data.Write(zLevel);
            data.Align(4);
            data.Write(attributes, v => v.write(data));
            data.Write(media, v => v.write(data));
            data.Write(transitions, v => v.write(data));
            data.Write(children, v => v.write(data));
        }
    }
}
