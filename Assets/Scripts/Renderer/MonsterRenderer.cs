using UnityEngine;
using System.Collections;

public class MonsterRenderer : MonoBehaviour {

    GameObject p_Body;
    GameObject p_Leg,p_Head;

    Transform currentBody;

	// Use this for initialization
	void Start () {
        p_Body = Resources.Load("BodyParts/Body") as GameObject;
        p_Leg = Resources.Load("BodyParts/Leg") as GameObject;
        p_Head = Resources.Load("BodyParts/Head") as GameObject;
        CreateMonster(3f,10);
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

    public void CreateMonster(float size,int nlegs) {
        GameObject goBody = Instantiate(p_Body);
        currentBody = goBody.transform;

        

        BodySocketManager bsm = goBody.GetComponent<BodySocketManager>();        
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

        /*
        for (int i = 0; i < nlegs; i++)
        {
            float percent = (i+1) / (float) (1 + nlegs);
            //percent = 0.5f;
            Debug.Log(percent);
            Transform t = new GameObject().transform;
            t.position = Vector3.Lerp(bsm.LegSocketStart.transform.position,bsm.LegSocketEnd.transform.position,percent);
            t.rotation = bsm.LegSocketEnd.transform.rotation;
            InstantiatePart(p_Leg, t);
        }


        //legs
        for (int i = 0; i < nlegs; i++)
        {
            float percent = (i + 1) / (float)(1 + nlegs);
            //percent = 0.5f;
            Debug.Log(percent);
            Transform t = new GameObject().transform;
            t.position = Vector3.Lerp(bsm.RightSocketStart.transform.position, bsm.RightSocketEnd.transform.position, percent);
            t.rotation = bsm.RightSocketEnd.transform.rotation;
            InstantiatePart(p_Leg, t);
        }
         * 
         * */


        //handle size
        currentBody.transform.localScale = Vector3.one * size;


    }
}
