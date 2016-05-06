using UnityEngine;
using System.Collections;

public class LegAnimator : MonoBehaviour {

    public float speed;
    public AnimationCurve aCurve;

    Vector3 origRot;

	// Use this for initialization
	void Start () {
        origRot = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
	    
        transform.Translate( new Vector3(0f,1f,0f)*Mathf.Sin((Time.time+transform.position.x)*5f)*0.01f);
        //transform.Rotate(new Vector3(0f, 1f, 0f) * Mathf.Sin((Time.time + transform.position.x) * 3f / speed),Space.Self);
        //transform.Rotate(new Vector3(0f, 1f, 0f) * aCurve.Evaluate(Time.time * speed));
        transform.rotation = Quaternion.Euler(origRot + new Vector3(0f, 35f, 0f) * aCurve.Evaluate(Time.time * speed));
	}
}
