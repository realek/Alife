using UnityEngine;
using System.Collections;
using GA;




public class WorldRunner : MonoBehaviour {


    //BASIC DATA
    private const int genomeSize = 63; // number of genes in genome;
    private const int populationSize = 50;
    private const int similarityRate = 75;
    [SerializeField]
    private bool m_flood = false;
    [SerializeField]
    private bool m_meteor = false;
    [SerializeField]
    private float m_timeStep = 1;
    WaitForSeconds m_w8;
    //
    bool m_running;
    [SerializeField]
    Population population;
	// Use this for initialization
	void Awake () {
    m_running = true;
        m_w8 = new WaitForSeconds(Time.deltaTime*m_timeStep);
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
            population.MassExtinction(ref m_flood,ref m_meteor);
            yield return m_w8;


        }

    }

    public bool Flood
    {
        set
        {
            m_flood = value;
        }
    }

    public bool Meteor
    {
        set
        {
            m_meteor = value;
        }
    }

    public float TimeStep
    {
        get
        {
            return m_timeStep;
        }

        set
        {
            m_timeStep = value;
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
