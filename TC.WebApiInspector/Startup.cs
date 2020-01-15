using System;
using System.Collections.Generic;
using System.Text;
using TC.WebApiInspector.Inspectors;

namespace TC.WebApiInspector
{
    public class Startup
    {
        private IEnumerable<IInspector> inspectors;

        public Startup(IEnumerable<IInspector> inspectors)
        {
            this.inspectors = inspectors;
        }
        public void Run()
        {
            foreach(var inspector in inspectors)
            {
                inspector.Inspect();
            }
        }
    }
}
