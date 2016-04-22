using System;

namespace GA
{

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

        public static int Calculate(byte[] _genes)
        {
            int _calculatedFitness = 0;
            for(int i = 0; i < _genes.Length; i++)
            {
                if (_genes[i] == _desiredGenes[i])
                {
                    _calculatedFitness++;
                }
            }
            return _calculatedFitness;
        }

        public static int MaxFitness()
        {
            int maxFitness = _desiredGenes.Length;
            return maxFitness;
        }
    }
    
}
