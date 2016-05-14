using UnityEngine;
using System.Collections;

namespace GA
{

    static class GeneData
    {
        public readonly static byte[] colorGeneID = new byte[3] { 0, 0, 0 };
        public readonly static byte[] sizeGeneID = new byte[3] { 0, 0, 1 };
        public readonly static byte[] weightGeneID = new byte[3] { 0, 1, 0 };
        public readonly static byte[] powerGeneID = new byte[3] { 0, 1, 1 };
        public readonly static byte[] lifeSpanGeneID = new byte[3] { 1, 0, 0 };
        public readonly static byte[] armsGeneID = new byte[3] { 1, 1, 0 };
        public readonly static byte[] legsGeneID = new byte[3] { 1, 1, 1 };
        public const int geneLength = 9;
        public const int geneIdentifierLength = 3;
        public const int geneValueLength = 6;
        public const int NrMandatoryGenes = 7;
        public const int TotalGenesNr = 7;
        public const float AgeStep = 0.0001f;
    }

}