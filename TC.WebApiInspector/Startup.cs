using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public async Task Run()
        {
            var inspectorTasks = new List<Task>();
            foreach(var inspector in inspectors)
            {
                inspectorTasks.Add(inspector.Inspect());
            }

            await Task.WhenAll(inspectorTasks);
        }
    }
}
