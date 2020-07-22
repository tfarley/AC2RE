﻿namespace AC2E.Def {

    public class QueryExaminationProfileSEvt : IServerEvent {

        public ServerEventFunctionId funcId => ServerEventFunctionId.Examination__QueryExaminationProfile;

        // WM_Examination::SendSEvt_QueryExaminationProfile
        public ExaminationRequest _inRequest;

        public QueryExaminationProfileSEvt(AC2Reader data) {
            _inRequest = data.UnpackPackage<ExaminationRequest>();
        }
    }
}