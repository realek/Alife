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
                _desiredGenes[i] = Byte.Parse(goal[i].ToString());
            }
        }

        public static void FoodFitness(ref Genome Geno,FitnessFunction funcType)
        {
            switch (funcType)
            {
                case FitnessFunction.FoodGathering:
                    {
                        Geno.Fitness = Geno.encoded.Speed;
                        break;
                    }
                case FitnessFunction.Meteor:
                    {

                        break;
                    }
            }
        }

        public static int MaxFitness()
        {
            int maxFitness = _desiredGenes.Length;
            return maxFitness;
        }
    }
    
}
