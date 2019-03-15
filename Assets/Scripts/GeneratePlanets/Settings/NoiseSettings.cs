using System;

namespace GeneratePlanets.Settings
{
    [Serializable]
    public class NoiseSettings
    {
        public NoiseFilterType NoiseFilterType;
        public SimpleNoiseSettings SimpleNoiseSettings;
        public RigidNoiseSettings RigidNoiseSettings;
    }
}
