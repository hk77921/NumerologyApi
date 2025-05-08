// Services/INumerologyService.cs
using NumerologyApi.Models;

namespace NumerologyApi.Services
{
    public interface INumerologyService
    {
        NumerologyResult Analyze(NumerologyRequest request);
         VedicGridResponse GetVedicGridAnalysis(string mobile);
    }
}