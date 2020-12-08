using X;
using UnityEngine;

namespace JSI.Scenario {
    public partial class JSINavigateScenario {
        public class DollyCameraScene : JSIScene {
            private static DollyCameraScene mSingleton = null;
            public static DollyCameraScene getSingleton() {
                Debug.Assert(DollyCameraScene.mSingleton != null);
                return DollyCameraScene.mSingleton;
            }
            
            public static DollyCameraScene createSingleton(XScenario scenario) {
                Debug.Assert(DollyCameraScene.mSingleton == null);
                DollyCameraScene.mSingleton = new DollyCameraScene(scenario);
                return DollyCameraScene.mSingleton;
            }

            // constructor
            private DollyCameraScene(XScenario scenario) : base(scenario) {
            }


            // methods
            public override void handleKeyDown(KeyCode kc) {
            }

            public override void handleKeyUp(KeyCode kc) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftControl:
                        XCmdToChangeScene.execute(app,
                            this.mReturnScene, null);
                        break;
                    case KeyCode.LeftAlt:
                        XCmdToChangeScene.execute(app,
                            JSINavigateScenario.TumbleCameraScene.getSingleton(),
                            this.mReturnScene);
                        break;
                }
            }

            public override void handlePenDown(Vector2 pt) {
            }

            public override void handlePenDrag(Vector2 pt) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                JSICmdToDollyCamera.execute(app);
            }

            public override void handlePenUp(Vector2 pt) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(app,
                    JSINavigateScenario.PanReadyScene.getSingleton(),
                    this.mReturnScene);
            }

            public override void getReady() {
            }

            public override void wrapUp() {
            }
        }
    }
    
}