namespace AC2RE.Definitions {

    public struct MotionValues {

        public float lift; // mLift
        public float drag; // mDrag
        public float terminalVel; // mTerminalVel
        public float sinkOffset; // mSinkOffset
        public float maxForwardVel; // mMaxForwardVel
        public float maxBackwardVel; // mMaxBackwardVel
        public float maxStrafeVel; // mMaxStrafeVel
        public float maxVertVel; // mMaxVertVel
        public float stillTurnVel; // mStillTurnVel
        public float runningTurnVel; // mRunningTurnVel
        public float forwardAccel; // mForwardAcl
        public float backwardAccel; // mBackwardAcl
        public float strafeAccel; // mStrafeAcl
        public float turnAccel; // mTurnAcl
        public float vertAccel; // mVertAcl
        public float forwardSB; // mForwardSB
        public float backwardSB; // mBackwardSB
        public float strafeSB; // mStrafeSB
        public float turnSB; // mTurnSB
        public float jumpImpulse; // mJumpImpulse
        public float hoverHeight; // mHoverHeight
        public bool gravity; // mGravity

        public MotionValues(AC2Reader data) {
            lift = data.ReadSingle();
            drag = data.ReadSingle();
            terminalVel = data.ReadSingle();
            gravity = data.ReadBoolean();
            sinkOffset = data.ReadSingle();
            hoverHeight = data.ReadSingle();
            jumpImpulse = data.ReadSingle();
            maxForwardVel = data.ReadSingle();
            maxBackwardVel = data.ReadSingle();
            maxStrafeVel = data.ReadSingle();
            stillTurnVel = data.ReadSingle();
            runningTurnVel = data.ReadSingle();
            maxVertVel = data.ReadSingle();
            forwardSB = data.ReadSingle();
            backwardSB = data.ReadSingle();
            strafeSB = data.ReadSingle();
            turnSB = data.ReadSingle();
            forwardAccel = data.ReadSingle();
            backwardAccel = data.ReadSingle();
            strafeAccel = data.ReadSingle();
            turnAccel = data.ReadSingle();
            vertAccel = data.ReadSingle();
        }
    }
}
