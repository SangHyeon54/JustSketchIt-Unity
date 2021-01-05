using X;
using UnityEngine;

namespace JSI.Scenario {
    //for each class, we can create several files: partical class
    public partial class JSIEditStandingCardScenario : XScenario {
        private static JSIEditStandingCardScenario mSingleton = null;
        public static JSIEditStandingCardScenario getSingleton() {
            Debug.Assert(JSIEditStandingCardScenario.mSingleton != null);
            return JSIEditStandingCardScenario.mSingleton;
        }
        
        public static JSIEditStandingCardScenario createSingleton(XApp app) {
            Debug.Assert(JSIEditStandingCardScenario.mSingleton == null);
            JSIEditStandingCardScenario.mSingleton = 
                new JSIEditStandingCardScenario(app);
            return JSIEditStandingCardScenario.mSingleton;
        }
        
        private JSIEditStandingCardScenario (XApp app) : base(app) {
        }
        
        protected override void addScenes() {
            this.addScene(JSIEditStandingCardScenario.ScaleStandingCardScene.
                createSingleton(this));
            this.addScene(JSIEditStandingCardScenario.RotateStandingCardScene.
                createSingleton(this));
            this.addScene(JSIEditStandingCardScenario.MoveStandingCardScene.
                createSingleton(this));
        }
    }
}