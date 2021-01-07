using System.Text;
using X;
using UnityEngine;

namespace JSI.Scenario
{
    internal class JSICmdToDoSomething : XLoggableCmd {

        // private constructor
        private JSICmdToDoSomething(XApp app) : base(app)
        {
            JSIApp jsi = (JSIApp)this.mApp;
        }

        public static bool execute(XApp app)
        {
            JSICmdToDoSomething cmd = new JSICmdToDoSomething(app);
            return cmd.execute();
        }

        protected override bool defineCmd()
        {
            throw new System.NotImplementedException();
        }

        protected override string createLog()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            return sb.ToString();
        }
    }
}