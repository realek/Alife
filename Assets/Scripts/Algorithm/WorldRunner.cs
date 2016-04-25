using UnityEngine;
using System.Collections;
using GA;




public class WorldRunner : MonoBehaviour {


    //BASIC DATA
    System.Random rand;
    int genomeSize = 8; // number of genes in genome;
    int populationSize = 100;
    WaitForEndOfFrame m_w8;
    //
    bool m_running;
    Population population;
	// Use this for initialization
	void Start () {

        m_running = true;
        m_w8 = new WaitForEndOfFrame();
        rand = new System.Random();
        int[] currentGoal = new int[genomeSize];
        //create Random goal 
        for (int i = 0; i < currentGoal.Length; i++)
        {
            //0,1 cause bits
            currentGoal[i] = rand.Next(2);
        }

        //load goal
        Fitness.LoadGoal(currentGoal);

        population = new Population(populationSize, genomeSize);
        population.GenerateInitalPopulation();

        StartCoroutine(WorldRoutine());
    }
	
    IEnumerator WorldRoutine()
    {
        while (m_running)
        {
            population.EvaluatePopulation();
            Debug.Log("Current gen: " + GeneticAlgorithm.Generation + " most fit is: "+population.BestGenome.Fitness);
            GeneticAlgorithm.EvolvePopulation(ref population);
            yield return m_w8;


        }

    }
}
