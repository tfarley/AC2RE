using System.IO;

namespace AC2E.Def {

    public class EmptyCEvt : IClientEvent {

        public ClientEventFunctionId funcId { get; private set; }

        public EmptyCEvt(ClientEventFunctionId funcId) {
            this.funcId = funcId;
        }

        public void write(BinaryWriter data) {

        }
    }
}
