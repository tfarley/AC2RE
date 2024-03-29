﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class UpdateCraftSkillsDoneCEvt : IClientEvent {

    public ClientEventFunctionId funcId => ClientEventFunctionId.Craft__UpdateCraftSkills_Done;

    // WM_Craft::PostCEvt_UpdateCraftSkills_Done
    public List<CraftSkillRecord> craftSkillRecords; // _listCraftSkillRecs

    public UpdateCraftSkillsDoneCEvt() {

    }

    public UpdateCraftSkillsDoneCEvt(AC2Reader data) {
        craftSkillRecords = data.UnpackHeapObject<RList>().to<CraftSkillRecord>();
    }

    public void write(AC2Writer data) {
        data.Pack(RList.from(craftSkillRecords));
    }
}
