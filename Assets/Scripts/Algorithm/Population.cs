﻿using System;
using System.Collections.Generic;

namespace GA
{
    class Population
    {
        private Random m_Randomizer;
        private List<Genome> m_Genomes;
        private Genome m_BestGenome;
        private int m_GenomeSize;
        private int m_Popsize;
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

            //List<int> badGenerated = new List<int>(0);

            // m_BestGenome = m_Genomes[0];
            // GenomeEncoder.Encode(ref m_BestGenome);
            //  Fitness.FoodFitness(ref m_BestGenome,FitnessFunction.FoodGathering);// get first chromosome
            List<Genome> properGenomes = new List<Genome>();
            for (int i = 0; i < m_Genomes.Count; i++)
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
