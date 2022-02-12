using System;
using System.Collections.Generic;
using System.IO;

namespace AC2RE.Definitions;

public class MediaDesc {

    // Const MEDIA_*
    public enum MediaType : uint {
        UNDEF, // MEDIA_UNDEF
        ANIMATION, // MEDIA_ANIMATION
        PAUSE, // MEDIA_PAUSE
        IMAGE, // MEDIA_IMAGE
        ALPHA, // MEDIA_ALPHA
        SOUND, // MEDIA_SOUND
        JUMP, // MEDIA_JUMP
        MESSAGE, // MEDIA_MESSAGE
        STATE, // MEDIA_STATE
        AVI, // MEDIA_AVI
        CURSOR, // MEDIA_CURSOR
    }

    public class AnimationMediaData : IWritable {

        // MD_Data_Anim
        public float duration; // m_duration
        public uint drawMode; // m_drawMode
        public int offsetX; // m_xOffset
        public int offsetY; // m_yOffset
        public List<uint> frames; // m_frames

        public AnimationMediaData() {

        }

        public AnimationMediaData(AC2Reader data) {
            duration = data.ReadSingle();
            drawMode = data.ReadUInt32();
            offsetX = data.ReadInt32();
            offsetY = data.ReadInt32();
            frames = data.ReadList(data.ReadUInt32);
        }

        public void write(AC2Writer data) {
            data.Write(duration);
            data.Write(drawMode);
            data.Write(offsetX);
            data.Write(offsetY);
            data.Write(frames, data.Write);
        }
    }

    public class PauseMediaData : IWritable {

        // MD_Data_Pause
        public float minDuration; // m_minDuration
        public float maxDuration; // m_maxDuration

        public PauseMediaData() {

        }

        public PauseMediaData(AC2Reader data) {
            minDuration = data.ReadSingle();
            maxDuration = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(minDuration);
            data.Write(maxDuration);
        }
    }

    public class ImageMediaData : IWritable {

        // MD_Data_Image
        public DataId imageDid; // m_file
        public uint drawMode; // m_drawMode
        public int offsetX; // m_xOffset
        public int offsetY; // m_yOffset

        public ImageMediaData() {

        }

        public ImageMediaData(AC2Reader data) {
            imageDid = data.ReadDataId();
            drawMode = data.ReadUInt32();
            offsetX = data.ReadInt32();
            offsetY = data.ReadInt32();
        }

        public void write(AC2Writer data) {
            data.Write(imageDid);
            data.Write(drawMode);
            data.Write(offsetX);
            data.Write(offsetY);
        }
    }

    public class AlphaMediaData : IWritable {

        // MD_Data_Alpha
        public DataId imageDid; // m_file
        public int offsetX; // m_xOffset
        public int offsetY; // m_yOffset

        public AlphaMediaData() {

        }

        public AlphaMediaData(AC2Reader data) {
            imageDid = data.ReadDataId();
            offsetX = data.ReadInt32();
            offsetY = data.ReadInt32();
        }

        public void write(AC2Writer data) {
            data.Write(imageDid);
            data.Write(offsetX);
            data.Write(offsetY);
        }
    }

    public class SoundMediaData : IWritable {

        // MD_Data_Sound
        public DataId soundDid; // m_soundDID

        public SoundMediaData() {

        }

        public SoundMediaData(AC2Reader data) {
            soundDid = data.ReadDataId();
        }

        public void write(AC2Writer data) {
            data.Write(soundDid);
        }
    }

    public class JumpMediaData : IWritable {

        // MD_Data_Jump
        public uint jumpItemIndex; // m_jumpItemIndex
        public float probability; // probability

        public JumpMediaData() {

        }

        public JumpMediaData(AC2Reader data) {
            jumpItemIndex = data.ReadUInt32();
            probability = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(jumpItemIndex);
            data.Write(probability);
        }
    }

    public class MessageMediaData : IWritable {

        // MD_Data_Message
        public uint messageId; // m_messageID
        public float probability; // probability

        public MessageMediaData() {

        }

        public MessageMediaData(AC2Reader data) {
            messageId = data.ReadUInt32();
            probability = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(messageId);
            data.Write(probability);
        }
    }

    public class StateMediaData : IWritable {

        // MD_Data_State
        public uint stateId; // m_stateID

        public StateMediaData() {

        }

        public StateMediaData(AC2Reader data) {
            stateId = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(stateId);
        }
    }

    public class CursorMediaData : IWritable {

        // MD_Data_Cursor
        public DataId cursorDid; // m_file
        public int hotspotX; // m_xHotspot
        public int hotspotY; // m_yHotspot

        public CursorMediaData() {

        }

        public CursorMediaData(AC2Reader data) {
            cursorDid = data.ReadDataId();
            hotspotX = data.ReadInt32();
            hotspotY = data.ReadInt32();
        }

        public void write(AC2Writer data) {
            data.Write(cursorDid);
            data.Write(hotspotX);
            data.Write(hotspotY);
        }
    }

    // MediaDesc
    public MediaType type; // m_type
    public IWritable mediaData;

    public MediaDesc() {

    }

    public MediaDesc(AC2Reader data) {
        // Duplicate line is intentional to match how actual code works
        type = data.ReadEnum<MediaType>();
        type = data.ReadEnum<MediaType>();
        mediaData = type switch {
            MediaType.ANIMATION => new AnimationMediaData(data),
            MediaType.PAUSE => new PauseMediaData(data),
            MediaType.IMAGE => new ImageMediaData(data),
            MediaType.ALPHA => new AlphaMediaData(data),
            MediaType.SOUND => new SoundMediaData(data),
            MediaType.JUMP => new JumpMediaData(data),
            MediaType.MESSAGE => new MessageMediaData(data),
            MediaType.STATE => new StateMediaData(data),
            MediaType.AVI => throw new NotImplementedException(type.ToString()),
            MediaType.CURSOR => new CursorMediaData(data),
            _ => throw new InvalidDataException(type.ToString()),
        };
    }

    public void write(AC2Writer data) {
        // Duplicate line is intentional to match how actual code works
        data.WriteEnum(type);
        data.WriteEnum(type);
        mediaData.write(data);
    }
}
