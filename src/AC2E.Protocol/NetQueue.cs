﻿namespace AC2E.Protocol {

    public enum NetQueue : ushort {
        NET_QUEUE_INVALID,
        NET_QUEUE_EVENT,
        NET_QUEUE_CONTROL,
        NET_QUEUE_WEENIE,
        NET_QUEUE_LOGON,
        NET_QUEUE_DATABASE,
        NET_QUEUE_SECURECONTROL,
        NET_QUEUE_SECUREWEENIE,
        NET_QUEUE_SECURELOGON,
        NET_QUEUE_MAX,
    }
}
