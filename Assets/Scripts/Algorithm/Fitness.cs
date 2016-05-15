namespace GA
{
    enum MassExtinction
    {
        Flood,
        Meteor
    }
    static class Fitness
    {

        public static float FoodFitness(Genome Geno)
        {
            if (Geno.encoded.CanClimb)
            {
                return Geno.encoded.Speed + 1;
            }
            else
            {
                return Geno.encoded.Speed;
            }

        }

        public static float Flood(Genome Geno)
        {
            if (Geno.encoded.CanSwim || Geno.encoded.CanClimb || Geno.encoded.Size > 5)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static float MeteorStrike(Genome Geno)
        {
            if (Geno.encoded.Size > 1)
            {
                return 0;
            }
            else
            {
                return 1;
            } 
        }

    }
    
}
