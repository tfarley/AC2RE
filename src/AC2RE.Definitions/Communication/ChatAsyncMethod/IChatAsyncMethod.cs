using System;

namespace AC2RE.Definitions;

public interface IChatAsyncMethod : IWritable {

    public static IChatAsyncMethod read(ChatAsyncMethodId id, ChatNetworkBlobType blobType, AC2Reader data) {
        bool isRequest = isRequestType(blobType);
        return id switch {
            ChatAsyncMethodId.SEND_TO_ROOM_BY_ID => isRequest ? new SendToRoomByIdChatRequestAsyncMethod(data) : new SendToRoomByIdChatResponseAsyncMethod(data),
            ChatAsyncMethodId.SEND_TO_ROOM_BY_NAME => isRequest ? new SendToRoomChatEvent(data) : null,
            _ => throw new NotImplementedException($"Unhandled chat async method: {id} with type: {blobType}."),
        };
    }

    private static bool isRequestType(ChatNetworkBlobType blobType) {
        return blobType == ChatNetworkBlobType.EVENT_BINARY
            || blobType == ChatNetworkBlobType.EVENT_XMLRPC
            || blobType == ChatNetworkBlobType.REQUEST_BINARY
            || blobType == ChatNetworkBlobType.REQUEST_XMLRPC;
    }
}
