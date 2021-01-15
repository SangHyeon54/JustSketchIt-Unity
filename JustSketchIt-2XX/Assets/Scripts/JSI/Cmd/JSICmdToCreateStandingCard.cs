using System.Text;
using X;
using UnityEngine;
using System.Collections.Generic;
using JSI.AppObject;
using System;
using JSI.Geom;

namespace JSI.Cmd
{
    internal class JSICmdToCreateStandingCard : XLoggableCmd {
        // private constructor
        private JSICmdToCreateStandingCard(XApp app) : base(app) {
        }

        public static bool execute(XApp app) {
            JSICmdToCreateStandingCard cmd = 
                new JSICmdToCreateStandingCard(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            JSIApp app = (JSIApp) this.mApp;
            JSIPerspCameraPerson cp = app.getPerspCameraPerson();
        
            // check if there exist 2D point curves.
            if (app.getPtCurve2DMgr().getPtCurve2Ds().Count == 0) {
                return false;
            }

            //find the lowest 2D point.
            Vector2 lowestPt2D = this.calcLowestPt2D(
                app.getPtCurve2DMgr().getPtCurve2Ds());

            // calculate the lowest 3D point.
            Ray lowestPtRay = cp.getCamera().ScreenPointToRay(lowestPt2D);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float lowestPtDist = float.NaN;
            groundPlane.Raycast(lowestPtRay, out lowestPtDist);
            Vector3 lowestPt3D = lowestPtRay.GetPoint(lowestPtDist);

            // calculate the noraml vector of the card plane.
            Vector3 normalDir = Vector3.ProjectOnPlane(
                -cp.getView(), Vector3.up).normalized;
            
            // calculate 3D point curves by projecting 2D point curves
            // to the plane.
            Plane cardPlane = new Plane(normalDir, lowestPt3D);
            List<JSIAppPolyline3D> ptCurve3Ds = 
                this.calcProjectedPtCurve3Ds(
                    app.getPtCurve2DMgr().getPtCurve2Ds(), cardPlane,
                    cp.getCamera());

            // clear 2D point curves.
            foreach (JSIAppPolyline2D ptCurve2D in
                app.getPtCurve2DMgr().getPtCurve2Ds()) {
                    ptCurve2D.destroyGameObject();
                }
            app.getPtCurve2DMgr().getPtCurve2Ds().Clear();

            // calculate
            Vector3 cardZDir = -normalDir;
            Vector3 cardYDir = Vector3.up;
            Vector3 cardXDir = Vector3.Cross(cardYDir, cardZDir).normalized;

            Vector3 cardCtr = this.calcCardCtr(ptCurve3Ds, lowestPt3D,
                cardXDir, cardYDir, cardZDir);
            

            // calculate the local 3D 
            List<JSIAppPolyline3D> localPtCurve3Ds =
                this.createLocalPtCurve3Ds(ptCurve3Ds, cardCtr, cardXDir,
                    cardYDir, cardZDir);
            
            // clear 3D point curves.
            foreach (JSIAppPolyline3D ptCurve3D in ptCurve3Ds) {
                ptCurve3D.destroyGameObject();
            }
            ptCurve3Ds.Clear();

            // create a new standing card.
            float cardWidth = this.calcCardWidth(localPtCurve3Ds);
            float cardHeight = this.calcCardHeight(localPtCurve3Ds);
            Quaternion q = Quaternion.LookRotation(cardZDir, cardYDir);
            JSIStandingCard sc = new JSIStandingCard("StandingCard", cardWidth,
                cardHeight, cardCtr, q, localPtCurve3Ds);
            
            // add the standing car to its manager.
            app.getStandingCardMgr().getStandingCards().Add(sc);

            return true;
        }

        protected override string createLog()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            return sb.ToString();
        }

        private Vector2 calcLowestPt2D(List<JSIAppPolyline2D> ptCurve2Ds) {
            Vector2 lowestPt2D = new Vector2(0.0f, Mathf.Infinity);
            foreach (JSIAppPolyline2D ptCurve2D in ptCurve2Ds) {
                JSIPolyline2D polyline =
                    (JSIPolyline2D)ptCurve2D.getGeom2D();
                foreach (Vector2 pt2D in polyline.getPts()) {
                    if (pt2D.y < lowestPt2D.y) {
                        lowestPt2D = pt2D;
                    }
                }
            }
            return lowestPt2D;
        }

        private List<JSIAppPolyline3D> calcProjectedPtCurve3Ds(
            List<JSIAppPolyline2D> ptCurve2Ds, Plane cardPlane, Camera camera) {
            List<JSIAppPolyline3D> ptCurve3Ds =
                new List<JSIAppPolyline3D>();
            
            foreach (JSIAppPolyline2D ptCurve2D in ptCurve2Ds) {
                JSIPolyline2D polyline2D = (JSIPolyline2D)ptCurve2D.getGeom2D();
                List<Vector3> pt3Ds = new List<Vector3>();
                foreach (Vector2 pt2D in polyline2D.getPts()) {
                    Ray ptRay = camera.ScreenPointToRay(pt2D);
                    float ptDist = float.NaN;
                    cardPlane.Raycast(ptRay, out ptDist);
                    Vector3 pt3D = ptRay.GetPoint(ptDist);
                    pt3Ds.Add(pt3D);
                }

                JSIAppPolyline3D ptCurve3D = new JSIAppPolyline3D(
                    "PtCurve3D", pt3Ds, 0.01f, Color.black);
                ptCurve3Ds.Add(ptCurve3D);
            }
            return ptCurve3Ds;
        }

        private Vector3 calcCardCtr(List<JSIAppPolyline3D> ptCurve3Ds, 
            Vector3 lowestPt3D, Vector3 cardXDir, Vector3 cardYDir, 
            Vector3 cardZDir) {
            List<float> xs = new List<float>();
            List<float> ys = new List<float>();
            foreach(JSIAppPolyline3D ptCurve3D in ptCurve3Ds) {
                JSIPolyline3D polyline3D = (JSIPolyline3D)ptCurve3D.getGeom3D();
                foreach (Vector3 pt3D in polyline3D.getPts()) {
                    float x = Vector3.Dot(pt3D - lowestPt3D, cardXDir);
                    float y = Vector3.Dot(pt3D - lowestPt3D, cardYDir);
                    xs.Add(x);
                    ys.Add(y);
                }
            }
            float xMin = Mathf.Min(xs.ToArray());
            float xMax = Mathf.Max(xs.ToArray());
            float yMin = Mathf.Min(ys.ToArray());
            float yMax = Mathf.Max(ys.ToArray());
            float cardWidth = xMax - xMin;
            float cardHeight = yMax - yMin;
            Vector3 cardCtr = lowestPt3D + 
                0.5f * (xMin + xMax) * cardXDir +
                0.5f * (yMin + yMax) * cardYDir;
            
            return cardCtr;
        }

        private List<JSIAppPolyline3D> createLocalPtCurve3Ds(
            List<JSIAppPolyline3D> ptCurve3Ds, Vector3 cardCtr, 
            Vector3 cardXDir, Vector3 cardYDir, Vector3 cardZDir) {
            
            List<JSIAppPolyline3D> localPtCurve3Ds =
                new List<JSIAppPolyline3D>();

            foreach (JSIAppPolyline3D ptCurve3D in ptCurve3Ds) {
                
                JSIPolyline3D polyline3D = (JSIPolyline3D)ptCurve3D.getGeom3D();
                List<Vector3> localPt3Ds = new List<Vector3>();
                foreach (Vector3 pt3D in polyline3D.getPts()) {
                    Vector3 dif = pt3D - cardCtr;
                    float x = Vector3.Dot(dif, cardXDir);
                    float y = Vector3.Dot(dif, cardYDir);
                    float z = Vector3.Dot(dif, cardZDir);
                    Vector3 localPt3D = new Vector3(x, y, z);
                    localPt3Ds.Add(localPt3D);
                }
                JSIAppPolyline3D localPtCurve3D = new JSIAppPolyline3D(
                    "PtCurve3D", localPt3Ds, 0.01f, Color.black);
                localPtCurve3Ds.Add(localPtCurve3D);
            }
            return localPtCurve3Ds;
        }


        private float calcCardWidth(List<JSIAppPolyline3D> ptCurve3Ds)
        {
            float max = float.NegativeInfinity;
            float min = float.PositiveInfinity;

            foreach (JSIAppPolyline3D ptCurve3D in ptCurve3Ds) {
                JSIPolyline3D polyline3D = (JSIPolyline3D)ptCurve3D.getGeom3D();
                foreach (Vector3 pt3D in polyline3D.getPts()) {
                    if (pt3D.x > max)
                        max = pt3D.x;
                    if (pt3D.x < min)
                        min = pt3D.x;
                }

            }
            float width = max - min;
            return width;
        }

        private float calcCardHeight(List<JSIAppPolyline3D> ptCurve3Ds)
        {
            float max = float.NegativeInfinity;
            float min = float.PositiveInfinity;

            foreach (JSIAppPolyline3D ptCurve3D in ptCurve3Ds) {
                JSIPolyline3D polyline3D = (JSIPolyline3D)ptCurve3D.getGeom3D();
                foreach (Vector3 pt3D in polyline3D.getPts()) {
                    if (pt3D.y > max)
                        max = pt3D.y;
                    if (pt3D.y < min)
                        min = pt3D.y;
                }

            }
            float height = max - min;
            return height;
        }
    }
}