using GeneratePlanets.Settings;

namespace GeneratePlanets.NoiseFilters
{
    public static class NoiseFilterFactory 
    {
        public static INoiseFilter CreateNoiseFilter(NoiseSettings settings)
        {
            switch (settings.NoiseFilterType)
            {
                case NoiseFilterType.Rigid:
                    return new RigidNoiseFilter(settings.RigidNoiseSettings);
                case NoiseFilterType.Simple:
                    return new SimpleNoiseFilter(settings.SimpleNoiseSettings);
            }

            return null;
        }
    }
}
