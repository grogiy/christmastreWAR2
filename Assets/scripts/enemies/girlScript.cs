using Unity.VisualScripting;
using UnityEngine;

public class girlScript : MonoBehaviour
{
	[SerializeField] Renderer boundingArea;
	Transform plr;
	Vector3 target;
	public bool haveLooked;
	public GameObject girl;
	public GameObject newGirl;
	public GameObject newGirlGFX;
	Camera cam;
	public float moveSpeed;
	public float distance;
	public Transform spawn;
	void Start()
	{
		plr = GameObject.Find("playerOrientation").transform;
		cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		
	}
	void FixedUpdate()

	{
		girl.transform.position = Vector3.MoveTowards(girl.transform.position, target, moveSpeed * Time.deltaTime);
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
		spawn.position = plr.position + -transform.forward * distance;
		
		if(GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))

		{
			haveLooked = true;
			target = plr.position;
		}else

		{
			target = girl.transform.position;
		}if(!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds) && haveLooked)
		{
			Destroy(girl);
			GameObject clone = Instantiate(newGirl, spawn.position, newGirl.transform.rotation);
			 girlScript gs = clone.GetComponentInChildren<girlScript>();
			if (gs != null) gs.enabled = true;
			likeNextBots lnb = clone.GetComponentInChildren<likeNextBots>();
			if (lnb != null) lnb.enabled = true;
			clone.name = "girl";
			
			girl = clone;
			haveLooked = false;
		}
	}
}
