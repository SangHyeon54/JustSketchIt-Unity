using System.Text;
using X;
using UnityEngine;
using JSI.AppObject;

namespace JSI.Scenario
{
    internal class JSICmdToRotateStandingCard : XLoggableCmd {
        
        // fields
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        private JSICmdToRotateStandingCard(XApp app) : base(app)
        {
            JSIApp jsi = (JSIApp)this.mApp;
            JSIPenMark pm = jsi.getPenMarkMgr().getLastPenMark();
            this.mPrevPt = pm.getRecentPt(1);
            this.mCurPt = pm.getRecentPt(0);
        }

        public static bool execute(XApp app)
        {
            JSICmdToRotateStandingCard cmd =
                new JSICmdToRotateStandingCard(app);
            return cmd.execute();
        }

        protected override bool defineCmd()
        {
            JSIApp app = (JSIApp)this.mApp;
            JSIPerspCameraPerson cp = app.getPerspCameraPerson();

            // create the ground plane.
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            // project the previous screen point to the plane.
            Ray prevPtRay = cp.getCamera().ScreenPointToRay(this.mPrevPt);
            float prevPtDist = float.NaN;
            groundPlane.Raycast(prevPtRay, out prevPtDist);
            Vector3 prevPtOnPlane = prevPtRay.GetPoint(prevPtDist);

            // project the previous screen point to the plane.
            Ray curPtRay = cp.getCamera().ScreenPointToRay(this.mCurPt);
            float curPtDist = float.NaN;
            groundPlane.Raycast(curPtRay, out curPtDist);
            Vector3 curPtOnPlane = curPtRay.GetPoint(curPtDist);

            // calculate rotation
            JSIEditStandingCardScenario scenario =
                JSIEditStandingCardScenario.getSingleton();
            JSIStandingCard  standingCardToRotate =
                scenario.getSelectedStandingCard();
            
            JSIAppCircle3D stand = standingCardToRotate.getStand();
            Vector3 standCtr = stand.getGameObject().transform.position;

            Quaternion prevRot = Quaternion.LookRotation(
                Vector3.up, prevPtOnPlane - standCtr);
            Quaternion curRot = Quaternion.LookRotation(
                Vector3.up, curPtOnPlane - standCtr);
            Quaternion delRot = curRot * Quaternion.Inverse(prevRot);

            standingCardToRotate.getGameObject().transform.rotation =
                delRot * standingCardToRotate.getGameObject().transform.rotation;

            return true;
        }

        protected override string createLog()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            return sb.ToString();
        }
    }
}