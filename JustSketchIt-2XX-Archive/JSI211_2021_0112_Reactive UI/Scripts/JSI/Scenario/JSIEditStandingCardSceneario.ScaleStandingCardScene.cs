using X;
using UnityEngine;

namespace JSI.Scenario {
    public partial class JSIEditStandingCardScenario {
        public class ScaleStandingCardScene : JSIScene {
            private static ScaleStandingCardScene mSingleton = null;
            public static ScaleStandingCardScene getSingleton() {
                Debug.Assert(ScaleStandingCardScene.mSingleton != null);
                return ScaleStandingCardScene.mSingleton;
            }
            
            public static ScaleStandingCardScene createSingleton(XScenario scenario) {
                Debug.Assert(ScaleStandingCardScene.mSingleton == null);
                ScaleStandingCardScene.mSingleton = new ScaleStandingCardScene(scenario);
                return ScaleStandingCardScene.mSingleton;
            }

            // constructor
            private ScaleStandingCardScene(XScenario scenario) : base(scenario) {
            }


            // methods
            public override void handleKeyDown(KeyCode kc) {
            }

            public override void handleKeyUp(KeyCode kc) {
            }

            public override void handlePenDown(Vector2 pt) {
            }

            public override void handlePenDrag(Vector2 pt) {
                JSIApp app = (JSIApp) this.mScenario.getApp();
                JSICmdToScaleStandingCard.execute(app);
            }

            public override void handlePenUp(Vector2 pt) {
                JSIApp app = (JSIApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(app, this.mReturnScene, null);
                
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

                // highlight selected stand.
                JSIEditStandingCardScenario scenario =
                    (JSIEditStandingCardScenario)this.mScenario;
                scenario.getSelectedStandingCard().getScaleHandle().
                    getGameObject().SetActive(true);
                scenario.getSelectedStandingCard().highlightScaleHandle(true);
            }

            public override void wrapUp() {
            }
        }
    }
    
}