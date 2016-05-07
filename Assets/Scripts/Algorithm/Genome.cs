using UnityEngine;


namespace GA
{
    [System.Serializable]
    class Genome
    {
        private int m_GenomeSize;
        private float m_GenomeFitness;
        [SerializeField]
        private byte[] m_Genes;
        public EncodedGenome encoded;
        private readonly static byte[] sizeGeneID = new byte[3] { 0, 0, 1 };
        private readonly static byte[] weightGeneID = new byte[3] { 0, 1, 0 };
        private readonly static byte[] powerGeneID = new byte[3] { 0, 1, 1 };
        private const int m_ValSize = 6;
        private const int m_IDSize = 3;
        private const int m_sizeIDIDX = 0;
        private const int m_weightIDIDX = 9;
        private const int m_powerIDIDX = 18;

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
                m_Genes[i] = sizeGeneID[i];
                m_Genes[i + m_weightIDIDX] = weightGeneID[i];
                m_Genes[i + m_powerIDIDX] = powerGeneID[i];
            }

            for (int i = 0; i < m_ValSize; i++)
            {
                m_Genes[i+m_IDSize] = byte.Parse(Random.Range(0, 2).ToString());
                m_Genes[i+m_weightIDIDX+m_IDSize] = byte.Parse(Random.Range(0, 2).ToString());
                m_Genes[i+m_powerIDIDX+m_IDSize] = byte.Parse(Random.Range(0, 2).ToString());

            }

            for (int i = m_powerIDIDX+m_IDSize+m_ValSize; i < m_Genes.Length; i++)
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