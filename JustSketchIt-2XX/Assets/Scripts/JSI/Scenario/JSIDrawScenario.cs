using X;
using UnityEngine;

namespace JSI.Scenario {
    //for each class, we can create several files: partical class
    public partial class JSIDrawScenario : XScenario {
        private static JSIDrawScenario mSingleton = null;
        public static JSIDrawScenario getSingleton() {
            Debug.Assert(JSIDrawScenario.mSingleton != null);
            return JSIDrawScenario.mSingleton;
        }
        
        public static JSIDrawScenario createSingleton(XApp app) {
            Debug.Assert(JSIDrawScenario.mSingleton == null);
            JSIDrawScenario.mSingleton = new JSIDrawScenario(app);
            return JSIDrawScenario.mSingleton;
        }
        
        private JSIDrawScenario (XApp app) : base(app) {
        }
        
        protected override void addScenes() {
            this.addScene(JSIDrawScenario.DrawScene.createSingleton(this));
        }
    }
}