using UnityEngine;

public class classicGun : MonoBehaviour
{
	float cd;
	public float maxCd;
	public float bulletsRemain;
	float reloadCD;
	public float reloadMaxCd;
	public KeyCode shootKey;
	public KeyCode reloadKey;
	public float spread;
	public Camera cam;
	public float range;
	public LayerMask enemyLayer;
	public GameObject point;
	public LineRenderer lr;
	public Transform shootPos;
		
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		lr.SetPosition(1, shootPos.position);
		lr.startWidth = 0.25f;
		lr.endWidth = 0.25f;
		lr.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(shootKey))

		{
			if(cd <= 0)

			{
				Shoot();
			}
		}
		if(lr.startWidth <= 0.01f)

		{
			lr.enabled = false;	
		}
		cd -= Time.deltaTime;
		lr.endWidth = Mathf.MoveTowards(lr.endWidth, 0, Time.deltaTime * 3);
		lr.startWidth = Mathf.MoveTowards(lr.startWidth, 0, Time.deltaTime * 3);
	}
	void Shoot()
	{
		RaycastHit hit;
		float x = Random.Range(-spread, spread);
		float y = Random.Range(-spread, spread);
		Vector3 direction = cam.transform.forward + new Vector3(x, y, 0);
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		cd = maxCd;
		if(Physics.Raycast(cam.transform.position, direction, out hit, range))

		{
			if(hit.collider.CompareTag("enemyHead"))

			{
				enemyHealth eHp = hit.transform.GetComponent<enemyHealth>();
				eHp.TakeDamage(3);
			}
			if(hit.collider.CompareTag("enemyTorso"))

			{
				enemyHealth eHp = hit.transform.GetComponent<enemyHealth>();
				eHp.TakeDamage(1);
			}
			//Instantiate(point, hit.point, Quaternion.LookRotation(hit.normal));
			Debug.Log("shooting");
			lr.enabled = true;
			lr.SetPosition(0, shootPos.position);
			lr.SetPosition(1, hit.point);
			lr.startWidth = 0.25f;
			lr.endWidth = 0.25f;
		}else

		{
			lr.enabled = true;
			lr.SetPosition(0, shootPos.position);
			lr.SetPosition(1, cam.transform.position + cam.transform.forward * range);
			lr.startWidth = 0.25f;
			lr.endWidth = 0.25f;
		}
	}
	void LateUpdate()
	{
		
	}
}
