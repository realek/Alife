﻿using System;
using System.Collections.Generic;

namespace GA
{
    class Population
    {
        private Random m_Randomizer;
        private Genome[] m_Genomes;
        private Genome m_BestGenome;
        private int m_GenomeSize;

        public Population(int popSize, int genoSize)
        {
            m_GenomeSize = genoSize;
            m_Genomes = new Genome[popSize];
        }


        public void GenerateInitalPopulation()
        {
            m_Randomizer = new Random();

            for (int i = 0; i < m_Genomes.Length; i++)
            {
                m_Genomes[i] = new Genome(m_Randomizer.Next(), m_GenomeSize); 
            }
        }

        public Genome GetGenome(int index)
        {
            return m_Genomes[index];
        }

        public void EvaluatePopulation()
        {

            float bestFitness = 0;
            //List<int> badGenerated = new List<int>(0);

            // m_BestGenome = m_Genomes[0];
            // GenomeEncoder.Encode(ref m_BestGenome);
            //  Fitness.FoodFitness(ref m_BestGenome,FitnessFunction.FoodGathering);// get first chromosome
            List<Genome> properGenomes = new List<Genome>();
            for (int i = 0; i < m_Genomes.Length; i++)
            {
                GenomeEncoder.Encode(ref m_Genomes[i]);
                if (!m_Genomes[i].discarded)
                {
                    properGenomes.Add(m_Genomes[i]);
                }
            }

            m_Genomes = properGenomes.ToArray();
            for (int i = 0; i < m_Genomes.Length; i++)
            {
                Fitness.FoodFitness(ref m_Genomes[i], FitnessFunction.FoodGathering);
                UnityEngine.Debug.Log(m_Genomes[i].Fitness);
                if (m_BestGenome != null)
                {
                    if (m_BestGenome.Fitness < m_Genomes[i].Fitness)
                    {
                        m_BestGenome = m_Genomes[i];
                    }

                }
                else
                {
                    if (i == 0)
                    {
                        bestFitness = m_Genomes[i].Fitness;
                    }
                    else if (bestFitness < m_Genomes[i].Fitness)
                    {
                        m_BestGenome = m_Genomes[i]; // record best chromosome
                    }
                }
            }
           }

        public void InsertGenome(int index, Genome chromo)
        {
            m_Genomes[index] = chromo;
        }

        public int PopulationSize
        {
            get
            {
                return m_Genomes.Length;
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
