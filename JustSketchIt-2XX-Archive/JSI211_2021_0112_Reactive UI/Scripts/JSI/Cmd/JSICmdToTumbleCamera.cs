using System.Text;
using X;
using UnityEngine;

namespace JSI.Scenario
{
    internal class JSICmdToTumbleCamera : XLoggableCmd {
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        // private constructor
        private JSICmdToTumbleCamera(XApp app) : base(app)
        {
            JSIApp jsi = (JSIApp)this.mApp;
            JSIPenMark pm = jsi.getPenMarkMgr().getLastPenMark();
            this.mPrevPt = pm.getRecentPt(1);
            this.mCurPt = pm.getRecentPt(0);
        }

        public static bool execute(XApp app)
        {
            JSICmdToTumbleCamera cmd = new JSICmdToTumbleCamera(app);
            return cmd.execute();
        }

        protected override bool defineCmd()
        {
            JSIApp app = (JSIApp) this.mApp;
            JSIPerspCameraPerson cp = app.getPerspCameraPerson();

            float dx = this.mCurPt.x - this.mPrevPt.x;
            float dy = this.mCurPt.y - this.mPrevPt.y;
            float dAzimuth = 180.0f * dx / Screen.width;
            float dZenith = - 180.0f * dy / Screen.height;

            // Quaternion qa = Quaternion.AngleAxis(dAzimuth, Vector3.up);
            Quaternion qa = Quaternion.AngleAxis(dAzimuth, cp.getUp());
            Quaternion qz = Quaternion.AngleAxis(dZenith, cp.getRight());

            Vector3 pivotToEye = cp.getEye() - cp.getPivot();
            Vector3 nextEye = cp.getPivot() + qa * qz * pivotToEye;
            Vector3 nextView = qa * qz * cp.getView();

            cp.setEye(nextEye);
            cp.setView(nextView);

            return true;
        }

        protected override string createLog()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            sb.Append(this.mPrevPt).Append("\t");
            sb.Append(this.mCurPt);
            return sb.ToString();
        }
    }
}