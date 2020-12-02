using X;
using UnityEngine;

namespace JSI {
    public class JSIApp : XApp {
        //fields
        private JSIGrid mGrid = null;
        
        public override XLogMgr getLogMgr() {
            throw new System.NotImplementedException();
        }

        public override XScenarioMgr getScenarioMgr() {
            throw new System.NotImplementedException();
        }

        //called once
        private void Start() {
            //Debug.Log("Hello, World!");
            this.mGrid = new JSIGrid();
        }

        //called every time
        private void Update() {

        }
    }
}
