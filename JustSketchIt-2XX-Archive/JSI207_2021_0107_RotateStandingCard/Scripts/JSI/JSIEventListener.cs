using UnityEngine;

namespace JSI {
    public class JSIEventListener {
        //fields
        private JSIApp mApp = null;

        //constructor
        public JSIEventListener(JSIApp app) {
            this.mApp = app;
        }

        //methods
        public void keyPressed(KeyCode kc) {
            // Debug.Log("keyPressed: " + kc);
            JSIScene curScene = 
                (JSIScene) this.mApp.getScenarioMgr().getCurScene();
            curScene.handleKeyDown(kc);
        }
        public void keyReleased(KeyCode kc) {
            // Debug.Log("keyReleased: " + kc);
            JSIScene curScene = 
                (JSIScene) this.mApp.getScenarioMgr().getCurScene();
            curScene.handleKeyUp(kc);
        }
        public void mouseMoved(Vector2 pt) {
            // Debug.Log("mouseMoved: " + pt);
            this.mApp.getCursor().getGameObject().transform.position = pt;
        }
        public void mouseLeftPressed(Vector2 pt) {
            // Debug.Log("mouseLeftPressed: " + pt);
            this.mApp.getCursor().getGameObject().transform.position = pt;
            if (this.mApp.getPenMarkMgr().penDown(pt)) {
                JSIScene curScene = 
                    (JSIScene) this.mApp.getScenarioMgr().getCurScene();
                curScene.handlePenDown(pt);
            }
        }
        public void mouseLeftDragged(Vector2 pt) {
            // Debug.Log("mouseLeftDragged: " + pt);
            this.mApp.getCursor().getGameObject().transform.position = pt;
            if (this.mApp.getPenMarkMgr().penDrag(pt)) {
                JSIScene curScene = 
                    (JSIScene) this.mApp.getScenarioMgr().getCurScene();
                curScene.handlePenDrag(pt);
            }
        }
        public void mouseLeftReleased(Vector2 pt) {
            // Debug.Log("mouseLeftReleased: " + pt);
            this.mApp.getCursor().getGameObject().transform.position = pt;
            if (this.mApp.getPenMarkMgr().penUp(pt)) {
                JSIScene curScene = 
                    (JSIScene) this.mApp.getScenarioMgr().getCurScene();
                curScene.handlePenUp(pt);
            }
        }
        public void mouseRightPressed(Vector2 pt) {
            Debug.Log("mouseRightPressed: " + pt);
            // if (this.mApp.getPenMarkMgr().penDown(pt)) {
            //     JSIScene curScene = 
            //         (JSIScene) this.mApp.getScenarioMgr().getCurScene();
            //     curScene.handlePenDown(pt);
            // }
        }
        public void mouseRightDragged(Vector2 pt) {
            Debug.Log("mouseRightDragged: " + pt);
            // if (this.mApp.getPenMarkMgr().penDrag(pt)) {
            //     JSIScene curScene = 
            //         (JSIScene) this.mApp.getScenarioMgr().getCurScene();
            //     curScene.handlePenDrag(pt);
            // }
        }
        public void mouseRightReleased(Vector2 pt) {
            Debug.Log("mouseRightReleased: " + pt);
            // if (this.mApp.getPenMarkMgr().penUp(pt)) {
            //     JSIScene curScene = 
            //         (JSIScene) this.mApp.getScenarioMgr().getCurScene();
            //     curScene.handlePenUp(pt);
            }
        }
    }