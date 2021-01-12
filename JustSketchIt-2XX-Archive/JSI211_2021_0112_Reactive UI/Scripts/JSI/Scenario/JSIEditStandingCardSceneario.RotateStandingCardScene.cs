using X;
using UnityEngine;

namespace JSI.Scenario {
    public partial class JSIEditStandingCardScenario {
        public class RotateStandingCardScene : JSIScene {
            private static RotateStandingCardScene mSingleton = null;
            public static RotateStandingCardScene getSingleton() {
                Debug.Assert(RotateStandingCardScene.mSingleton != null);
                return RotateStandingCardScene.mSingleton;
            }
            
            public static RotateStandingCardScene createSingleton(XScenario scenario) {
                Debug.Assert(RotateStandingCardScene.mSingleton == null);
                RotateStandingCardScene.mSingleton = new RotateStandingCardScene(scenario);
                return RotateStandingCardScene.mSingleton;
            }

            // constructor
            private RotateStandingCardScene(XScenario scenario) : base(scenario) {
            }


            // methods
            public override void handleKeyDown(KeyCode kc) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftAlt:
                        XCmdToChangeScene.execute(app, 
                            JSIEditStandingCardScenario.MoveStandingCardScene.
                            getSingleton(), this.mReturnScene);
                    break;
                }
            }

            public override void handleKeyUp(KeyCode kc) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftControl:
                        XCmdToChangeScene.execute(app, this.mReturnScene, null);
                    break;
                }
            }

            public override void handlePenDown(Vector2 pt) {
            }

            public override void handlePenDrag(Vector2 pt) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                JSICmdToRotateStandingCard.execute(app);
            }

            public override void handlePenUp(Vector2 pt) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(app,
                    JSINavigateScenario.RotateReadyScene.getSingleton(),
                    this.mReturnScene);
            }

            public override void getReady() {
                // JSIApp app = (JSIApp) this.mScenario.getApp();
                // JSIEditStandingCardScenario scenario =
                //     (JSIEditStandingCardScenario)this.mScenario;

                // // selecte the card to roate 
                // JSIStandingCard standingCardToRotate =
                //     scenario.selectStandingCardByStand();
                // scenario.setSelectedStandingCard(standingCardToRotate);

                JSIApp app = (JSIApp)this.mScenario.getApp();

                // deactivate stands.
                // deactivate scale handles.
                foreach (JSIStandingCard sc in 
                    app.getStandingCardMgr().getStandingCards()) {
                    sc.getStand().getGameObject().SetActive(false);
                    sc.getScaleHandle().getGameObject().SetActive(false);
                }

                // highlight selected stand.
                JSIEditStandingCardScenario scenario =
                    (JSIEditStandingCardScenario)this.mScenario;
                scenario.getSelectedStandingCard().getStand().getGameObject().
                    SetActive(true);
                scenario.getSelectedStandingCard().highlightStand(true);
            }

            public override void wrapUp() {
            }
        }
    }
    
}