﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class ExaminationProfile : IHeapObject {

    public NativeType nativeType => NativeType.ExaminationProfile;

    public ExaminationRequest request; // _request
    public List<ExaminationDataNode> nodes; // _nodeList
    public uint unk1;

    public ExaminationProfile() {

    }

    public ExaminationProfile(AC2Reader data) {
        request = new(data);
        nodes = data.ReadList(() => new ExaminationDataNode(data));
        unk1 = data.ReadUInt32();
    }

    public void write(AC2Writer data) {
        request.write(data);
        data.Write(nodes, v => v.write(data));
        data.Write(unk1);
    }
}
