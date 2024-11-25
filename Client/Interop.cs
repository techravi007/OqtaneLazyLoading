using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace LLM.Module.LazyLoadingTest
{
    public class Interop
    {
        private readonly IJSRuntime _jsRuntime;

        public Interop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
    }
}
