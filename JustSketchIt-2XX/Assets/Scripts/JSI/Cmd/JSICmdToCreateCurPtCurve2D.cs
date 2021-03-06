﻿using System.Text;
using X;
using UnityEngine;
using System.Collections.Generic;
using JSI.AppObject;

namespace JSI.Cmd
{
    internal class JSICmdToCreateCurPtCurve2D : XLoggableCmd {

        // fields
        private Vector2 mPt = JSIUtil.VECTOR2_NAN;

        // private constructor
        private JSICmdToCreateCurPtCurve2D(XApp app) : base(app)
        {
            JSIApp jsi = (JSIApp)this.mApp;
            this.mPt = jsi.getPenMarkMgr().getLastPenMark().getLastPt();
        }

        public static bool execute(XApp app)
        {
            JSICmdToCreateCurPtCurve2D cmd = 
                new JSICmdToCreateCurPtCurve2D(app);
            return cmd.execute();
        }

        protected override bool defineCmd()
        {
            JSIApp app = (JSIApp)this.mApp;

            List<Vector2> pts = new List<Vector2>();
            pts.Add(this.mPt);
            JSIAppPolyline2D ptCurve2D = new JSIAppPolyline2D(
                "PtCurve2D", pts, JSIPtCurve2DMgr.PT_CURVE_WIDTH,
                JSIPtCurve2DMgr.PT_CURVE_COLOR);
            app.getPtCurve2DMgr().setCurPtCurve2D(ptCurve2D);
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