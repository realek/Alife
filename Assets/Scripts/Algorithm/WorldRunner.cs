using UnityEngine;
using System.Collections;
using GA;




public class WorldRunner : MonoBehaviour {


    //BASIC DATA
    System.Random rand;
    int genomeSize = 63; // number of genes in genome;
    int populationSize = 100;
    WaitForEndOfFrame m_w8;
    //
    bool m_running;
    [SerializeField]
    Population population;
	// Use this for initialization
	void Start () {

        m_running = true;
        m_w8 = new WaitForEndOfFrame();
        rand = new System.Random();
        int[] currentGoal = new int[genomeSize];

        population = new Population(populationSize, genomeSize);
        population.GenerateInitalPopulation();

        StartCoroutine(WorldRoutine());
    }
	
    IEnumerator WorldRoutine()
    {
        while (m_running)
        {
            population.EvaluatePopulation();
            Debug.Log("Current gen: " + GeneticAlgorithm.Generation + " most fit is: " + population.BestGenome.Fitness);
            population = GeneticAlgorithm.EvolvePopulation(population);
            yield return m_w8;


        }

    }
}
