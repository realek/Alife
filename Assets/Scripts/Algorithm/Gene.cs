using UnityEngine;
using System.Collections;

namespace GA
{
    public enum GeneType
    {
        Legs,
        Arms,
        Eyes,
        Size,
        SkinColor,
        ImuneSystem
    }

    public class Gene
    {
        private GeneType m_GeneType;
        private object m_value;

        public GeneType GeneType
        {
            get
            {
                return m_GeneType;
            }

            set
            {
                m_GeneType = value;
            }
        }

        public void SetValue(object value)
        {
            m_value = value;
        }

        public object GetValue()
        {
            return m_value;
        }

    }
}


