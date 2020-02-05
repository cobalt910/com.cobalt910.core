using com.cobalt910.core.Runtime.Core.Spline.Scripts.Spline;
using UnityEditor;
using UnityEngine;

namespace com.cobalt910.core.Runtime.Core.Spline.Editor
{
    [CustomEditor(typeof(SplineTangent))]
    public class SplineTangentEditor : UnityEditor.Editor
    {
        void OnSceneGUI ()
        {
            if (Tools.pivotMode == PivotMode.Center)
            {
                Tools.pivotMode = PivotMode.Pivot;
            }
        }

        [DrawGizmo(GizmoType.Selected)]
        static void RenderCustomGizmo(Transform objectTransform, GizmoType gizmoType)
        {
            if (objectTransform.parent != null && objectTransform.parent.parent != null)
            {
                SplineEditor.RenderCustomGizmo(objectTransform.parent.parent, gizmoType);
            }
        }
    }
}