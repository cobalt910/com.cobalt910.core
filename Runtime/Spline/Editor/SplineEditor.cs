#if UNITY_EDITOR
using com.cobalt910.core.Runtime.Spline.Scripts.Spline;
using UnityEditor;
using UnityEngine;

namespace com.cobalt910.core.Runtime.Spline.Editor
{
    [CustomEditor(typeof(Scripts.Spline.Spline))]
    public class SplineEditor : UnityEditor.Editor
    {
        Scripts.Spline.Spline _target;

        void OnEnable ()
        {
            _target = target as Scripts.Spline.Spline;
        }

        public override void OnInspectorGUI ()
        {
            DrawDefaultInspector ();
            DrawAddButton ();
        }

        [DrawGizmo(GizmoType.Selected)]
        public static void RenderCustomGizmo (Transform objectTransform, GizmoType gizmoType)
        {
            DrawTools (objectTransform);
        }

        void OnSceneGUI ()
        {
            DrawTools ((target as Scripts.Spline.Spline).transform);	
        }

        void DrawAddButton ()
        {
            if (GUILayout.Button ("Extend"))
            {
                Undo.RegisterCreatedObjectUndo (_target.AddAnchors (1) [0], "Extend Spline");
            }
        }

        static void DrawTools (Transform target)
        {
            Scripts.Spline.Spline spline = target.GetComponent<Scripts.Spline.Spline> ();

            if (spline == null) return;
            if (target.transform.childCount == 0) return;

            Handles.color = spline.SecondaryColor;

            for (int i = 0; i < spline.Anchors.Length; i++)
            {
                Quaternion lookRotation = Quaternion.identity;
                SplineAnchor currentAnchor = spline.Anchors[i];

                currentAnchor.InTangent.localScale = Vector3.one * (spline.toolScale * 1.3f);
                currentAnchor.OutTangent.localScale = Vector3.one * (spline.toolScale * 1.3f);
                currentAnchor.Anchor.localScale = Vector3.one * (spline.toolScale * 2.1f);

                if (spline.toolScale > 0)
                {
                    if (currentAnchor.OutTangent.gameObject.activeSelf)
                    {
                        Handles.DrawDottedLine (currentAnchor.Anchor.position, currentAnchor.OutTangent.position, 3);
                        
                        if (SceneView.currentDrawingSceneView != null)
                        {
                            lookRotation = Quaternion.LookRotation ((SceneView.currentDrawingSceneView.camera.transform.position - currentAnchor.OutTangent.position).normalized);
                            Handles.CircleHandleCap (0, currentAnchor.OutTangent.position, lookRotation, spline.toolScale * .65f, EventType.Repaint);
                            Handles.CircleHandleCap (0, currentAnchor.OutTangent.position, lookRotation, spline.toolScale * .25f, EventType.Repaint);
                        }
                    }

                    if (currentAnchor.InTangent.gameObject.activeSelf)
                    {
                        Handles.DrawDottedLine (currentAnchor.Anchor.position, currentAnchor.InTangent.position, 3);

                        if (SceneView.currentDrawingSceneView != null)
                        {
                            lookRotation = Quaternion.LookRotation ((SceneView.currentDrawingSceneView.camera.transform.position - currentAnchor.InTangent.position).normalized);
                            Handles.CircleHandleCap (0, currentAnchor.InTangent.position, lookRotation, spline.toolScale * .65f, EventType.Repaint);
                            Handles.CircleHandleCap (0, currentAnchor.InTangent.position, lookRotation, spline.toolScale * .25f, EventType.Repaint);
                        }
                    }
                    
                    if (SceneView.currentDrawingSceneView != null)
                    {
                        lookRotation = Quaternion.LookRotation ((SceneView.currentDrawingSceneView.camera.transform.position - currentAnchor.Anchor.position).normalized);
                        Handles.CircleHandleCap (0, currentAnchor.Anchor.position, lookRotation, spline.toolScale, EventType.Repaint);
                    }

                    if (spline.direction == SplineDirection.Forward && i == 0 || spline.direction == SplineDirection.Backwards && i == spline.Anchors.Length - 1) 
                    {
                        Handles.CircleHandleCap (0, currentAnchor.Anchor.position, lookRotation, spline.toolScale * .8f, EventType.Repaint);
                        Handles.CircleHandleCap (0, currentAnchor.Anchor.position, lookRotation, spline.toolScale * .75f, EventType.Repaint);
                        Handles.CircleHandleCap (0, currentAnchor.Anchor.position, lookRotation, spline.toolScale * .5f, EventType.Repaint);
                        Handles.CircleHandleCap (0, currentAnchor.Anchor.position, lookRotation, spline.toolScale * .45f, EventType.Repaint);
                        Handles.CircleHandleCap (0, currentAnchor.Anchor.position, lookRotation, spline.toolScale * .25f, EventType.Repaint);
                    }
                }
            }

            for (int i = 0; i < spline.Anchors.Length - 1; i++)
            {
                SplineAnchor startAnchor = spline.Anchors[i];
                SplineAnchor endAnchor = spline.Anchors[i+1];
                Handles.DrawBezier (startAnchor.Anchor.position, endAnchor.Anchor.position, startAnchor.OutTangent.position, endAnchor.InTangent.position, spline.color, null, 2);
            }
        }
    }
}
#endif