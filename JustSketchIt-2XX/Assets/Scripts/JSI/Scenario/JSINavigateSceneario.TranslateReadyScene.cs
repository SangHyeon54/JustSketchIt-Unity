using X;
using UnityEngine;
using JSI.Cmd;

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

                foreach (JSIStandingCard sc in
                    app.getStandingCardMgr().getStandingCards()) {
                    if (app.getCursor().hits(sc.getStand())) {
                        JSICmdToSelectSmallestStandingCardByStand.execute(app);
                        XCmdToChangeScene.execute(app,
                            JSIEditStandingCardScenario.
                            MoveStandingCardScene.getSingleton(),
                            this.mReturnScene);
                        return;
                    }
                }

                XCmdToChangeScene.execute(app, 
                    JSINavigateScenario.DollyCameraScene.getSingleton(), 
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