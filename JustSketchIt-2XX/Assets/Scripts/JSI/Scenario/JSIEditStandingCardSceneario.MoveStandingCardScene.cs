using X;
using UnityEngine;
using JSI.Cmd;

namespace JSI.Scenario {
    public partial class JSIEditStandingCardScenario {
        public class MoveStandingCardScene : JSIScene {
            private static MoveStandingCardScene mSingleton = null;
            public static MoveStandingCardScene getSingleton() {
                Debug.Assert(MoveStandingCardScene.mSingleton != null);
                return MoveStandingCardScene.mSingleton;
            }
            
            public static MoveStandingCardScene createSingleton(XScenario scenario) {
                Debug.Assert(MoveStandingCardScene.mSingleton == null);
                MoveStandingCardScene.mSingleton = new MoveStandingCardScene(scenario);
                return MoveStandingCardScene.mSingleton;
            }

            // constructor
            private MoveStandingCardScene(XScenario scenario) : base(scenario) {
            }


            // methods
            public override void handleKeyDown(KeyCode kc) {
            }

            public override void handleKeyUp(KeyCode kc) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftControl:
                        XCmdToChangeScene.execute(app, this.mReturnScene, null);
                        break;
                    case KeyCode.LeftAlt:
                        XCmdToChangeScene.execute(app, 
                            JSIEditStandingCardScenario.RotateStandingCardScene.
                            getSingleton(), this.mReturnScene);
                        break;

                }
            }

            public override void handlePenDown(Vector2 pt) {
            }

            public override void handlePenDrag(Vector2 pt) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                JSICmdToMoveStandingCard.execute(app);
            }

            public override void handlePenUp(Vector2 pt) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(app,
                    JSINavigateScenario.TranslateReadyScene.getSingleton(),
                    this.mReturnScene);
            }

            public override void getReady() {
                // JSIApp app = (JSIApp) this.mScenario.getApp();
                // JSIEditStandingCardScenario scenario =
                //     (JSIEditStandingCardScenario)this.mScenario;

                // // selecte the card to roate 
                // JSIStandingCard standingCardToMove =
                //     scenario.selectStandingCardByStand();
                // scenario.setSelectedStandingCard(standingCardToMove);

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