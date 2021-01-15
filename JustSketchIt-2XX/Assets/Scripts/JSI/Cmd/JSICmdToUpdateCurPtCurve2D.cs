using System.Text;
using X;
using UnityEngine;
using JSI.AppObject;

namespace JSI.Cmd
{
    internal class JSICmdToUpdateCurPtCurve2D : XLoggableCmd {

        // private constructor
        private JSICmdToUpdateCurPtCurve2D(XApp app) : base(app)
        {
            JSIApp jsi = (JSIApp)this.mApp;
        }

        public static bool execute(XApp app)
        {
            JSICmdToUpdateCurPtCurve2D cmd = new JSICmdToUpdateCurPtCurve2D(app);
            return cmd.execute();
        }

        protected override bool defineCmd()
        {
            JSIApp app = (JSIApp)this.mApp;
            JSIAppPolyline2D curPtCurve2D =
                app.getPtCurve2DMgr().getCurPtCurve2D();
            curPtCurve2D.setPts(
                app.getPenMarkMgr().getLastPenMark().getPts());
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