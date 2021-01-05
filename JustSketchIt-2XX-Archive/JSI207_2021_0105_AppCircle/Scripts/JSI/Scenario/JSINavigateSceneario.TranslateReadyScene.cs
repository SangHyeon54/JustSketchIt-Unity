using X;
using UnityEngine;

namespace JSI.Scenario {
    public partial class JSINavigateScenario {
        public class TranslateReadyScene : JSIScene {
            private static TranslateReadyScene mSingleton = null;
            public static TranslateReadyScene getSingleton() {
                Debug.Assert(TranslateReadyScene.mSingleton != null);
                return TranslateReadyScene.mSingleton;
            }
            
            public static TranslateReadyScene createSingleton(XScenario scenario) {
                Debug.Assert(TranslateReadyScene.mSingleton == null);
                TranslateReadyScene.mSingleton = new TranslateReadyScene(scenario);
                return TranslateReadyScene.mSingleton;
            }

            // constructor
            private TranslateReadyScene(XScenario scenario) : base(scenario) {
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