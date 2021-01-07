using X;
using UnityEngine;

namespace JSI.Scenario {
    //for each class, we can create several files: partical class
    public partial class JSIEmptyScenario : XScenario {
        private static JSIEmptyScenario mSingleton = null;
        public static JSIEmptyScenario getSingleton() {
            Debug.Assert(JSIEmptyScenario.mSingleton != null);
            return JSIEmptyScenario.mSingleton;
        }
        
        public static JSIEmptyScenario createSingleton(XApp app) {
            Debug.Assert(JSIEmptyScenario.mSingleton == null);
            JSIEmptyScenario.mSingleton = new JSIEmptyScenario(app);
            return JSIEmptyScenario.mSingleton;
        }
        
        private JSIEmptyScenario (XApp app) : base(app) {
        }
        
        protected override void addScenes() {
            this.addScene(JSIEmptyScenario.EmptyScene.createSingleton(this));
        }
    }
}