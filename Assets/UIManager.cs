using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    int maxCreaturesOnScreen;

    [SerializeField]
    Text GenerationCount,CreatureStats;

    MonsterRenderer mr;
    Camera cam;
    WorldRunner wr;

	// Use this for initialization
	void Start () {
        mr = GameObject.FindObjectOfType<MonsterRenderer>();
        cam = Camera.main;
        wr = GameObject.FindObjectOfType<WorldRunner>();
	}

    IEnumerator ShakeCam()
    {
        float t = 0.1f;
        Vector3 origpos = transform.position;
        cam.GetComponent<OrbitCamera>().enabled = false;
        do
        {
            t -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
            
            Debug.Log(transform.position);
            cam.fieldOfView += t*5f;
        } while (t > 0f);

        cam.fieldOfView = 60f;
        cam.GetComponent<OrbitCamera>().enabled = true;

    }

    public void CauseFlood()
    {
        StartCoroutine(ShakeCam());
        wr.Flood = true;
    }

    public void CauseMeteorite()
    {
        StartCoroutine(ShakeCam());
        wr.Meteor = true;
    }

    public void ClickOnRender() {

        GameObject lastMonster = mr.RenderBest();
        cam.GetComponent<OrbitCamera>()._target = lastMonster.transform;

        //handle max creatures on screen
        HandleMaxCreatures();

        //update creature stats
        GA.EncodedGenome t = wr.CPopulation.BestGenome.encoded;
        string s = "";
        s += "Size: " + t.Size.ToString();
        s += "\n";
        s += "Legs: " + t.NumberOfLegs.ToString();
        s += "\n";
        s += "Arms: " + t.NumberOfArms.ToString();
        s += "\n";
        s += "Speed: " + t.Speed.ToString();
        s += "\n";
        s += "Power: " + t.Power.ToString();
        s += "\n";

        if (t.CanClimb)
        {
            s += "Can Climb";
            s += "\n";
        }

        if (t.CanSwim)
        {
            s += "Can Swim";
            s += "\n";
        }

        CreatureStats.text = s;

        
    }

    public void Update()
    {

        GenerationCount.text = "Generation: "+GA.GeneticAlgorithm.Generation.ToString();
    }

    void HandleMaxCreatures()
    {
        BodySocketManager[] creatures = GameObject.FindObjectsOfType<BodySocketManager>();
        if (creatures.Length <= maxCreaturesOnScreen)
            return;
        else
        {

            Destroy(creatures[creatures.Length-1].gameObject);
        }
    }
}
