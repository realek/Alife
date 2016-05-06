using UnityEngine;
using System.Collections;

namespace GA
{
    public class EncodedGenome
    {

        private int m_nrLegs = 0;
        private int m_nrArms = 0;
        private float m_size = 0;
        private float m_power = 0;
        private float m_weight = 0;
        private int m_lifeSpan = 0;
        private Color m_color;

        public EncodedGenome(Color color,int nrUpperLimbs,int nrLowerLimbs, float size, float power,float weight,int lifespan)
        {
            m_nrArms = nrUpperLimbs;
            m_nrLegs = nrLowerLimbs;
            m_size = size;
            m_power = power;
            m_weight = weight;
            m_color = color;
            m_lifeSpan = lifespan;
        }

        public int NumberOfLegs
        {
            get
            {
                return m_nrLegs;
            }

        }

        public int NumberOfArms
        {
            get
            {
                return m_nrArms;
            }
        }

        public float Size
        {
            get
            {
                return m_size;
            }
        }

        public bool CanSwim
        {
            get
            {
                return ((m_size / m_nrLegs) * m_power) >= m_weight;
            }
        }

        public bool CanClimb
        {
            get
            {
                if (m_nrArms == 0)
                {
                    return false;
                }
                else
                {
                    return (((m_nrLegs + m_nrArms) * m_power) / m_size) > m_weight;
                }
            }
        }

        public float Speed
        {
            get
            {
                return m_weight / (m_nrLegs * m_power);
            }
        }

        public Color Color
        {
            get
            {
                return m_color;
            }
        }

        public int LifeSpan
        {
            get
            {
                return m_lifeSpan;
            }
        }
    }
}

