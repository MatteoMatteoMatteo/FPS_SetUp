using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class src_Models
{
    #region - Player -

    [Serializable]
    public class PlayerSettingsModel
    {
        [Header("View Settings")]
        public float ViewXSensitivity;
        public float ViewYSensitivity;
        
        public bool ViewXInverted;
        public bool ViewYInverted;

        [Header("Movement")] 
        public float WalkingForwardSpeed;
        public float WalkingBackwardsSpeed;
        public float WalkingStrafeSpeed;
        
    }
    
    #endregion
}
