﻿namespace AC2RE.Definitions;

// Const NET_QUEUE_*
public enum NetQueue : ushort {
    INVALID, // NET_QUEUE_INVALID
    EVENT, // NET_QUEUE_EVENT
    CONTROL, // NET_QUEUE_CONTROL
    WEENIE, // NET_QUEUE_WEENIE
    LOGON, // NET_QUEUE_LOGON
    DATABASE, // NET_QUEUE_DATABASE
    SECURECONTROL, // NET_QUEUE_SECURECONTROL
    SECUREWEENIE, // NET_QUEUE_SECUREWEENIE
    SECURELOGON, // NET_QUEUE_SECURELOGON
    MAX, // NET_QUEUE_MAX
}
