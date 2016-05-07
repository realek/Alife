using UnityEngine;

namespace GA
{
    static class GeneticAlgorithm
    {
       private static float m_crossOverRate = 0.7f;
       private static float m_mutationRate = 0.255f;
       private static int m_tournamentSize = 4;
       private static bool m_elite = true;
       private static bool m_evolving = false;
       private static int m_generation = 0;

        /// <summary>
        /// Evolves population
        /// </summary>
        /// <param name="population"> population to be evolved (use ref keyword)</param>
        public static Population EvolvePopulation(Population population)
        {

            if (m_evolving)
            {
                return null;
            }
            if (population == null || population.GetGenome(0) == null)
            {
               // Debug.LogError("POPULATION IS NULL, WTF ARE YOU DOING?");
                return null;
            }

            m_evolving = true;
            Population nPop = new Population(population.PopulationSize, population.GenomeSize());


            if (m_elite)
            {
                nPop.InsertGenome(population.BestGenome);
            }


            int iVal = 0;
            //offset for best fitt individual
            if (m_elite)
            {
                iVal = 1;
            }

            ///Breeding process
 
            for (int i = iVal; i < population.PopulationSize; i=i+1)
            {
                Genome parent1 = SelectionByTournament(population);
                Genome parent2 = SelectionByTournament(population);
                Genome[] children = CrossOver(parent1, parent2);
                nPop.InsertGenome(children[0]);
                nPop.InsertGenome(children[1]);
            }

            //mutate after breeding
            for (int i = iVal; i < nPop.PopulationSize; i++)
            {
                Mutate(nPop.GetGenome(i));
            }
            Debug.Log("Evolution Complete");

            m_generation++;
            m_evolving = false;
            return nPop;
        }

        /// <summary>
        /// used to reset generation count, rought implem, needs rework
        /// </summary>
        public static void ResetGenerationCount()
        {
            m_generation = 0;
        }

        public static int Generation
        {
            get
            {
                return m_generation;
            }
        }


        /// <summary>
        /// Selects the fittest genome out of a given number of genomes within a population
        /// </summary>
        /// <param name="population"></param>
        /// <returns></returns>
        private static Genome SelectionByTournament(Population population)
        {

            Genome geno = null;
            Population tournamentCanditates = new Population(m_tournamentSize,population.GenomeSize());

            for (int i = 0; i < m_tournamentSize; i++)
            {
                int rgeno = Random.Range(0, population.PopulationSize);
                tournamentCanditates.InsertGenome(population.GetGenome(rgeno));
            }

            tournamentCanditates.EvaluatePopulation();
            geno = tournamentCanditates.BestGenome;

            return geno;
        }

        /// <summary>
        /// Cross over function, breeds children from parent genomes
        /// </summary>
        /// <param name="genoA"></param>
        /// <param name="genoB"></param>
        /// <returns></returns>
        private static Genome[] CrossOver(Genome genoA, Genome genoB)
        {
            if (genoA == null || genoB == null)
            {
                return null;
            }

            Genome[] children = new Genome[2]; // 2 parents two children simple crossover

            int genomeSize = genoA.GenomeSize;
            if(Random.value > m_crossOverRate)
            {
                children[0] = genoA;
                children[1] = genoB;

                return children;
            }
            else
            {
                byte[] firstChildgenes = new byte[genoA.GenomeSize];
                byte[] secondChildgenes = new byte[genoA.GenomeSize];
                byte[] mandatoryGeneValues = new byte[GeneData.geneValueLength * GeneData.NrMandatoryGenes];



                children[0] = new Genome(firstChildgenes);
                children[1] = new Genome(secondChildgenes); 

                return children;
            }


        }

        /// <summary>
        /// Mutation function called after crossover, mutates genome randomly
        /// </summary>
        /// <param name="geno"></param>
        private static void Mutate(Genome geno)
        {
            System.Random rand = new System.Random();
            for (int i = 0; i < geno.GenomeSize; i++)
            {
                if (rand.NextDouble() <= m_mutationRate)
                {
                    ///must be replaced by gene class
                    byte gene = (byte)rand.Next(0, 2);
                    geno.MutateGene(i, gene);
                   
                }
            }
        }


    }
}
