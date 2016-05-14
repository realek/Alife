using UnityEngine;
using System.Collections;

public class MonsterTester : MonoBehaviour {


    public float Size;
    public int NumberOfLegs;
    public int NumberOfArms;
    public float Speed;
    public float Power;

    public Color CreatureColor;

	// Use this for initialization
	void Start () {

        GetComponent<MonsterRenderer>().CreateMonster(Size, NumberOfLegs, NumberOfArms, CreatureColor, Speed, Power,true,true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
