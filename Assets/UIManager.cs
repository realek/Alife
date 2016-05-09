using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    [SerializeField]
    int maxCreaturesOnScreen;

    MonsterRenderer mr;
    Camera cam;

	// Use this for initialization
	void Start () {
        mr = GameObject.FindObjectOfType<MonsterRenderer>();
        cam = Camera.main;
	}

    public void ClickOnRender() {

        GameObject lastMonster = mr.RenderBest();
        cam.GetComponent<OrbitCamera>()._target = lastMonster.transform;

        //handle max creatures on screen
        HandleMaxCreatures();
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
