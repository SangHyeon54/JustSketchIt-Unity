using System.Text;
using X;
using UnityEngine;

namespace JSI.Scenario
{
    internal class JSICmdToSelectSmallestStandingCardByScaleHandle : XLoggableCmd {

        // fields
        JSIStandingCard mSelectedStandingCard = null;

        // private constructor
        private JSICmdToSelectSmallestStandingCardByScaleHandle(XApp app) : base(app)
        {
        }

        public static bool execute(XApp app)
        {
            JSICmdToSelectSmallestStandingCardByScaleHandle cmd = 
                new JSICmdToSelectSmallestStandingCardByScaleHandle(app);
            return cmd.execute();
        }

        protected override bool defineCmd()
        {
            JSIApp app = (JSIApp)this.mApp;

            JSIEditStandingCardScenario scenario =
                JSIEditStandingCardScenario.getSingleton();
            
            this.mSelectedStandingCard = scenario.selectStandingCardByScaleHandle();
            scenario.setSelectedStandingCard(this.mSelectedStandingCard);

            return true;
        }

        protected override string createLog()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            sb.Append(this.mSelectedStandingCard);
            return sb.ToString();
        }
    }
}