using System;

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
            m_BestGenome = m_Genomes[0];
            m_BestGenome.Fitness = Fitness.Calculate(m_BestGenome.Genes);// get first chromosome
            for (int i = 1; i < m_Genomes.Length; i++)
            {
                m_Genomes[i].Fitness = Fitness.Calculate(m_Genomes[i].Genes);
                if (m_BestGenome.Fitness < m_Genomes[i].Fitness)
                {
                    m_BestGenome = m_Genomes[i]; // record best chromosome
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
