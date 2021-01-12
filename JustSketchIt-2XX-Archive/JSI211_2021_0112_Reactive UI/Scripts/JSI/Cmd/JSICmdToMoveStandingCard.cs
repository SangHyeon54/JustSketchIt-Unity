using System.Text;
using X;
using UnityEngine;
using JSI.AppObject;

namespace JSI.Scenario
{
    internal class JSICmdToMoveStandingCard : XLoggableCmd {
        
        // fields
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        private JSICmdToMoveStandingCard(XApp app) : base(app)
        {
            JSIApp jsi = (JSIApp)this.mApp;
            JSIPenMark pm = jsi.getPenMarkMgr().getLastPenMark();
            this.mPrevPt = pm.getRecentPt(1);
            this.mCurPt = pm.getRecentPt(0);
        }

        public static bool execute(XApp app)
        {
            JSICmdToMoveStandingCard cmd =
                new JSICmdToMoveStandingCard(app);
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

            // calculate position difference between the two points.
            Vector3 diff = curPtOnPlane - prevPtOnPlane;

            // update the position of the selected standing card
            JSIEditStandingCardScenario scenario =
                JSIEditStandingCardScenario.getSingleton();
            JSIStandingCard standingCardToMove =
                scenario.getSelectedStandingCard();
            standingCardToMove.getGameObject().transform.position += diff;
                        
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