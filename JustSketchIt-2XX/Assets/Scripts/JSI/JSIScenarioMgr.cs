using X;
using JSI.Scenario;

namespace JSI {
    public class JSIScenarioMgr : XScenarioMgr {
        // constructor
        public JSIScenarioMgr(XApp app) : base(app) {
        }

        // methods
        protected override void addScenarios() {
            JSIApp app = (JSIApp) this.mApp;
            this.addScenario(JSIDefaultScenario.createSingleton(app));
        }

        protected override void setInitCurScene() {
            this.setCurScene(JSIDefaultScenario.ReadyScene.getSingleton());
        }
    }
}