namespace com.cobalt910.core.Runtime.Extension
{
    public static class FloatExtension
    {
        public static float Remap (this float from, float fromMin, float fromMax, float toMin,  float toMax)
        {
            var fromAbs  =  from - fromMin;
            var fromMaxAbs = fromMax - fromMin;      
       
            var normal = fromAbs / fromMaxAbs;
 
            var toMaxAbs = toMax - toMin;
            var toAbs = toMaxAbs * normal;
 
            var to = toAbs + toMin;
       
            return to;
        }
    }
}