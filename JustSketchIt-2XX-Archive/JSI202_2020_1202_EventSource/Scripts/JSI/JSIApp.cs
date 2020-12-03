using X;
using UnityEngine;

namespace JSI {
    public class JSIApp : XApp {
        //fields
        private JSIGrid mGrid = null;
        
        public override XLogMgr getLogMgr() {
            throw new System.NotImplementedException();
        }

        private JSIPenMarkMgr mPenMarkMgr = null;
        public JSIPenMarkMgr getPenMarkMgr() {
            return this.mPenMarkMgr;
        }

        private XScenarioMgr mScenarioMgr = null;

        public override XScenarioMgr getScenarioMgr() {
            return this.mScenarioMgr;
        }

        private JSIKeyEventSource mKeyEventSource = null;
        private JSIMouseEventSource mMouseEventSource = null;
        private JSIEventListener mEventListener = null;


        //called once
        private void Start() {
            //Debug.Log("Hello, World!");
            this.mGrid = new JSIGrid();
            ////////////
            this.mPenMarkMgr = new JSIPenMarkMgr();
            this.mScenarioMgr = new JSIScenarioMgr(this);

            this.mKeyEventSource = new JSIKeyEventSource();
            this.mMouseEventSource = new JSIMouseEventSource();
            this.mEventListener = new JSIEventListener(this);

            this.mKeyEventSource.setEventListener(this.mEventListener);
            this.mMouseEventSource.setEventListener(this.mEventListener);
        }

        //called every time
        private void Update() {
            this.mKeyEventSource.update();
            this.mMouseEventSource.update();
        }
    }
}
