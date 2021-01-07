using X;
using UnityEngine;
using JSI.AppObject;
using JSI.Geom;
using System.Collections.Generic;

namespace JSI.Scenario {
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

        // fields
        private JSIStandingCard mSelectedStandingCard = null;
        public JSIStandingCard getSelectedStandingCard() {
            return this.mSelectedStandingCard;
        }
        public void setSelectedStandingCard(JSIStandingCard sc) {
            this.mSelectedStandingCard = sc;
        }

        // methods
        private JSIStandingCard selectStandingCardByStand() {
            JSIApp app = (JSIApp)this.mApp;
            List<JSIStandingCard> hitStandingCards =
                new List<JSIStandingCard>();
            foreach (JSIStandingCard standingCard in
                app.getStandingCardMgr().getStandingCards()) {
                if (app.getCursor().hits(standingCard.getStand())) {
                    hitStandingCards.Add(standingCard);
                }
            }

            if (hitStandingCards.Count == 0) {
                return null;
            }

            // find and return the smallest standing card among ths hit ones.
            float minWidth = Mathf.Infinity;
            JSIStandingCard smallestStandingCard = null;
            foreach (JSIStandingCard standingCard in
                hitStandingCards) {
                JSIAppRect3D card = standingCard.getCard();
                JSIRect3D rect = (JSIRect3D)card.getGeom3D();
                if (rect.getWidth() < minWidth) {
                    smallestStandingCard = standingCard;
                    minWidth = rect.getWidth();
                }
            }
            return smallestStandingCard;
        }
    }
}