using HoloToolkit.Unity.InputModule;
using System;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

namespace HoloToolkit.Unity
{
    /// <summary>  
    /// GestureManager creates a gesture recognizer and signs up for a tap gesture.  
    /// When a tap gesture is detected, GestureManager uses GazeManager to find the game object.  
    /// GestureManager then sends a message to that game object.  
    /// </summary>  
    [RequireComponent(typeof(GazeManager))]
    public partial class GestureManager : Singleton<GestureManager>
    {
        /// <summary>  
        /// Key to press in the editor to select the currently gazed hologram  
        /// </summary>  
        public KeyCode EditorSelectKey = KeyCode.Space;

        /// <summary>  
        /// To select even when a hologram is not being gazed at,  
        /// set the override focused object.  
        /// If its null, then the gazed at object will be selected.  
        /// </summary>  
        public GameObject OverrideFocusedObject
        {
            get; set;
        }

        /// <summary>  
        /// Gets the currently focused object, or null if none.  
        /// </summary>  
        public GameObject FocusedObject
        {
            get { return focusedObject; }
        }

        private GestureRecognizer gestureRecognizer;
        private GameObject focusedObject;

        public bool IsNavigating { get; private set; }
        public Vector3 NavigationPosition { get; private set; }

        void Start()
        {
            //  创建GestureRecognizer实例  
            gestureRecognizer = new GestureRecognizer();
            //  注册指定的手势类型,本例指定单击及双击手势类型  
            gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap
                | GestureSettings.DoubleTap);
            //  订阅手势事件  
            gestureRecognizer.TappedEvent += GestureRecognizer_TappedEvent;

            //  开始手势识别  
            gestureRecognizer.StartCapturingGestures();
        }

        private void OnTap()
        {
            if (focusedObject != null)
            {
                focusedObject.SendMessage("OnTap");
            }
        }

        private void OnDoubleTap()
        {
            if (focusedObject != null)
            {
                focusedObject.SendMessage("OnDoubleTap");
            }
        }

        private void GestureRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
        {
            if (tapCount == 1)
            {
                OnTap();
            }
            else
            {
                OnDoubleTap();
            }
        }

        void LateUpdate()
        {
            GameObject oldFocusedObject = focusedObject;

            if (GazeManager.Instance.HitObject &&
                OverrideFocusedObject == null &&
                GazeManager.Instance.HitInfo.collider != null)
            {
                // If gaze hits a hologram, set the focused object to that game object.  
                // Also if the caller has not decided to override the focused object.  
                focusedObject = GazeManager.Instance.HitInfo.collider.gameObject;
            }
            else
            {
                // If our gaze doesn't hit a hologram, set the focused object to null or override focused object.  
                focusedObject = OverrideFocusedObject;
            }

            if (focusedObject != oldFocusedObject)
            {
                // If the currently focused object doesn't match the old focused object, cancel the current gesture.  
                // Start looking for new gestures.  This is to prevent applying gestures from one hologram to another.  
                gestureRecognizer.CancelGestures();
                gestureRecognizer.StartCapturingGestures();
            }

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(EditorSelectKey))
            {
                OnTap();
            }
#endif
        }

        protected override void OnDestroy()
        {
            gestureRecognizer.StopCapturingGestures();
            gestureRecognizer.TappedEvent -= GestureRecognizer_TappedEvent;
        }
    }
}
