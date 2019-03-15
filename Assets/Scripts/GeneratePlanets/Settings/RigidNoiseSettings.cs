using System;

namespace GeneratePlanets.Settings
{
    [Serializable]
    public class RigidNoiseSettings : SimpleNoiseSettings
    {
        public float WeightMultiplier = 0.8f;
    }
}