using System;
using System.Linq;

namespace GA
{

    static class GeneData
    {
        public readonly static byte[] colorGeneID = new byte[3] { 0, 0, 0 };
        public readonly static byte[] sizeGeneID = new byte[3]{ 0, 0, 1 };
        public readonly static byte[] weightGeneID = new byte[3] { 0, 1, 0 };
        public readonly static byte[] powerGeneID = new byte[3] { 0, 1, 1 };
        public readonly static byte[] lifeSpanGeneID = new byte[3] { 1, 0, 0 };
        public readonly static byte[] armsGeneID = new byte[3] { 1, 1, 0 };
        public readonly static byte[] legsGeneID = new byte[3] { 1, 1, 1 };
    }
    
    static class GenomeEncoder
    {
        private const int m_geneLength = 9;
        private const int m_geneIdentifier = 3;
        private const int m_geneValue = 6;

        public static EncodedGenome Encode(ref Genome geno)
        {
            byte[] geneData = geno.Genes;                        
            EncodedGenome eGeno = null;

            for (int i = 0; i < geneData.Length; i = i + m_geneLength)
            {
                byte[] geneID = new byte[m_geneIdentifier];
                for (int j = 0; j < geneID.Length; j++)
                {

                    geneID[j] = geneData[i + j];
                }

                byte[] geneValue = new byte[m_geneValue];

                for (int j = 0; j < geneValue.Length; j++)
                {
                    geneValue[j] = geneData[i + m_geneIdentifier + j];
                }

                float size = 0;
                float power = 0;
                float weight = 0;
                int nrArms = 0;
                int nrLegs = 0;
                int lifespan = 0;
                float[] color = new float[3];

                if (geneID.SequenceEqual(GeneData.armsGeneID))
                {

                }
                else if (geneID.SequenceEqual(GeneData.colorGeneID))
                {

                }
                else if (geneID.SequenceEqual(GeneData.legsGeneID))
                {

                }
                else if (geneID.SequenceEqual(GeneData.lifeSpanGeneID))
                {

                }
                else if (geneID.SequenceEqual(GeneData.powerGeneID))
                {

                }
                else if (geneID.SequenceEqual(GeneData.sizeGeneID))
                {

                }
                else if (geneID.SequenceEqual(GeneData.weightGeneID))
                {

                }
            }

            return eGeno;
        }

    }
}


