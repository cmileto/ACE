using System;
using ACE.Entity;
using ACE.Server.Physics.Common;

namespace ACE.Server.Physics.Animation
{
    public class AnimSequenceNode
    {
        public Animation Anim;
        public float Framerate;
        public int LowFrame;
        public int HighFrame;

        public AnimSequenceNode()
        {
            Framerate = 30.0f;
            LowFrame = -1;
            HighFrame = 1;
        }

        public AnimSequenceNode(AnimData animData)
        {
            Framerate = animData.Framerate;
            LowFrame = animData.LowFrame;
            HighFrame = animData.HighFrame;

            set_animation_id(animData.AnimId);
        }

        public AnimSequenceNode GetNext()
        {
            return null;
        }

        public AnimSequenceNode GetPev()
        {
            return null;
        }

        public float get_ending_frame()
        {
            if (Framerate > 0.0f)
                return HighFrame + 1 - PhysicsGlobals.EPSILON;
            else
                return LowFrame;
        }

        public int get_high_frame()
        {
            return HighFrame;
        }

        public int get_low_frame()
        {
            return LowFrame;
        }

        public AnimFrame get_part_frame(int frameIdx)
        {
            if (Anim == null) return null;
            if (frameIdx < 0 || frameIdx >= Anim.NumFrames)
                return null;

            return Anim.PartFrames[frameIdx];
        }

        public AFrame get_pos_frame(int frameIdx)
        {
            if (Anim == null) return null;
            if (frameIdx < 0 || frameIdx >= Anim.NumFrames)
                return null;

            return Anim.PosFrames[frameIdx];
        }

        public AFrame get_pos_frame(float frameIdx)
        {
            return get_pos_frame((int)Math.Floor(frameIdx));
        }

        public float get_starting_frame()
        {
            if (Framerate > 0.0f)
                return LowFrame;
            else
                return HighFrame + 1 - PhysicsGlobals.EPSILON;
        }

        public bool has_anim(int appraisalProfile = 0)
        {
            return Anim != null;
        }

        public void multiply_framerate(float multiplier)
        {
            if (multiplier < 0.0f)
            {
                var swap = LowFrame;
                LowFrame = HighFrame;
                HighFrame = swap;
            }
            Framerate *= multiplier;
        }

        public void set_animation_id(int animID)
        {
            Anim = (Animation)DBObj.Get(new QualifiedDataID(8, animID));
            if (Anim == null) return;

            if (HighFrame < 0)
                HighFrame = Anim.NumFrames - 1;

            if (LowFrame >= Anim.NumFrames)
                LowFrame = Anim.NumFrames - 1;

            if (HighFrame >= Anim.NumFrames)
                HighFrame = Anim.NumFrames - 1;

            if (LowFrame > HighFrame)
                HighFrame = LowFrame;
        }
    }
}