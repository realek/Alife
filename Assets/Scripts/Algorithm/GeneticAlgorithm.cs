using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace GA
{
    static class GeneticAlgorithm
    {
       private static float m_crossOverRate = 0.7f;
       private static float m_mutationRate = 0.225f;
       private static int m_tournamentSize = 4;
       private static bool m_elite = true;
       private static bool m_evolving = false;
       private static int m_generation = 0;
       private static int m_maxPopulation = 250;
        /// <summary>
        /// Main function of the algorithm, handles breeding within the current population, then mutation(we first breed than we mutate)
        /// </summary>
        /// <param name="population"> population to be evolved.</param>
        public static Population EvolvePopulation(Population population)
        {

            if (m_evolving)
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
            List<Genome> parents = new List<Genome>(0);
            for (int i = iVal; i < population.PopulationSize; i=i+2)
            {
                
                Genome parent1 = SelectionByTournament(population);
                Genome parent2 = SelectionByTournament(population);
                if (GenomeSimilarityCalculator.CanReproduce(parent1, parent2))
                {
                    Genome[] children = CrossOver(parent1, parent2);

                    if (!parents.Contains(parent1))
                    {
                        parents.Add(parent1);
                    }

                    if (!parents.Contains(parent1))
                    {
                        parents.Add(parent1);
                    }

                    for (int j = 0; j < children.Length; j++)
                    {
                        nPop.InsertGenome(children[j]);
                        if (nPop.PopulationSize == m_maxPopulation)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    if (!parents.Contains(parent1))
                    {
                        parents.Add(parent1);
                    }

                    if (!parents.Contains(parent1))
                    {
                        parents.Add(parent1);
                    }
                }

                if (nPop.PopulationSize >= m_maxPopulation)
                {
                    break;
                }

            }
            for (int pop = m_maxPopulation - nPop.PopulationSize; pop > 0; pop--)
            {
                for (int i = 0; i < parents.Count; i++)
                {
                    if (parents[i].Fitness >= population.AverageFitness)
                    {
                        nPop.InsertGenome(parents[i]);
                        parents.RemoveAt(i);
                        break;
                    }
                }
            }
            //mutate after breeding
            for (int i = 0; i < nPop.PopulationSize; i=i+2)
            {
                Mutate(nPop.GetGenome(i));

            }
 


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
        /// Selects the fittest genome out of a randomly selected group of genomes from the current population
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
            //  int noOfchildren = (int)(((genoA.Fitness + genoB.Fitness)/4.0f)+((genoA.encoded.LifeSpan+genoB.encoded.LifeSpan)/4.0f));
            int noOfchildren = 2;
            Genome[] children = new Genome[noOfchildren]; // 2 parents two children simple crossover
            if(Random.value > m_crossOverRate)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            {
                                children[i] = genoA;
                                break;
                            }

                        case 1:
                            {
                                children[i] = genoB;
                                break;
                            }
                    }
                }
               
                return children;
            }
            else
            {
                //children genome data
                byte[][] childrenGenes = new byte[noOfchildren][];
                byte[] firstChildgenes = new byte[genoA.GenomeSize];
                byte[] secondChildgenes = new byte[genoA.GenomeSize];

                for (int i = 0; i < children.Length; i++)
                {
                    childrenGenes[i] = new byte[genoA.GenomeSize];
                    for (int j = 0; j < genoA.Genes.Length; j = j + GeneData.geneLength)
                    {
                        byte[] geneAID = new byte[GeneData.geneIdentifierLength];
                        byte[] geneBID = new byte[GeneData.geneIdentifierLength];
                        for (int k = 0; k < geneAID.Length; k++)
                        {

                            geneAID[k] = genoA.Genes[j + k];
                            geneBID[k] = genoB.Genes[j + k];
                        }

                        //current gene value
                        byte[] geneAValue = new byte[GeneData.geneValueLength];
                        byte[] geneBValue = new byte[GeneData.geneValueLength];
                        for (int k = 0; k < GeneData.geneValueLength; k++)
                        {
                            geneAValue[k] = genoA.Genes[j + (geneAID.Length) + k];
                            geneBValue[k] = genoB.Genes[j + (geneBID.Length) + k];
                        }

                        if (geneAID.SequenceEqual(geneBID))
                        {
                            //two point crossover
                            int firstCutA = Random.Range(0, GeneData.geneValueLength - 1);
                            int secondCutA = Random.Range(firstCutA, GeneData.geneValueLength);
                            for (int k = firstCutA; k < secondCutA; k++)
                            {
                                byte aux = geneAValue[k];
                                geneAValue[k] = geneBValue[k];
                                geneBValue[k] = aux;
                            }


                        }

                        for (int k = 0; k < GeneData.geneIdentifierLength; k++)
                        {
                            childrenGenes[i][k + j] = geneAID[k];
                            childrenGenes[i][k + j] = geneBID[k];
                        }

                        for (int k = 0; k < GeneData.geneValueLength; k++)
                        {
                            childrenGenes[i][j + k + GeneData.geneIdentifierLength] = geneAValue[k];
                            childrenGenes[i][j + k + GeneData.geneIdentifierLength] = geneBValue[k];
                        }
                    }
                    children[i] = new Genome(childrenGenes[i]);
                }

                return children;
            }


        }

        /// <summary>
        /// Mutation function called after crossover, mutates genome randomly
        /// by shifting the gene values for the mandatory genes, then shiting bytes in both id+value for the remaining genes
        /// </summary>
        /// <param name="geno"></param>
        private static void Mutate(Genome geno)
        {
            for (int i = 0; i < geno.Genes.Length; i = i + GeneData.geneLength)
            {
                byte[] geneID = new byte[GeneData.geneIdentifierLength];
                for (int j = 0; j < geneID.Length; j++)
                {

                    geneID[j] = geno.Genes[i + j];
                }

                for (int j = 0; j < GeneData.geneValueLength; j++)
                {
                    if (Random.value <= m_mutationRate)
                    {
                        geno.MutateGene(i + (geneID.Length) + j, (byte)Random.Range(0, 2));
                    }
                }
            }
        }


    }
}
