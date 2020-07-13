using AC2E.Interp;
using System.IO;

namespace AC2E.WLib {

    public class QueryExaminationProfileSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Examination__QueryExaminationProfile;

        // WM_Examination::SendSEvt_QueryExaminationProfile
        public ExaminationRequestPkg _inRequest;

        public QueryExaminationProfileSEvt(BinaryReader data) {
            _inRequest = data.UnpackPackage<ExaminationRequestPkg>();
        }
    }
}
