using System;

namespace GA
{
    enum FitnessFunction
    {
        FoodGathering,
        Meteor
    }
    static class Fitness
    {

        public static float FoodFitness(Genome Geno,FitnessFunction funcType)
        {
            float fitness=0;

            switch (funcType)
            {
                case FitnessFunction.FoodGathering:
                    {
                        fitness =  Geno.encoded.Speed;
                        break;
            
                    }
            }
            return fitness;
        }

    }
    
}
