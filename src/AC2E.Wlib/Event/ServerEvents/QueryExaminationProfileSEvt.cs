﻿using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class QueryExaminationProfileSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Examination__QueryExaminationProfile;

        // WM_Examination::SendSEvt_QueryExaminationProfile
        public ExaminationRequest _inRequest;

        public QueryExaminationProfileSEvt(BinaryReader data) {
            _inRequest = data.UnpackPackage<ExaminationRequest>();
        }
    }
}
