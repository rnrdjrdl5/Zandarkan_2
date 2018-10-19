using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTablePhysics : MonoBehaviour {


    public float[] YPower;
    public float[] XZPower;
    public float[] TorquePower;

    public float Mass;

    public GameObject[] FoodTableObjects; // 

    public float CreateOffPhysicsObjectTime;
    public float PhysicsOffTime;


    private Rigidbody rb;


    IEnumerator CoroSetOffPhysics;

    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
        rb.mass = Mass;
        CoroSetOffPhysics = SetOffPhysics();
    }

    private void OnDisable()
    {
        for (int i = FoodTableObjects.Length - 1; i >= 0; i--)
        {
            FoodTableObjects[i].SetActive(false);
        }
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Action(Vector3 dirVector)
    {

        // 물체가 날라갈 방향, XZ 축
        Vector3 NotY_NormalVector3 = dirVector.normalized;
        NotY_NormalVector3.y = 0;

        int count = FoodTableObjects.Length;

        // 1. 각 물체마다 분리한다. 
        for (int i = 0; i < count; i++)
        {
            FoodTableObjects[i].transform.parent = null;

            Rigidbody addRB = FoodTableObjects[i].AddComponent<Rigidbody>();

            addRB.constraints = RigidbodyConstraints.None;
            addRB.maxAngularVelocity = TorquePower[i];
            addRB.mass = Mass;

            FoodTableObjects[i].AddComponent<BoxCollider>();

            addRB.AddForce(NotY_NormalVector3 * XZPower[i], ForceMode.Impulse);

            addRB.AddForce(Vector3.up * YPower[i], ForceMode.Impulse);

            // 이동방향으로 회전방향 구하기
            Quaternion QuatY = Quaternion.Euler(0.0f, 90.0f, 0.0f);

            // 회전시키기
            addRB.AddTorque((QuatY * NotY_NormalVector3) * TorquePower[i], ForceMode.Impulse);

            FoodTableObjects[i].layer = LayerMask.NameToLayer("NoPlayerInterEnd");
        }

        // 코루틴 작동하기
        StartCoroutine(CoroSetOffPhysics);


        
    }

    IEnumerator SetOffPhysics()
    {
        yield return new WaitForSeconds(CreateOffPhysicsObjectTime);

        int count = FoodTableObjects.Length;
        for (int i = 0; i < count; i++)
        {
            
            OffObjectPhysics offObjectPhysics = FoodTableObjects[i].AddComponent<OffObjectPhysics>();
            offObjectPhysics.OffTime = PhysicsOffTime;
            offObjectPhysics.SetPhysicsObjectType(OffObjectPhysics.EnumPhysicsObject.MAINOBJECT);
        }
    }

}
