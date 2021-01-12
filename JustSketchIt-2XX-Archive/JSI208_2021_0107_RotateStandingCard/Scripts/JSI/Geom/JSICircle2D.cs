using System.Collections.Generic;
using UnityEngine;
namespace JSI.Geom {
    public class JSICircle2D : JSIGeom2D {
        // fields
        private readonly float mRadius = float.NaN;
        public float getRadius() {
            return this.mRadius;
        }
        private readonly Vector2 mPos = Vector2.zero;
        public Vector2 getPos() {
            return this.mPos;
        }
        private readonly Quaternion mRot = Quaternion.identity;
        public Quaternion getRot() {
            return this.mRot;
        }

        // constructor
        public JSICircle2D(float radius, Vector2 pos, Quaternion rot) {
            this.mRadius = radius;
            this.mPos = pos;
            this.mRot = rot;
        }
        // methods
        public List<Vector2> calcPts(int sideNum) {
            float dtheta = 2.0f * Mathf.PI / (float)sideNum;
            List<Vector2> pts = new List<Vector2>();
            for (int i = 0; i < sideNum + 1; i++) {
                Vector2 pt = this.mPos + new Vector2(
                    this.mRadius * Mathf.Cos((float)i * dtheta),
                    this.mRadius * Mathf.Sin((float)i * dtheta));
                pts.Add(pt);
            }
            return pts; 
        }
        public Mesh calcMesh(int sideNum) {
            List<Vector3> vertices = new List<Vector3>();
            foreach (Vector2 pt in this.calcPts(sideNum)) {
                vertices.Add((Vector3)pt);
            }
            vertices.Add((Vector3)this.mPos);

            int[] triangles = new int[3 * sideNum];
            for (int i = 0; i < sideNum; i++) {
                int j = 3 * i;
                triangles[j] = i;
                triangles[j + 1] = i + 1;
                triangles[j + 2] = sideNum + 1;
            }

            Mesh mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles;
            return mesh;
        }
    }
}