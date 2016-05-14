using UnityEngine;


namespace GA
{
    [System.Serializable]
    public class Genome
    {
        private int m_GenomeSize;
        private float m_GenomeFitness;
        [SerializeField]
        private byte[] m_Genes;
        public EncodedGenome encoded;
        private const int m_ValSize = 6;
        private const int m_IDSize = 3;
        private const int m_sizeIDIDX = 0;
        private const int m_weightIDIDX = 9;
        private const int m_powerIDIDX = 18;
        private const int m_lifeSpanIDIDX = 27;
        private const int m_colorIDIDX = 36;
        private const int m_armsIDIDX = 45;
        private const int m_legsIDIDX = 54;
        public bool discarded;
        public bool dead;
        private float m_lifespan;

        public Genome(int seed, int size)
        {
            
            discarded = false;
            dead = false;
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

        /// <summary>
        /// Generates the gene data for the genome, weight,power,lifespan,color, arms and legs.
        /// </summary>
        private void GenerateGenome()
        {
            for (int i = 0; i < m_IDSize; i++)
            {
                m_Genes[i] = GeneData.sizeGeneID[i];
                m_Genes[i + m_weightIDIDX] = GeneData.weightGeneID[i];
                m_Genes[i + m_powerIDIDX] = GeneData.powerGeneID[i];
                m_Genes[i + m_lifeSpanIDIDX] = GeneData.lifeSpanGeneID[i];
                m_Genes[i + m_colorIDIDX] = GeneData.colorGeneID[i];
                m_Genes[i + m_armsIDIDX] = GeneData.armsGeneID[i];
                m_Genes[i + m_legsIDIDX] = GeneData.legsGeneID[i];
            }

            for (int i = 0; i < m_ValSize; i++)
            {
                m_Genes[i + m_IDSize] = (byte)Random.Range(0, 2);
                m_Genes[i + m_weightIDIDX + m_IDSize] = (byte)Random.Range(0, 2);
                m_Genes[i + m_powerIDIDX + m_IDSize] = (byte)Random.Range(0, 2);
                m_Genes[i + m_lifeSpanIDIDX + m_IDSize] = (byte)Random.Range(0, 2);
                m_Genes[i + m_colorIDIDX + m_IDSize] = (byte)Random.Range(0, 2);
                m_Genes[i + m_armsIDIDX + m_IDSize] = (byte)Random.Range(0, 2);
                m_Genes[i + m_legsIDIDX + m_IDSize] = (byte)Random.Range(0, 2);
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

        public float LifeSpan
        {
            get
            {
                return m_lifespan;
            }
            set
            {
                m_lifespan = value;
            }
        }

    }
}