using UnityEngine;


namespace GA
{
    [System.Serializable]
    class Genome
    {
        private int m_GenomeSize;
        private float m_GenomeFitness;
        private byte[] m_Genes;
        public EncodedGenome encoded;
        private const int m_ValSize = 6;
        private const int m_IDSize = 3;
        private const int m_sizeIDIDX = 0;
        private const int m_weightIDIDX = 9;
        private const int m_powerIDIDX = 18;
        private const int m_lifeSpanIDIDX = 27;
        public bool discarded;

        public Genome(int seed, int size)
        {
            
            discarded = false;
            m_GenomeSize = size;
            m_Genes = new byte[m_GenomeSize];
            GenerateGenome();
        }

        public Genome(byte[] Genes)
        {
            m_Genes = Genes;
            m_GenomeSize = m_Genes.Length;
        }

        public int GenomeSize
        {
            get
            {
                return m_GenomeSize;
            }
        }

        private void GenerateGenome()
        {
            for (int i = 0; i < m_IDSize; i++)
            {
                m_Genes[i] = GeneData.sizeGeneID[i];
                m_Genes[i + m_weightIDIDX] = GeneData.weightGeneID[i];
                m_Genes[i + m_powerIDIDX] = GeneData.powerGeneID[i];
                m_Genes[i + m_lifeSpanIDIDX] = GeneData.lifeSpanGeneID[i];
            }

            for (int i = 0; i < m_ValSize; i++)
            {
                m_Genes[i+m_IDSize] = byte.Parse(Random.Range(0, 2).ToString());
                m_Genes[i+m_weightIDIDX+m_IDSize] = byte.Parse(Random.Range(0, 2).ToString());
                m_Genes[i+m_powerIDIDX+m_IDSize] = byte.Parse(Random.Range(0, 2).ToString());
                m_Genes[i+m_lifeSpanIDIDX+m_IDSize] = byte.Parse(Random.Range(0, 2).ToString());
            }



            for (int i = m_lifeSpanIDIDX+m_IDSize+m_ValSize; i < m_Genes.Length; i++)
            {
                m_Genes[i] = byte.Parse(Random.Range(0,2).ToString());
            }

            

            m_GenomeFitness = 0;
        }

        public byte[] Genes
        {
            get
            {
                return m_Genes;
            }
        }

        public void MutateGene(int i,byte b)
        {
            m_Genes[i] = b;
        }


        public float Fitness
        {
            set
            {
                m_GenomeFitness = value;
            }
            get
            {
                return m_GenomeFitness;
            }
        }

    }
}