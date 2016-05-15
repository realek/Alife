using System;

namespace GA
{
    static class Fitness
    {

        public static float FoodFitness(Genome Geno)
        {
            if (Geno.encoded.CanClimb)
            {
                return Geno.encoded.Speed + 1;
            }
            else
            {
                return Geno.encoded.Speed;
            }

        }

    }
    
}
