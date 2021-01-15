using System.Collections.Generic;
using UnityEngine;

namespace JSI.Geom {
    public class JSIPolyline2D : JSIGeom2D {
        //fiels
        //readonly: once declared, can't no longer be modified
        private readonly List<Vector2> mPts = null;
        public List<Vector2> getPts() {
            return this.mPts;

        }
        
        //constructor
        public JSIPolyline2D(List<Vector2> pts) {
            this.mPts = pts;
        }

        //utility methods
        public Vector2 calcCentroid() {
            Vector2 centroid = Vector2.zero;
            int num = this.mPts.Count;
            foreach (Vector2 pt in this.mPts) {
                centroid += pt;
            }
            centroid /= (float) num;
            return centroid;
        }
        public float calcMaxDevFrom(Vector2 centroid) {
            float maxDev = float.NegativeInfinity;
            foreach (Vector2 pt in this.mPts) {
                float dev = Vector2.Distance(pt, centroid);
                if (dev > maxDev) {
                    maxDev = dev;
                }
            }
            return maxDev;
        }
    }
}