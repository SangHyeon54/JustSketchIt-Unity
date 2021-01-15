using X;
using UnityEngine;
using JSI.Cmd;

namespace JSI.Scenario {
    public partial class JSINavigateScenario {
        public class RotateReadyScene : JSIScene {
            private static RotateReadyScene mSingleton = null;
            public static RotateReadyScene getSingleton() {
                Debug.Assert(RotateReadyScene.mSingleton != null);
                return RotateReadyScene.mSingleton;
            }
            
            public static RotateReadyScene createSingleton(XScenario scenario) {
                Debug.Assert(RotateReadyScene.mSingleton == null);
                RotateReadyScene.mSingleton = new RotateReadyScene(scenario);
                return RotateReadyScene.mSingleton;
            }

            // constructor
            private RotateReadyScene(XScenario scenario) : base(scenario) {
            }


            // methods
            public override void handleKeyDown(KeyCode kc) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftAlt:
                        XCmdToChangeScene.execute(app,
                            JSINavigateScenario.TranslateReadyScene.getSingleton(),
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
                JSIApp app = (JSIApp)this.mScenario.getApp();

                foreach (JSIStandingCard sc in
                    app.getStandingCardMgr().getStandingCards()) {
                    if (app.getCursor().hits(sc.getStand())) {
                        JSICmdToSelectSmallestStandingCardByStand.execute(app);
                        XCmdToChangeScene.execute(app,
                            JSIEditStandingCardScenario.
                            RotateStandingCardScene.getSingleton(),
                            this.mReturnScene);
                        return;
                    }
                }
                XCmdToChangeScene.execute(app, 
                    JSINavigateScenario.TumbleCameraScene.getSingleton(), 
                    this.mReturnScene);

            }

            public override void handlePenDrag(Vector2 pt) {
            }

            public override void handlePenUp(Vector2 pt) {
            }

            public override void getReady() {
                JSIApp app = (JSIApp)this.mScenario.getApp();

                // activate stands.
                // deactivate scale handles.
                foreach (JSIStandingCard sc in 
                    app.getStandingCardMgr().getStandingCards()) {
                    sc.getStand().getGameObject().SetActive(true);
                    sc.getScaleHandle().getGameObject().SetActive(false);
                    sc.highlightStand(false);
                }
            }

            public override void wrapUp() {
            }
        }
    }
    
}