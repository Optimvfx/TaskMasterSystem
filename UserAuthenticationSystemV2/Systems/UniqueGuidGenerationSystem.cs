using System;
using System.Collections.Generic;
using System.Linq;
using UserAuthenticationSystemV2.Systems.Base;

namespace UserAuthenticationSystemV2.Systems
{
    public class UniqueGuidGenerationSystem
    {
        public Guid GenerateUniqueId(IEnumerable<Guid> generatedGuids, int maxGenerationIterations = 10)
        {
            if (maxGenerationIterations < 0)
                throw new ArgumentException();

            Guid id = Guid.NewGuid();

            var generationIterations = 0;

            while (generatedGuids.Contains(id))
            {
                id = Guid.NewGuid();

                generationIterations++;

                if (generationIterations >= maxGenerationIterations)
                    throw new StackOverflowException();
            }

            return id;
        }
    }
}