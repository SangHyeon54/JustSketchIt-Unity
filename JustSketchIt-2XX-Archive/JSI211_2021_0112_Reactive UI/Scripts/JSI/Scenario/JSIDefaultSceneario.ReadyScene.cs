using X;
using UnityEngine;

namespace JSI.Scenario {
    public partial class JSIDefaultScenario {
        public class ReadyScene : JSIScene {
            private static ReadyScene mSingleton = null;
            public static ReadyScene getSingleton() {
                Debug.Assert(ReadyScene.mSingleton != null);
                return ReadyScene.mSingleton;
            }
            
            public static ReadyScene createSingleton(XScenario scenario) {
                Debug.Assert(ReadyScene.mSingleton == null);
                ReadyScene.mSingleton = new ReadyScene(scenario);
                return ReadyScene.mSingleton;
            }

            // constructor
            private ReadyScene(XScenario scenario) : base(scenario) {
            }


            // methods
            public override void handleKeyDown(KeyCode kc) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftControl:
                        XCmdToChangeScene.execute(app,
                            JSINavigateScenario.RotateReadyScene.getSingleton(),
                            this);
                        break;
                }
            }

            public override void handleKeyUp(KeyCode kc) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.Return:
                        JSICmdToCreateEmptyStandingCard.execute(app);
                        break;
                }
            }

            public override void handlePenDown(Vector2 pt) {
                JSIApp app = (JSIApp)this.mScenario.getApp();

                foreach (JSIStandingCard sc in
                    app.getStandingCardMgr().getStandingCards()) {
                    if (app.getCursor().hits(sc.getScaleHandle())) {
                        JSICmdToSelectSmallestStandingCardByScaleHandle.
                            execute(app);
                        XCmdToChangeScene.execute(app,
                            JSIEditStandingCardScenario.
                            ScaleStandingCardScene.getSingleton(),
                            this);
                        return;
                    }
                }
            }

            public override void handlePenDrag(Vector2 pt) {
            }

            public override void handlePenUp(Vector2 pt) {
            }

            public override void getReady() {
                JSIApp app = (JSIApp)this.mScenario.getApp();

                // deactivate stands.
                // activate scale handles.
                foreach (JSIStandingCard sc in 
                    app.getStandingCardMgr().getStandingCards()) {
                    sc.getStand().getGameObject().SetActive(false);
                    sc.getScaleHandle().getGameObject().SetActive(true);
                    sc.highlightScaleHandle(false);
                }
            }

            public override void wrapUp() {
            }
        }
    }
    
}