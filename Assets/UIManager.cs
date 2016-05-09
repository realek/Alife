using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

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
    }
}
