using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Container : gmCEntity {

    public override PackageType packageType => PackageType.Container;

    public List<InstanceId> contents; // mContents
    public InstanceId transactionOnBehalfOfId; // m_transactionOnBehalfOfID
    public List<IHeapObject> segments; // mSegments
    public List<InstanceId> containers; // mContainers
    public bool hasBeenOpened; // mHasBeenOpened

    public Container(AC2Reader data) : base(data) {
        data.ReadHO<LList>(v => contents = v.to<InstanceId>());
        transactionOnBehalfOfId = data.ReadInstanceId();
        data.ReadHO<RList>(v => segments = v);
        data.ReadHO<LList>(v => containers = v.to<InstanceId>());
        hasBeenOpened = data.ReadBoolean();
    }
}
