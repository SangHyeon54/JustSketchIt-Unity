using UnityEngine;
using JSI.AppObject;

namespace JSI {
    public class JSICursor2D : JSIAppCircle2D {
        // contants
        public static readonly float RADIUS = 5.0f;
        public static readonly Color COLOR = Color.red;
        // public static readonly Color COLOR = Color.clear;
        
        // fields
        private JSIApp mApp = null;

        // constructor
        public JSICursor2D(JSIApp app) : base("Cursor2D", JSICursor2D.RADIUS,
            JSICursor2D.COLOR) {
            this.mApp = app;
        }

        // methods 
        public bool hits(JSIAppGeom3D appGeom3D) {
            Vector2 ctr = this.mGameObject.transform.position;
            JSIPerspCameraPerson cp = this.mApp.getPerspCameraPerson();
            Ray ray = cp.getCamera().ScreenPointToRay(ctr);
            RaycastHit hit;
            Collider collider = appGeom3D.getCollider();
            if (collider.Raycast(ray, out hit, Mathf.Infinity)) {
                JSIUtil.createDebugSphere(hit.point);
                return true;
            } else {
                return false;
            }
        }
    }
}