using UnityEngine;
using System.Collections;

public class MonsterRenderer : MonoBehaviour {

    GameObject p_Body;
    GameObject p_Leg,p_LegClimb,p_Head,p_Arm,p_HeadWater;

    Transform currentBody;

    WorldRunner wr;

	// Use this for initialization
	void Start () {
        p_Body = Resources.Load("BodyParts/Body") as GameObject;
        p_Leg = Resources.Load("BodyParts/Leg") as GameObject;
        p_LegClimb = Resources.Load("BodyParts/LegClimb") as GameObject;
        p_Arm = Resources.Load("BodyParts/Arm") as GameObject;
        p_Head = Resources.Load("BodyParts/Head") as GameObject;
        p_HeadWater = Resources.Load("BodyParts/HeadSwim") as GameObject;
        //CreateMonster(Size,NumberOfLegs,NumberOfArms,CreatureColor,Speed,Power);
        wr = GameObject.FindObjectOfType<WorldRunner>();
	}

    public GameObject RenderBest()
    {

        GA.EncodedGenome temp = wr.CPopulation.BestGenome.encoded;

        GameObject lastMonster = CreateMonster(temp.Size, temp.NumberOfLegs, temp.NumberOfArms, temp.Color, temp.Speed, temp.Power, temp.CanSwim,temp.CanClimb);
        return lastMonster;
    }



    void InstantiatePart(GameObject part, Transform where)
    {
        Vector3 diff = Vector3.up * Random.RandomRange(0f, 1f) *0f;

        GameObject go = Instantiate(part, where.position+diff, where.rotation) as GameObject;
        go.transform.parent = currentBody;
        
        go.transform.localScale *= 1f;
    }

    void PlaceLimbs(GameObject socketStart, GameObject socketEnd, int numberOfLegs, GameObject limb) {

        for (int i = 0; i < numberOfLegs; i++)
        {
            float percent = (i + 1) / (float)(1 + numberOfLegs);
            //percent = 0.5f;
            
            Transform t = new GameObject().transform;
            t.position = Vector3.Lerp(socketStart.transform.position, socketEnd.transform.position, percent);
            t.rotation = socketEnd.transform.rotation;
            InstantiatePart(limb, t);
            Destroy(t.gameObject);
        }
        
    }

    public GameObject CreateMonster(float size,int nlegs,int narms,Color color,float speed,float power, bool canSwim, bool canClimb) {
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

        if(canSwim)
        {
            Debug.Log("Can swim");
            InstantiatePart(p_HeadWater, bsm.HeadSocket.transform);
        }
        else
        {
            InstantiatePart(p_Head, bsm.HeadSocket.transform);
        }
        

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

        if (!canClimb)
        {
            PlaceLimbs(bsm.LeftLegSocketStart, bsm.LeftLegSocketEnd, left_legs, p_Leg);
            PlaceLimbs(bsm.RightLegSocketStart, bsm.RightLegSocketEnd, right_legs, p_Leg);
        }
        else
        {
            PlaceLimbs(bsm.LeftLegSocketStart, bsm.LeftLegSocketEnd, left_legs, p_LegClimb);
            PlaceLimbs(bsm.RightLegSocketStart, bsm.RightLegSocketEnd, right_legs, p_LegClimb);
        }
        

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

        PlaceLimbs(bsm.RightArmSocketEnd, bsm.RightArmSocketStart, right_arms,p_Arm);
        PlaceLimbs(bsm.LeftArmSocketEnd, bsm.LeftArmSocketStart, left_arms,p_Arm);

       
        //handle size
        currentBody.transform.localScale = Vector3.one * size;

        //handle color
        HandleColors(color);

        //handle position

        currentBody.position = new Vector3(0f, 20f, 0f);

        Rigidbody rb = goBody.AddComponent<Rigidbody>();
        rb.freezeRotation = true;

        //apply animation to legs and power
        LegAnimator[] legs = GameObject.FindObjectsOfType<LegAnimator>();
        for (int i = 0; i < legs.Length; i++)
        {
            if(!legs[i].alreadyUsed)
            {
                legs[i].speed = speed/3f;
                legs[i].transform.localScale *= power;
                legs[i].alreadyUsed = true;

            }
        }

        //rotate object
        float angle = Random.Range(0f, 360f);
        goBody.transform.Rotate(0f, angle, 0f);
            

        return goBody;
    }

    void ColorPartOfCreature(string tagname, Color color) {

        GameObject[] list = GameObject.FindGameObjectsWithTag(tagname);
        for (int i = 0; i < list.Length; i++)
        {
            list[i].GetComponent<Renderer>().material.color = color;
            list[i].gameObject.tag = "Untagged";
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
