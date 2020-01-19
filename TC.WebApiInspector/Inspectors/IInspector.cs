using System.Threading.Tasks;

namespace TC.WebApiInspector.Inspectors
{
    public interface IInspector
    {
        Task Inspect();
    }
}
