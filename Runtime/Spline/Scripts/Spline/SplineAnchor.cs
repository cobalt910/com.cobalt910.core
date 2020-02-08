using UnityEngine;

namespace com.cobalt910.core.Runtime.Spline.Scripts.Spline
{
    public enum TangentMode {Mirrored, Aligned, Free}

    [ExecuteInEditMode]
    public class SplineAnchor : MonoBehaviour
    {
        public TangentMode tangentMode;

        public bool RenderingChange
        {
            get; set;
        }

        public bool Changed
        {
            get; set;
        }

        public Transform Anchor
        {
            get
            {
                if (!_initialized) Initialize();
                return _anchor;
            }

            private set
            {
                _anchor = value;
            }
        }

        public Transform InTangent
        {
            get
            {
                if (!_initialized) Initialize();
                return _inTangent;
            }

            private set
            {
                _inTangent = value;
            }
        }

        public Transform OutTangent
        {
            get
            {
                if (!_initialized) Initialize();
                return _outTangent;
            }

            private set
            {
                _outTangent = value;
            }
        }

        bool _initialized;
        [SerializeField][HideInInspector] Transform _masterTangent;
        [SerializeField][HideInInspector] Transform _slaveTangent;
        TangentMode _previousTangentMode;
        Vector3 _previousInPosition;
        Vector3 _previousOutPosition;
        Vector3 _previousAnchorPosition;
        Bounds _skinnedBounds;
        Transform _anchor;
        Transform _inTangent;
        Transform _outTangent;

        void Awake ()
        {
            Initialize ();
        }

        void Update ()
        {
            //don't let an anchor scale:
            transform.localScale = Vector3.one;

            //initialization:
            if (!_initialized)
            {
                Initialize ();
            }

            //override any skinned mesh bounds changes:
            Anchor.localPosition = Vector3.zero;

            //has the anchor moved?
            if (_previousAnchorPosition != transform.position)
            {
                Changed = true;
                RenderingChange = true;
                _previousAnchorPosition = transform.position;
            }

            //run a tangent operation if mode has changed:
            if (_previousTangentMode != tangentMode)
            {
                Changed = true;
                RenderingChange = true;
                TangentChanged ();
                _previousTangentMode = tangentMode;
            }

            //detect tangent movements:
            if (InTangent.localPosition != _previousInPosition)
            {
                Changed = true;
                RenderingChange = true;
                _previousInPosition = InTangent.localPosition;
                _masterTangent = InTangent;
                _slaveTangent = OutTangent;
                TangentChanged ();
                return;
            }

            if (OutTangent.localPosition != _previousOutPosition)
            {
                Changed = true;
                RenderingChange = true;
                _previousOutPosition = OutTangent.localPosition;
                _masterTangent = OutTangent;
                _slaveTangent = InTangent;
                TangentChanged ();
                return;
            }
        }

        void TangentChanged ()
        {
            switch (tangentMode)
            {
            case TangentMode.Free:
                break;

            case TangentMode.Mirrored:
                Vector3 mirroredOffset = _masterTangent.position - transform.position;
                _slaveTangent.position = transform.position - mirroredOffset;
                break;

            case TangentMode.Aligned:
                float distance = Vector3.Distance (_slaveTangent.position, transform.position);
                Vector3 alignedOffset = (_masterTangent.position - transform.position).normalized;
                _slaveTangent.position = transform.position - (alignedOffset * distance);
                break;
            }

            _previousInPosition = InTangent.localPosition;
            _previousOutPosition = OutTangent.localPosition;
        }

        void Initialize ()
        {
            _initialized = true;

            //grabs references:
            InTangent = transform.GetChild (0);
            OutTangent = transform.GetChild (1);
            Anchor = transform.GetChild (2); 

            //prepopulate master and slave tangents:
            _masterTangent = InTangent;
            _slaveTangent = OutTangent;

            //hide some things to reduce clutter:
            Anchor.hideFlags = HideFlags.HideInHierarchy;

            foreach (var item in GetComponentsInChildren<Renderer> ())
            {
                item.sharedMaterial.hideFlags = HideFlags.HideInInspector;
            }

            foreach (var item in GetComponentsInChildren<MeshFilter> ())
            {
                item.hideFlags = HideFlags.HideInInspector;
            }

            foreach (var item in GetComponentsInChildren<MeshRenderer> ())
            {
                item.hideFlags = HideFlags.HideInInspector;
            }

            foreach (var item in GetComponentsInChildren<SkinnedMeshRenderer> ())
            {
                item.hideFlags = HideFlags.HideInInspector;
            }

            //synchronize status variables:
            _previousTangentMode = tangentMode;
            _previousInPosition = InTangent.localPosition;
            _previousOutPosition = OutTangent.localPosition;
            _previousAnchorPosition = transform.position;
        }

        //Public Methods:
        public void SetTangentStatus (bool inStatus, bool outStatus)
        {
            InTangent.gameObject.SetActive (inStatus);
            OutTangent.gameObject.SetActive (outStatus);
        }

        public void Tilt (Vector3 angles)
        {
            //save current rotation and rotate as requested:
            Quaternion rotation = transform.localRotation;
            transform.Rotate (angles);

            Vector3 inPosition = InTangent.position;
            Vector3 outPosition = OutTangent.position;

            InTangent.position = inPosition;
            OutTangent.position = outPosition;
        }
    }
}