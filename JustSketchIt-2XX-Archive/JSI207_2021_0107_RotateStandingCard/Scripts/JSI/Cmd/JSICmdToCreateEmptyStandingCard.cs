using System.Text;
using X;
using UnityEngine;

namespace JSI.Scenario
{
    internal class JSICmdToCreateEmptyStandingCard : XLoggableCmd {
        // private constructor
        private JSICmdToCreateEmptyStandingCard(XApp app) : base(app) {
        }

        public static bool execute(XApp app) {
            JSICmdToCreateEmptyStandingCard cmd = 
                new JSICmdToCreateEmptyStandingCard(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            JSIApp app = (JSIApp) this.mApp;
            JSIPerspCameraPerson cp = app.getPerspCameraPerson();
        
            // calculate the normal vector of the card plane.
            Vector3 normalDir = 
                Vector3.ProjectOnPlane(-cp.getView(), Vector3.up).normalized;
            
            // define card dimensions.
            float cardWidth = 1.0f;
            float cardHeight = 2.0f;
            Vector3 cardCenter = new Vector3(0.0f, cardHeight / 2.0f, 0.0f);
            Vector3 cardZDir = - normalDir;
            Vector3 cardYDir = Vector3.up;
            Vector3 cardXDir = Vector3.Cross(cardYDir, cardZDir);
            Quaternion rot = Quaternion.LookRotation(cardZDir, cardYDir);
            
            // create a new stadning card
            JSIStandingCard sc = new JSIStandingCard("EmptyStandingCard",
                cardWidth, cardHeight, cardCenter, rot, null);

            // add the standing card to its mgr
            app.getStandingCardMgr().getStandingCards().Add(sc);

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