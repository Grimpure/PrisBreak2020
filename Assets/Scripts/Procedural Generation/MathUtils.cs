public class MathUtils 
{
    public static float Map(float s, float al, float a2, float b1, float b2)
    {
        return b1 + (s - al) * (b2 - b1) / (a2 - al);
    }
}
