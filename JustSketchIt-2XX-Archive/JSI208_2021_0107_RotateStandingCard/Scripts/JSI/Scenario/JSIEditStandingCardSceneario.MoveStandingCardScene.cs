using X;
using UnityEngine;

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