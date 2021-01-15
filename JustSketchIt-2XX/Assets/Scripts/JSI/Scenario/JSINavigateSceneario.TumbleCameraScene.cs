using X;
using UnityEngine;
using JSI.Cmd;

namespace JSI.Scenario {
    public partial class JSINavigateScenario {
        public class TumbleCameraScene : JSIScene {
            private static TumbleCameraScene mSingleton = null;
            public static TumbleCameraScene getSingleton() {
                Debug.Assert(TumbleCameraScene.mSingleton != null);
                return TumbleCameraScene.mSingleton;
            }
            
            public static TumbleCameraScene createSingleton(XScenario scenario) {
                Debug.Assert(TumbleCameraScene.mSingleton == null);
                TumbleCameraScene.mSingleton = new TumbleCameraScene(scenario);
                return TumbleCameraScene.mSingleton;
            }

            // constructor
            private TumbleCameraScene(XScenario scenario) : base(scenario) {
            }


            // methods
            public override void handleKeyDown(KeyCode kc) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftAlt:
                        XCmdToChangeScene.execute(app,
                            JSINavigateScenario.DollyCameraScene.getSingleton(),
                            this.mReturnScene);
                        break;
                }
            }

            public override void handleKeyUp(KeyCode kc) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftControl:
                        XCmdToChangeScene.execute(app,
                            this.mReturnScene, null);
                        break;
                }
            }

            public override void handlePenDown(Vector2 pt) {
            }

            public override void handlePenDrag(Vector2 pt) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                JSICmdToTumbleCamera.execute(app);
            }

            public override void handlePenUp(Vector2 pt) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(app,
                    JSINavigateScenario.RotateReadyScene.getSingleton(),
                    this.mReturnScene);
            }

            public override void getReady() {
                JSIApp app = (JSIApp)this.mScenario.getApp();

                // deactivate stands.
                // deactivate scale handles.
                foreach (JSIStandingCard sc in 
                    app.getStandingCardMgr().getStandingCards()) {
                    sc.getStand().getGameObject().SetActive(false);
                    sc.getScaleHandle().getGameObject().SetActive(false);
                }
            }

            public override void wrapUp() {
            }
        }
    }
    
}