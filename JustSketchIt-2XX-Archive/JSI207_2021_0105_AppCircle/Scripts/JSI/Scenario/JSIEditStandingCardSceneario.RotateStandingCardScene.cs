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
            }

            public override void handleKeyUp(KeyCode kc) {
            }

            public override void handlePenDown(Vector2 pt) {
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