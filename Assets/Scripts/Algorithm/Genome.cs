﻿using System;

namespace GA
{

    class Genome
    {
        private int m_GenomeSize;
        private int m_GenomeFitness;
        private byte[] m_Genes;
        private Random m_Randomizer;

        public Genome(int seed, int size)
        {
            m_GenomeSize = size;
            m_Genes = new byte[m_GenomeSize];
            m_Randomizer = new Random(seed);
            GenerateGenome();
        }

        public Genome(byte[] Genes)
        {
            m_Genes = Genes;
            m_GenomeSize = m_Genes.Length;
        }
        public void ModifyChromo()
        {
            throw new NotImplementedException();
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
            for (int i = 0; i < m_Genes.Length; i++)
            {
                m_Genes[i] = byte.Parse(m_Randomizer.Next(2).ToString());
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

        public int Fitness
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

        public string Phenotype()
        {
            string pheno="";
            for (int i = 0; i < m_Genes.Length; i++)
            {
                int gene = Convert.ToInt32(m_Genes[i]);

                switch (gene)
                {
                    case 0:
                        {
                            pheno += "Y";
                            break;
                        }
                    case 1:
                        {
                            pheno += "X";
                            break;
                        }
                }
            }
            return pheno;
        }


    }
}
