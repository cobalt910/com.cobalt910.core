using ProjectCore.Spline.Scripts.Spline;
using UnityEditor;
using UnityEngine;

namespace ProjectCore.Spline.Editor
{
    [CustomEditor(typeof(SplineAnchor))]
    public class SplineAnchorEditor : UnityEditor.Editor
    {
        void OnSceneGUI ()
        {
            if (Tools.pivotMode == PivotMode.Center)
                Tools.pivotMode = PivotMode.Pivot;
        }

        [DrawGizmo(GizmoType.Selected)]
        static void RenderCustomGizmo(Transform objectTransform, GizmoType gizmoType)
        {
            if (objectTransform.parent != null)
                SplineEditor.RenderCustomGizmo(objectTransform.parent, gizmoType);
        }
    }
}