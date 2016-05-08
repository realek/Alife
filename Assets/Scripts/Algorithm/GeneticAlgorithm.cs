using UnityEngine;
using System.Linq;

namespace GA
{
    static class GeneticAlgorithm
    {
       private static float m_crossOverRate = 0.7f;
       private static float m_mutationRate = 0.2f;
       private static int m_tournamentSize = 4;
       private static bool m_elite = true;
       private static bool m_evolving = false;
       private static int m_generation = 0;

        /// <summary>
        /// Evolves population, via crossover and mutation.
        /// </summary>
        /// <param name="population"> population to be evolved.</param>
        public static Population EvolvePopulation(Population population)
        {

            if (m_evolving)
            {
                return null;
            }
            if (population == null || population.GetGenome(0) == null)
            {
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
 
            for (int i = iVal; i < population.PopulationSize; i=i+2)
            {
                
                Genome parent1 = SelectionByTournament(population);
                Genome parent2 = SelectionByTournament(population);
                if (GenomeSimilarityCalculator.CanReproduce(parent1, parent2))
                {
                    Genome[] children = CrossOver(parent1, parent2);

                    for (int j = 0; j < children.Length; j++)
                    {
                        nPop.InsertGenome(children[j]);
                    }
                }
                else
                {

                }
                
            }

            //mutate after breeding
            for (int i = iVal; i < nPop.PopulationSize; i++)
            {
            //    Mutate(nPop.GetGenome(i));
            }
      //      Debug.Log("Evolution Complete");

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


        private static bool Similarity(Genome genoA, Genome genoB)
        {
            if (genoA == null || genoB == null)
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Cross over function, breeds children from parent genomes, 
        /// uses a uniform crossover encapsulating a two point crossover with a 50% chance of occurence
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
            if(Random.value > m_crossOverRate)
            {
                children[0] = genoA;
                children[1] = genoB;

                return children;
            }
            else
            {
                //children genome data
                byte[] firstChildgenes = new byte[genoA.GenomeSize];
                byte[] secondChildgenes = new byte[genoA.GenomeSize];

                for (int i = 0; i < genoA.Genes.Length; i = i + GeneData.geneLength)
                {
                    //current gene id
                    byte[] geneAID = new byte[GeneData.geneIdentifierLength];
                    byte[] geneBID = new byte[GeneData.geneIdentifierLength];
                    for (int j = 0; j < geneAID.Length; j++)
                    {

                        geneAID[j] = genoA.Genes[i + j];
                        geneBID[j] = genoB.Genes[i + j];
                    }

                    //current gene value
                    byte[] geneAValue = new byte[GeneData.geneValueLength];
                    byte[] geneBValue = new byte[GeneData.geneValueLength];
                    for (int j = 0; j < GeneData.geneValueLength; j++)
                    {
                        geneAValue[j] = genoA.Genes[i + (geneAID.Length) + j];
                        geneBValue[j] = genoB.Genes[i + (geneBID.Length) + j];
                    }

                    if (geneAID.SequenceEqual(geneBID))
                    {
                        //encapsulated two point crossover within uniform crossover
                        switch (Random.Range(0, 2))
                        {
                            //two point crossover
                            case 0:
                                {
                                    //create random cuts within gene data
                                    int firstCutA = Random.Range(0, GeneData.geneValueLength - 1);
                                    int secondCutA = Random.Range(firstCutA, GeneData.geneValueLength);
                                    for (int k = firstCutA; k < secondCutA; k++)
                                    {
                                        byte aux = geneAValue[k];
                                        geneAValue[k] = geneBValue[k];
                                        geneBValue[k] = aux;
                                    }
                                    break;
                                }
                                //fully replace gene values in acordance to encapsulating uniform cross over
                            case 1:
                                {
                                    byte[] aux = geneAValue;
                                    geneAValue = geneBValue;
                                    geneBValue = aux;
                                    break;
                                }
                        }


                    }

                    for (int j = 0; j < GeneData.geneIdentifierLength; j++)
                    {
                        firstChildgenes[i+j] = geneAID[j];
                        secondChildgenes[i+j] = geneBID[j];
                    }

                    for (int j = 0; j < GeneData.geneValueLength; j++)
                    {
                        firstChildgenes[i+j+GeneData.geneIdentifierLength] = geneAValue[j];
                        secondChildgenes[i+j+GeneData.geneIdentifierLength] = geneBValue[j];
                    }

                }


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
            for (int i = 0; i < geno.GenomeSize; i++)
            {
                if (Random.value <= m_mutationRate)
                {
                    byte gene = (byte)Random.Range(0, 2);
                    geno.MutateGene(i, gene);
                   
                }
            }
        }


    }
}
