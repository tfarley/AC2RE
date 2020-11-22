namespace AC2RE.Definitions {

    public class QueryAllegianceProfileSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Allegiance__QueryAllegianceProfile;

        // WM_Allegiance::SendSEvt_QueryAllegianceProfile
        public InstanceId targetId; // _trg

        public QueryAllegianceProfileSEvt(AC2Reader data) {
            targetId = data.UnpackInstanceId();
        }
    }
}
