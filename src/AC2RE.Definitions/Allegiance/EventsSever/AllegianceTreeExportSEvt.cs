﻿namespace AC2RE.Definitions;

public class AllegianceTreeExportSEvt : IServerEvent {

    public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__AllegianceTreeExport;

    // WM_Allegiance::SendSEvt_AllegianceTreeExport
    public WPString fileName; // _filename

    public AllegianceTreeExportSEvt(AC2Reader data) {
        fileName = data.UnpackHeapObject<WPString>();
    }
}
