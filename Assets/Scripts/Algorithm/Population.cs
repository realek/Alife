using System;
using System.Collections.Generic;
using UnityEngine;

namespace GA
{
    [Serializable]
    public class Population
    {
        private System.Random m_Randomizer;
        [SerializeField]
        private List<Genome> m_Genomes;
        private Genome m_BestGenome;
        private int m_GenomeSize;
        private int m_Popsize;
        private float m_PopAvgFitness;
        public Population(int popSize, int genoSize)
        {
            m_GenomeSize = genoSize;
            m_Popsize = popSize;
            m_Genomes = new List<Genome>();
        }


        public void GenerateInitalPopulation()
        {
            for (int i = 0; i < m_Popsize; i++)
            {
                m_Genomes.Add(new Genome(UnityEngine.Random.Range(0, 100), m_GenomeSize)); 
            }
        }


        public Genome GetGenome(int index)
        {
            return m_Genomes[index];
        }

        public void EvaluatePopulation()
        {
            List<Genome> properGenomes = new List<Genome>();
            for (int i = 0; i < m_Genomes.Count; i++)
            {
                if (m_Genomes[i].encoded != null)
                {
                    properGenomes.Add(m_Genomes[i]);
                }
                else
                {
                    m_Genomes[i].encoded = GenomeEncoder.Encode(m_Genomes[i]);
                    if (m_Genomes[i].encoded == null)
                    {
                        m_Genomes[i].discarded = true;
                    }

                    if (!m_Genomes[i].discarded)
                    {
                        properGenomes.Add(m_Genomes[i]);
                    }
                }

            }

            m_Genomes = properGenomes;
            for (int i = 0; i < m_Genomes.Count; i++)
            {
                m_Genomes[i].Fitness = Fitness.FoodFitness(m_Genomes[i], FitnessFunction.FoodGathering);
                if (i == 0)
                {
                    m_BestGenome = m_Genomes[i];
                }
                else if(m_BestGenome.Fitness < m_Genomes[i].Fitness)
                {
                    m_BestGenome = m_Genomes[i];
                }
            }
           }

        /// <summary>
        /// Must always be called after the first evaluation, but always before the second,
        /// checks if the genome is about to die
        /// </summary>

        public void InsertGenome(Genome chromo)
        {
            m_Genomes.Add(chromo);
        }

        public int PopulationSize
        {
            get
            {
                return m_Genomes.Count;
            }
        }

        public int GenomeSize()
        {
            return m_Genomes[0].GenomeSize;
        }

        public Genome BestGenome
        {
            get
            {
                return m_BestGenome;
            }
            
        }
       
    }
}
