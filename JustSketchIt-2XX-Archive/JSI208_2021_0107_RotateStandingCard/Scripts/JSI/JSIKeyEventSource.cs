//collect events from unity

using System.Collections.Generic;
using UnityEngine;

namespace JSI {
    public class JSIKeyEventSource {
        // constants
        private static readonly List<KeyCode> WATCHING_KEY_CODE = 
            new List<KeyCode>() {
                KeyCode.LeftControl,    // for rotation
                KeyCode.LeftAlt,        //for translation
                KeyCode.Return
            };
        
        // fields
        private JSIEventListener mEventListener = null;
        public void setEventListener(JSIEventListener eventListener) {
            this.mEventListener = eventListener;
        }

        // constructor
        public JSIKeyEventSource() {

        }

        // methods
        public void update() {
            foreach (KeyCode kc in JSIKeyEventSource.WATCHING_KEY_CODE) {
                if (Input.GetKeyDown(kc)) {
                    this.mEventListener.keyPressed(kc);
                }
                if (Input.GetKeyUp(kc)) {
                    this.mEventListener.keyReleased(kc);
                }
            }
        }
    }
}