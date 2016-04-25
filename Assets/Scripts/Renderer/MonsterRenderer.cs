using UnityEngine;
using System.Collections;

public class MonsterRenderer : MonoBehaviour {

    GameObject p_Body;
    GameObject p_Leg,p_Head;

    Transform currentBody;

    public float Size;
    public int NumberOfLegs;
    public int NumberOfArms;

    public Color CreatureColor;

	// Use this for initialization
	void Start () {
        p_Body = Resources.Load("BodyParts/Body") as GameObject;
        p_Leg = Resources.Load("BodyParts/Leg") as GameObject;
        p_Head = Resources.Load("BodyParts/Head") as GameObject;
        CreateMonster(Size,NumberOfLegs,NumberOfArms,CreatureColor);
	}

    void InstantiatePart(GameObject part, Transform where)
    {
        GameObject go = Instantiate(part, where.position, where.rotation) as GameObject;
        go.transform.parent = currentBody;
        Debug.Log(part.name + " instantiated at " + where.position.ToString());
    }

    void PlaceLegs(GameObject socketStart, GameObject socketEnd, int numberOfLegs) {

        for (int i = 0; i < numberOfLegs; i++)
        {
            float percent = (i + 1) / (float)(1 + numberOfLegs);
            //percent = 0.5f;
            Debug.Log(percent);
            Transform t = new GameObject().transform;
            t.position = Vector3.Lerp(socketStart.transform.position, socketEnd.transform.position, percent);
            t.rotation = socketEnd.transform.rotation;
            InstantiatePart(p_Leg, t);
            Destroy(t.gameObject);
        }
        
    }

    public void CreateMonster(float size,int nlegs,int narms,Color color) {
        GameObject goBody = Instantiate(p_Body);
        currentBody = goBody.transform;

        

        BodySocketManager bsm = goBody.GetComponent<BodySocketManager>();   
        
        //resize body considering nlegs and arms
        GameObject bodymodel = bsm.BodyModel;
        float length;
        length = 1+nlegs / 2;
        
        float height = 1 + narms / 2;

        bodymodel.transform.localScale = new Vector3(bodymodel.transform.localScale.x * length, bodymodel.transform.localScale.y*height, bodymodel.transform.localScale.x);

        


        // head
        InstantiatePart(p_Head, bsm.HeadSocket.transform);

        //legs

        int right_legs, left_legs;
        if (nlegs % 2 == 0) //even number of legs
        {
            right_legs = nlegs / 2;
            left_legs = nlegs / 2;
        }
        else //uneven number of legs
        {
            right_legs = (nlegs-1)/2 + 1;
            left_legs = (nlegs - 1) / 2;
        }

        PlaceLegs(bsm.LeftLegSocketStart, bsm.LeftLegSocketEnd, left_legs);
        PlaceLegs(bsm.RightLegSocketStart, bsm.RightLegSocketEnd, right_legs);

        /// arms

        int right_arms, left_arms;
        if (narms % 2 == 0) //even number of legs
        {
            right_arms = narms / 2;
            left_arms = narms / 2;
        }
        else //uneven number of legs
        {
            right_arms = (narms - 1) / 2 + 1;
            left_arms = (narms - 1) / 2;
        }

        PlaceLegs(bsm.RightArmSocketEnd, bsm.RightArmSocketStart, right_arms);
        PlaceLegs(bsm.LeftArmSocketEnd, bsm.LeftArmSocketStart, left_arms);

       
        //handle size
        currentBody.transform.localScale = Vector3.one * size;

        //handle color
        HandleColors(color);
        

    }

    void ColorPartOfCreature(string tagname, Color color) {

        GameObject[] list = GameObject.FindGameObjectsWithTag(tagname);
        for (int i = 0; i < list.Length; i++)
        {
            list[i].GetComponent<Renderer>().material.color = color;
        }
       
    
    }

    void HandleColors(Color color) {

        //head
        ColorPartOfCreature("Head", color);

        //legs
        ColorPartOfCreature("Leg", Color.Lerp( color, Color.black,0.5f));


        //body
        ColorPartOfCreature("Body", Color.Lerp(color, Color.white, 0.1f));

    
    }
}
