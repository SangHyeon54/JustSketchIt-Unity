using X;
using UnityEngine;
using JSI.Cmd;

namespace JSI.Scenario {
    public partial class JSIDrawScenario {
        public class DrawScene : JSIScene {
            private static DrawScene mSingleton = null;
            public static DrawScene getSingleton() {
                Debug.Assert(DrawScene.mSingleton != null);
                return DrawScene.mSingleton;
            }
            
            public static DrawScene createSingleton(XScenario scenario) {
                Debug.Assert(DrawScene.mSingleton == null);
                DrawScene.mSingleton = new DrawScene(scenario);
                return DrawScene.mSingleton;
            }

            // constructor
            private DrawScene(XScenario scenario) : base(scenario) {
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
                JSICmdToUpdateCurPtCurve2D.execute(app);
            }

            public override void handlePenUp(Vector2 pt) {
                JSIApp app = (JSIApp) this.mScenario.getApp();
                JSICmdToAddCurPtCurve2DToPtCurve2Ds.execute(app);
                XCmdToChangeScene.execute(app, this.mReturnScene, null);
            }

            public override void getReady() {
            }

            public override void wrapUp() {
            }
        }
    }
    
}