﻿namespace ProjectCore.Spline.Scripts.Spline
{
    public struct CurveDetail
    {
        public int currentCurve;
        public float currentCurvePercentage;

        public CurveDetail (int currentCurve, float currentCurvePercentage)
        {
            this.currentCurve = currentCurve;
            this.currentCurvePercentage = currentCurvePercentage;
        }
    }
}