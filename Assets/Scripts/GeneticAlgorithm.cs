using System;
using UnityEngine;

namespace GA
{
    static class GeneticAlgorithm
    {
       private static float m_crossOverRate = 0.7f;
       private static float m_mutationRate = 0.255f;
       private static int m_tournamentSize = 10;
       private static bool m_elite = true;
       private static bool m_evolving = false;
       private static int m_generation = 0;
       public static void EvolvePopulation(ref Population population)
        {
            if (m_evolving)
            {
                Debug.Log("is evolving");
                return;
            }

            if (population == null || population.GetGenome(0) == null)
            {
                Debug.Log("null pop");
                return;
            }

            m_evolving = true;

            Population nPop = new Population(population.PopulationSize, population.GenomeSize());


            if (m_elite)
            {
                nPop.InsertGenome(0, population.BestGenome);
            }


            int iVal = 0;
            //offset for best fitt individual
            if (m_elite)
            {
                iVal = 1;
            }

            ///Breeding process
 
            for (int i = iVal; i < population.PopulationSize; i=i+2)
            {
                Genome parent1 = SelectionByTournament(population);
                Genome parent2 = SelectionByTournament(population);
                Genome[] children = CrossOver(parent1, parent2);
                nPop.InsertGenome(i, children[0]);
                if(i+1<population.PopulationSize)
                    nPop.InsertGenome(i + 1, children[1]);
            }

            //mutate after breeding
            for (int i = iVal; i < nPop.PopulationSize; i++)
            {
                Mutate(nPop.GetGenome(i));
            }

            population = nPop;
            m_generation++;
            m_evolving = false;
        }

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

        private static Genome SelectionByTournament(Population population)
        {

            Genome geno = null;
            Population tournamentCanditates = new Population(m_tournamentSize,population.GenomeSize());
            System.Random random = new System.Random();

            for (int i = 0; i < m_tournamentSize; i++)
            {
                int rgeno = random.Next(0, population.PopulationSize);
                tournamentCanditates.InsertGenome(i, population.GetGenome(rgeno));
            }

            tournamentCanditates.EvaluatePopulation();

            geno = tournamentCanditates.BestGenome;

            return geno;
        }

        private static Genome[] CrossOver(Genome genoA, Genome genoB)
        {
            if (genoA == null || genoB == null)
            {
                return null;
            }

            Genome[] children = new Genome[2]; // 2 parents two children simple crossover

            int genomeSize = genoA.GenomeSize;
            System.Random random = new System.Random();

            if (random.NextDouble() > m_crossOverRate)
            {
                children[0] = genoA;
                children[1] = genoB;

                return children;
            }
            else
            {
                int randCut1 = genoA.GenomeSize;
                while (randCut1 > genomeSize - 1) //first cut
                {
                    randCut1 = random.Next(0, genomeSize + 1);
                }
                int randcut2 = random.Next(randCut1 + 1, genomeSize + 1); // second cut

                ///Genes Override after class Implementation
                byte[] firstChildgenes = new byte[genoA.GenomeSize];
                byte[] secondChildgenes = new byte[genoA.GenomeSize];
                /// 

                
                for (int i = 0; i < genoA.GenomeSize; i++)
                {
                    if (i >= randCut1 || i <= randcut2)
                    {
                        firstChildgenes[i] = genoB.Genes[i];
                        secondChildgenes[i] = genoA.Genes[i];
                    }
                    else
                    {
                        firstChildgenes[i] = genoA.Genes[i];
                        secondChildgenes[i] = genoB.Genes[i];
                    }
                }

                children[0] = new Genome(firstChildgenes);
                children[1] = new Genome(secondChildgenes); 

                return children;
            }


        }

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
                    ///
                }
            }
        }


    }
}
