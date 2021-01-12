using System.Collections.Generic;
using UnityEngine;

public class JSIPenMark {
    // constants
    public static readonly double MIN_DIST_BTWN_PTS = 1.0f;
    
    // fields
    private List<Vector2> mPts = null;
    public List<Vector2> getPts() {
        return this.mPts;
    }
    
    public JSIPenMark(Vector2 pt) {
        this.mPts = new List<Vector2>();
        this.mPts.Add(pt);
    }
    
    public bool addPt(Vector2 pt) {
        //make sure at least one point is created
        Debug.Assert (this.mPts.Count >= 1);
        Vector2 lastPt = this.getLastPt();
        if (Vector2.Distance(lastPt, pt) < JSIPenMark.MIN_DIST_BTWN_PTS) {
            return false;
        } else {
            this.mPts.Add(pt);
            return true;
        }
    }
    
    public Vector2 getFirstPt() {
        return this.mPts[0];
    }
    
    public Vector2 getLastPt() {
        int size = this.mPts.Count;
        Debug.Assert(size > 0);
        return this.mPts[size - 1];
    }

    public Vector2 getRecentPt(int i) {
        int size = this.mPts.Count;
        int index = size - 1 - i;
        Debug.Assert(index >= 0 && index < size);
        return this.mPts[index];
    }
}