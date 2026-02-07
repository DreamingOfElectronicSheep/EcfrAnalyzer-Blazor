using EcfrAnalyzer.Models;
using System.Collections.Generic;

namespace EcfrTests.Builders
{
    internal static class AgenciyResponseModelBuilder
    {
        internal static AgenciesResponseModel CreateValidAgenciesResponse()
        {
            return new AgenciesResponseModel
            {
                Agencies = new List<Agency>
                {
                    new Agency
                    {
                        Name = "Test Agency",
                        ShortName = "TA",
                        DisplayName = "Test Agency Display",
                        Slug = "test-agency",
                        Children = new(),
                        CfrReferences = new()
                    }
                }
            };
        }

        internal static CorrectionsResponseModel CreateValidCorrectionsResponse()
        {
            return new CorrectionsResponseModel
            {
                EcfrCorrections = new List<EcfrCorrection>
                {
                    new EcfrCorrection
                    {
                        Id = 1,
                        CorrectiveAction = "Amended",
                        FrCitation = "70 FR 55706"
                    }
                }
            };
        }
    }
}
