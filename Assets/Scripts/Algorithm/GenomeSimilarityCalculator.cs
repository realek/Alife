using UnityEngine;
using System.Linq;



namespace GA
{
    /// <summary>
    /// Used to compare genomes
    /// </summary>
    static class GenomeSimilarityCalculator
    {
        private static int similarityRate=95;
        private static int maxSimilarity = 100;
        private static int minSimilarity = 10;
        /// <summary>
        /// Set the similarity rate, goes from 10 to 100
        /// </summary>
        /// <param name="rate"></param>
        public static void SetSimilarityRate(int rate)
        {
            similarityRate = Mathf.Clamp(rate, minSimilarity, maxSimilarity);
        }

        /// <summary>
        /// Normalization function
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static int NormalizeToSimilarityRate(int x)
        {
            int nnMax = GeneData.geneLength * GeneData.TotalGenesNr;
            int nnMin = GeneData.NrMandatoryGenes * GeneData.geneIdentifierLength;
            return minSimilarity + (((x - nnMin) * (maxSimilarity - minSimilarity)) / (nnMax - nnMin));
        }
        /// <summary>
        /// Use to check if two genomes are similar enough to reproduce
        /// </summary>
        /// <param name="genoA"></param>
        /// <param name="genoB"></param>
        public static bool CanReproduce(Genome genoA, Genome genoB)
        {

            int similarityFactor = 0;
            for (int i = 0; i < genoA.Genes.Length; i = i + GeneData.geneLength)
            {
                //current gene id
                byte[] geneAID = new byte[GeneData.geneIdentifierLength];
                byte[] geneBID = new byte[GeneData.geneIdentifierLength];
                for (int j = 0; j < geneAID.Length; j++)
                {

                    geneAID[j] = genoA.Genes[i + j];
                    geneBID[j] = genoB.Genes[i + j];
                }

                //current gene value
                byte[] geneAValue = new byte[GeneData.geneValueLength];
                byte[] geneBValue = new byte[GeneData.geneValueLength];
                for (int j = 0; j < GeneData.geneValueLength; j++)
                {
                    geneAValue[j] = genoA.Genes[i + (geneAID.Length) + j];
                    geneBValue[j] = genoB.Genes[i + (geneBID.Length) + j];
                }

                ///check if gene is the same
                if (geneAID.SequenceEqual(geneBID))
                {
                    //add gene id value to factor
                    similarityFactor += GeneData.geneIdentifierLength;
                    for (int j = 0; j < geneAValue.Length; j++)
                    {
                        //compare geneValue data for each matching bit increment
                        if (geneAValue[j] == geneBValue[j])
                        {
                            similarityFactor++;
                        }
                    }
                }

            }
            //normalize similarity factor
            similarityFactor = NormalizeToSimilarityRate(similarityFactor);
            if (similarityFactor >= similarityRate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

