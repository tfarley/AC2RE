using System.Collections.Generic;

namespace AC2RE.Definitions;

public class ElementDesc {

    public uint id; // elementID
    public uint type; // type
    public uint defaultState; // defaultState
    public ushort x0; // x0
    public ushort y0; // y0
    public ushort x1; // x1
    public ushort y1; // y1
    public ushort zLevel; // zLevel
    public bool leftEdge; // leftEdge
    public bool topEdge; // topEdge
    public bool rightEdge; // rightEdge
    public bool bottomEdge; // bottomEdge
    public List<StateDesc> states; // states

    public ElementDesc() {

    }

    public ElementDesc(AC2Reader data) {
        id = data.ReadUInt32();
        type = data.ReadUInt32();
        defaultState = data.ReadUInt32();
        x0 = data.ReadUInt16();
        y0 = data.ReadUInt16();
        x1 = data.ReadUInt16();
        y1 = data.ReadUInt16();
        zLevel = data.ReadUInt16();
        leftEdge = data.ReadByte() != 0;
        topEdge = data.ReadByte() != 0;
        rightEdge = data.ReadByte() != 0;
        bottomEdge = data.ReadByte() != 0;
        ushort numStates = data.ReadUInt16();
        data.Align(4);
        states = new();
        for (int i = 0; i < numStates; i++) {
            states.Add(new(data));
        }
    }

    public void write(AC2Writer data) {
        data.Write(id);
        data.Write(type);
        data.Write(defaultState);
        data.Write(x0);
        data.Write(y0);
        data.Write(x1);
        data.Write(y1);
        data.Write(zLevel);
        data.Write((byte)(leftEdge ? 1 : 0));
        data.Write((byte)(topEdge ? 1 : 0));
        data.Write((byte)(rightEdge ? 1 : 0));
        data.Write((byte)(bottomEdge ? 1 : 0));
        data.Write((ushort)states.Count);
        data.Align(4);
        foreach (StateDesc state in states) {
            state.write(data);
        }
    }
}
