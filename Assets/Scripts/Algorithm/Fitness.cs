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
        private static byte[] _desiredGenes;

        public static void LoadGoal(int[] goal)
        {
            _desiredGenes = new byte[goal.Length];
            for(int i = 0; i < goal.Length; i++)
            {
                _desiredGenes[i] = byte.Parse(goal[i].ToString());
            }
        }

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

        public static int MaxFitness()
        {
            int maxFitness = _desiredGenes.Length;
            return maxFitness;
        }
    }
    
}
