namespace GeneratePlanets.Utillity
{
    public class MinMax 
    {
        public float Max { get; private set; }
        public float Min { get; private set; }
        
        public MinMax()
        {
            Max = float.MinValue;
            Min = float.MaxValue;
        }

        public void Addvalue(float value)
        {
            if (value > Max)
            {
                Max = value;
            }

            if (value < Min)
            {
                Min = value;
            }
        }
    }
}
