using UnityEngine;

namespace JSI {
    public class JSIMouseEventSource {
        // constants
        private static readonly int LEFT_BUTTON = 0;
        private static readonly int RIGHT_BUTTON = 1;

        // fields
        private JSIEventListener mEventListener = null;
        public void setEventListener(JSIEventListener eventListener) {
            this.mEventListener = eventListener;
        }
        private bool mWasLeftPressed = false;
        private bool mIsLeftPressed = false;
        private bool mWasRightPressed = false;
        private bool mIsRightPressed = false;
        private Vector2 mPrevMousePt = Vector2.zero;
        private Vector2 mCurMousePt = Vector2.zero;

        // constructor
        public JSIMouseEventSource() {
        }

        // methods
        public void update() {
            this.mIsLeftPressed = Input.GetMouseButton(
                JSIMouseEventSource.LEFT_BUTTON);
            this.mIsRightPressed = Input.GetMouseButton(
                JSIMouseEventSource.RIGHT_BUTTON);
            this.mCurMousePt = Input.mousePosition;

            //move
            if (this.mPrevMousePt != this.mCurMousePt) {
                this.mEventListener.mouseMoved(this.mCurMousePt);
            }

            // left press
            // if (this.mWasLeftPressed == false &&
            //     this.mIsLeftPressed ==true) {
            if (!this.mWasLeftPressed && this.mIsLeftPressed) {
                this.mEventListener.mouseLeftPressed(this.mCurMousePt);
            }

            // left drag
            if ((this.mPrevMousePt != this.mCurMousePt) &&
                this.mWasLeftPressed && this.mIsLeftPressed) {
                this.mEventListener.mouseLeftDragged(this.mCurMousePt);
            }

            // left release
            if (this.mWasLeftPressed && !this.mIsLeftPressed) {
                this.mEventListener.mouseLeftReleased(this.mCurMousePt);
            }

            // right press
            if (!this.mWasRightPressed && this.mIsRightPressed) {
                this.mEventListener.mouseRightPressed(this.mCurMousePt);
            }

            // right drag
            if ((this.mPrevMousePt != this.mCurMousePt) &&
                this.mWasRightPressed && this.mIsRightPressed) {
                this.mEventListener.mouseRightDragged(this.mCurMousePt);
            }

            // right release
            if (this.mWasRightPressed && !this.mIsRightPressed) {
                this.mEventListener.mouseRightReleased(this.mCurMousePt);
            }

            // updates the previous data
            this.mWasLeftPressed = this.mIsLeftPressed;
            this.mWasRightPressed = this.mIsRightPressed;
            this.mPrevMousePt = this.mCurMousePt;
        }
    }
}