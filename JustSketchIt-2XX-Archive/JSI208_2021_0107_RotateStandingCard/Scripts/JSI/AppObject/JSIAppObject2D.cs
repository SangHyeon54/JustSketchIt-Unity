namespace JSI.AppObject {
    public abstract class JSIAppObject2D : JSIAppObject {
        public JSIAppObject2D(string name) : base(name) {
            this.mGameObject.layer = 5; // UI layer
        }
    }
}