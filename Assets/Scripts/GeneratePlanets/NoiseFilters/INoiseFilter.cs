﻿using UnityEngine;

namespace GeneratePlanets.NoiseFilters
{
    public interface INoiseFilter
    {
        float Evaluate(Vector3 point);
    }
}
