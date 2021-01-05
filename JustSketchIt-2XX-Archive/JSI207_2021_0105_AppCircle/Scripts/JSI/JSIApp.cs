using X;
using UnityEngine;
using JSI.AppObject;

namespace JSI {
    public class JSIApp : XApp {
        //fields
        private JSIPerspCameraPerson mPerspCameraPerson = null;
        public JSIPerspCameraPerson getPerspCameraPerson()
        {
            return this.mPerspCameraPerson;
        }
        private JSIGrid mGrid = null;
        
        public override XLogMgr getLogMgr() {
            return this.mLogMgr;
        }

        private JSIPenMarkMgr mPenMarkMgr = null;
        public JSIPenMarkMgr getPenMarkMgr() {
            return this.mPenMarkMgr;
        }

        private XScenarioMgr mScenarioMgr = null;
        private XLogMgr mLogMgr = null;


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

            new JSIAppRect3D("TestRect3D", 1.0f, 2.0f, 
                new Color(0.5f, 0.0f, 0.0f, 0.5f));
            new JSIAppCircle3D("TestCircle3D", 1.0f, 
                new Color(0.0f, 0.5f, 0.0f, 0.5f));
            
            this.mPerspCameraPerson = new JSIPerspCameraPerson();
            ////////////
            this.mPenMarkMgr = new JSIPenMarkMgr();
            this.mScenarioMgr = new JSIScenarioMgr(this);
            this.mLogMgr = new XLogMgr();
            mLogMgr.setPrintOn(true);

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
