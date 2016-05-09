using UnityEngine;
using System.Collections;
using GA;




public class WorldRunner : MonoBehaviour {


    //BASIC DATA
    private const int genomeSize = 63; // number of genes in genome;
    private const int populationSize = 100;
    private const int similarityRate = 75;
    WaitForEndOfFrame m_w8;
    //
    bool m_running;
    [SerializeField]
    Population population;
	// Use this for initialization
	void Awake () {
    m_running = true;
        m_w8 = new WaitForEndOfFrame();
        GenomeSimilarityCalculator.SetSimilarityRate(similarityRate);
        population = new Population(populationSize, genomeSize);
        population.GenerateInitalPopulation();
        population.EvaluatePopulation();
        StartCoroutine(WorldRoutine());
    }
	
    IEnumerator WorldRoutine()
    {
        while (m_running)
        {

           // Debug.Log("Current gen: " + GeneticAlgorithm.Generation + " most fit is: " + population.BestGenome.Fitness);
            population = GeneticAlgorithm.EvolvePopulation(population);
            population.EvaluatePopulation();
            yield return m_w8;


        }

    }

    public Population CPopulation 
    {
        get
        {
            return population;
        }
    }
}
