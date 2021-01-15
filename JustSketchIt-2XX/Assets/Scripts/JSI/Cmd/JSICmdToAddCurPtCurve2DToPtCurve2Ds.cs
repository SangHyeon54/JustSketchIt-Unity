using System.Text;
using X;
using UnityEngine;
using JSI.AppObject;
using JSI.Geom;

namespace JSI.Cmd
{
    internal class JSICmdToAddCurPtCurve2DToPtCurve2Ds : XLoggableCmd {

        // private constructor
        private JSICmdToAddCurPtCurve2DToPtCurve2Ds(XApp app) : base(app) {
            JSIApp jsi = (JSIApp)this.mApp;
        }

        public static bool execute(XApp app) {
            JSICmdToAddCurPtCurve2DToPtCurve2Ds cmd = 
                new JSICmdToAddCurPtCurve2DToPtCurve2Ds(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            JSIApp app = (JSIApp)this.mApp;
            JSIAppPolyline2D curPtCurve2D =
                app.getPtCurve2DMgr().getCurPtCurve2D();
            JSIPolyline2D polyline =
                (JSIPolyline2D)curPtCurve2D.getGeom2D();
            if (polyline.getPts().Count > 2) {
                app.getPtCurve2DMgr().getPtCurve2Ds().Add(curPtCurve2D);
                app.getPtCurve2DMgr().setCurPtCurve2D(null);
            } else {
                app.getPtCurve2DMgr().getCurPtCurve2D().destroyGameObject();
                app.getPtCurve2DMgr().setCurPtCurve2D(null);
            }
            
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