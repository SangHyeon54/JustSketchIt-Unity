using X;
using UnityEngine;

namespace JSI {
    public class JSIApp : XApp {
        public override XLogMgr getLogMgr() {
            throw new System.NotImplementedException();
        }

        public override XScenarioMgr getScenarioMgr() {
            throw new System.NotImplementedException();
        }

        //called once
        private void Start() {
            Debug.Log("Hello, World!");
        }

        //called every time
        private void Update() {

        }
    }
}
