using X;
using UnityEngine;

namespace JSI.Scenario {
    public partial class JSINavigateScenario {
        public class PanReadyScene : JSIScene {
            private static PanReadyScene mSingleton = null;
            public static PanReadyScene getSingleton() {
                Debug.Assert(PanReadyScene.mSingleton != null);
                return PanReadyScene.mSingleton;
            }
            
            public static PanReadyScene createSingleton(XScenario scenario) {
                Debug.Assert(PanReadyScene.mSingleton == null);
                PanReadyScene.mSingleton = new PanReadyScene(scenario);
                return PanReadyScene.mSingleton;
            }

            // constructor
            private PanReadyScene(XScenario scenario) : base(scenario) {
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
                            JSINavigateScenario.RotateReadyScene.getSingleton(),
                            this.mReturnScene);
                        break;
                }
            }

            public override void handlePenDown(Vector2 pt) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(app, 
                    JSINavigateScenario.DollyCameraScene.getSingleton(), 
                    this.mReturnScene);

            }

            public override void handlePenDrag(Vector2 pt) {
            }

            public override void handlePenUp(Vector2 pt) {
            }

            public override void getReady() {
            }

            public override void wrapUp() {
            }
        }
    }
    
}