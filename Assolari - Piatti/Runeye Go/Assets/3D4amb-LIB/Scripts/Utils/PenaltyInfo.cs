using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._3D4amb_LIB
{
    /// <summary>
    /// This object contains all the data to apply a penalty: 
    /// a trasparency, an eyepatch value and an eye to penalize
    /// </summary>
    /// <remarks>This is created and updated by PenaltyManager and other elements. 
    /// Do not create it manually</remarks>
    [Serializable]
    public class PenaltyInfo
    {
        /// <summary>
        /// Transparency applied on penalized objects
        /// </summary>
        /// <remarks>0: no trasparency, 1: object is invisibile</remarks>
        public float PenaltyTransparency;
        /// <summary>
        /// Transparency of the eye patched applied over the healthy eye
        /// </summary>
        /// <remarks>0: no eyepatch, 1: eyepatch completely obscure view</remarks>
        public float PenaltyEyePatch;
        /// <summary>
        /// The healthy eye, that's the one to penalize
        /// </summary>
        public Eye PenaltyEye;

        /// <summary>
        /// Ctor for the object, with all the values
        /// </summary>
        /// <param name="eye"><see cref="PenaltyEye"/></param>
        /// <param name="trans"><see cref="PenaltyTransparency"/></param>
        /// <param name="eyep"><see cref="PenaltyEyePatch"/></param>
        public PenaltyInfo(Eye eye, float trans, float eyep)
        {
            PenaltyEye = eye;
            PenaltyTransparency = trans;
            PenaltyEyePatch = eyep;
        }

        /// <summary>
        /// Ctor for the object, cloned from another PenaltyInfo object
        /// </summary>
        /// <param name="o">another PenaltyInfo object to copy the values from</param>
        public PenaltyInfo(PenaltyInfo o)
        {
            this.PenaltyTransparency = o.PenaltyTransparency;
            this.PenaltyEyePatch = o.PenaltyEyePatch;
            this.PenaltyEye = o.PenaltyEye;
        }

        /// <summary>
        /// Default ctor, with all 0 and left eye
        /// </summary>
        public PenaltyInfo()
        {
            this.PenaltyTransparency    = 0;
            this.PenaltyEyePatch        = 0;
            this.PenaltyEye             = Eye.LEFT;
        }

        public override string ToString()
        {
            return "[eye: " + PenaltyEye + "][trasp: " + PenaltyTransparency + "][eyep: " + PenaltyEyePatch + "]";
        }
    }
}
