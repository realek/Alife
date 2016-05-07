using System;
using System.Linq;
using UnityEngine;

namespace GA
{
    
    static class GenomeEncoder
    {

        public static EncodedGenome Encode(Genome geno)
        {
            byte[] geneData = geno.Genes;                        
            EncodedGenome eGeno = null;
            float size = 0;
            float power = 0;
            float weight = 0;
            int nrArms = 0;
            int nrLegs = 0;
            int lifespan = 0;
            Color color = new Color();
            bool hasArmsGene = false;
            bool hasLegsGene = false;
            bool hasSizeGene = false;
            bool hasPowerGene = false;
            bool hasWeightGene = false;
            bool hasColorGene = false;
            bool hasLifespanGene = false;

            for (int i = 0; i < geneData.Length; i = i + GeneData.m_geneLength)
            {
                byte[] geneID = new byte[GeneData.m_geneIdentifier];
                for (int j = 0; j < geneID.Length; j++)
                {

                    geneID[j] = geneData[i + j];
                }

                byte[] geneValue = new byte[GeneData.m_geneValue];

                for (int j = 0; j < GeneData.m_geneValue; j++)
                {
                    geneValue[j] = geneData[i + (geneID.Length) + j];
                }


                float[] colorVals = new float[3];

                if (geneID.SequenceEqual(GeneData.armsGeneID) && !hasArmsGene)
                {

                    hasArmsGene = true;
                    for (int j = 0; j < GeneData.m_geneValue; j++)
                    {
                        nrArms += geneValue[j];
                    }

                }
                else if (geneID.SequenceEqual(GeneData.colorGeneID) && !hasColorGene)
                {
                    hasColorGene = true;
                    colorVals[0] = geneValue[geneValue.Length-1] + geneValue[0];
                    colorVals[1] = geneValue[geneValue.Length/2] + geneValue[geneValue.Length/3];
                    colorVals[2] = geneValue[1] + geneValue[geneValue.Length-2];
                    Vector3 colorVec = new Vector3(colorVals[0], colorVals[1], colorVals[2]);
                    colorVec.Normalize();
                    color.r = colorVec.x;
                    color.g = colorVec.y;
                    color.b = colorVec.z;
                }
                else if (geneID.SequenceEqual(GeneData.legsGeneID) && !hasLegsGene)
                {
                    hasLegsGene = true;
                    for (int j = 0; j < GeneData.m_geneValue; j++)
                    {
                        nrLegs += geneValue[j];
                    }
                }
                else if (geneID.SequenceEqual(GeneData.lifeSpanGeneID) && !hasLifespanGene)
                {
                    hasLifespanGene = true;
                    for (int j = 0; j < GeneData.m_geneValue; j++)
                    {
                        lifespan += geneValue[j];
                    }
                }
                else if (geneID.SequenceEqual(GeneData.powerGeneID) && !hasPowerGene)
                {
                    hasPowerGene = true;
                    for (int j = 0; j < GeneData.m_geneValue; j++)
                    {
                        power += geneValue[j];
                    }
                }
                else if (geneID.SequenceEqual(GeneData.sizeGeneID) && !hasSizeGene)
                {
                    hasSizeGene = true;
                    for (int j = 0; j < GeneData.m_geneValue; j++)
                    {
                        size += geneValue[j];
                    }
                }
                else if (geneID.SequenceEqual(GeneData.weightGeneID) && !hasWeightGene)
                {
                    hasWeightGene = true;
                    for (int j = 0; j < GeneData.m_geneValue; j++)
                    {
                        weight += geneValue[j];
                    }
                }
            }

            if (hasSizeGene && hasWeightGene && hasLifespanGene && hasPowerGene)
            {
                eGeno = new EncodedGenome(color, nrArms, nrLegs, size, power, weight, lifespan);
                return eGeno;
            }
            else
            {
                return null;
            }

            
        }

    }
}


