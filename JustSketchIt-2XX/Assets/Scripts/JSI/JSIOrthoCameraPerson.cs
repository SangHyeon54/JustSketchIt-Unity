using UnityEngine;

namespace JSI
{
    public class JSIOrthoCameraPerson : JSICameraPerson
    {
        // constants
        public static readonly float NEAR = 0.1f; // in meter (10cm)
        public static readonly float FAR = 2.0f; // in meter (2m)
        public static readonly float SCREEN_CAMERA_DIST = 1.0f;
        // fields
        private float mScreenWidth = float.NaN;
        private float mScreenHeight = float.NaN;

        // constructor
        public JSIOrthoCameraPerson() : base("OrthoCameraPerson")
        {

        }


        protected override void defineExternalCameraParameters()
        {
            this.update();
        }

        protected override void defineInternalCameraParameters()
        {
            this.mCamera.orthographic = true;
            this.mCamera.depth = 1.0f;

            this.mCamera.clearFlags = CameraClearFlags.Depth; // depth buffer;
            this.mCamera.cullingMask = 32; // 100000, UI layer only

            this.mCamera.nearClipPlane = JSIOrthoCameraPerson.NEAR;
            this.mCamera.farClipPlane = JSIOrthoCameraPerson.FAR;
        }

        public void update() {
            if (Screen.width != this.mScreenWidth ||
                Screen.height != this.mScreenHeight) {
                
                // udpate the screen size.
                this.mScreenWidth = Screen.width;
                this.mScreenHeight = Screen.height;

                //update the screen camera.
                this.mCameraRig.getGameObject().transform.position =
                    new Vector3(this.mScreenWidth / 2.0f,
                        this.mScreenHeight / 2.0f, 
                        -JSIOrthoCameraPerson.SCREEN_CAMERA_DIST);
                this.mCamera.orthographicSize = this.mScreenHeight / 2.0f;
            }
        }
    }
}