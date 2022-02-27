using UnityEngine;
using System.Collections.Generic;

namespace Moku
{
	public class RopeScript : MonoBehaviour
	{ // can think of this script as the "Bullet"

		public Vector2 Destiny;

		#region vars
		[SerializeField] private float hookShootSpeed = 1;
		[SerializeField] private float distanceBetweenTheHingeJoints = 2;
		#endregion

		#region references
		[SerializeField] private GameObject player;
		[SerializeField] private GameObject extendablePrefab;
		[SerializeField] private GameObject lastNode;
		[SerializeField] private List<GameObject> HookObjs = new List<GameObject>();
		[SerializeField] private LineRenderer lineRenderer;
		[SerializeField] private GameObject mouthPivot;

		#endregion

		#region localVars
		private int vertexCount = 2;
		private bool done = false;
		public bool HitSignal = false;

		#endregion

		[SerializeField] private float hookPushForce = 1000f;
		[SerializeField] private float hookPullForce = 1500f;


		void Start()
		{
			player = GameObject.FindGameObjectWithTag("Player");
			lastNode = transform.gameObject;
			HookObjs.Add(transform.gameObject);
			mouthPivot = GameObject.FindGameObjectWithTag("MouthPivot");
		}

		void Update()
		{

			transform.position = Vector2.MoveTowards(transform.position, Destiny, hookShootSpeed);
			if ((Vector2)transform.position != Destiny)
			{
				if (Vector2.Distance(mouthPivot.transform.position, lastNode.transform.position) > distanceBetweenTheHingeJoints)
				{
					CreateHookGameObject();
				}
			}
			else if (!done)
			{
				done = true;
				while (Vector2.Distance(mouthPivot.transform.position, lastNode.transform.position) > distanceBetweenTheHingeJoints)
				{
					CreateHookGameObject();
				}
				lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
			}

			//    if (Input.GetKey(KeyCode.F))
			//    {
			//        foreach (var item in HookObjs)
			//        {
			//var joint = item.GetComponent<HingeJoint2D>();
			//            if (joint != null)
			//            {
			//	item.GetComponent<Rigidbody2D>()?.AddTorque(150.0f);
			//	Debug.Log("*");
			//	break;
			//            }
			//        }
			//    }

			RenderLine();
		}

		void RenderLine()
		{
			lineRenderer.positionCount = vertexCount;
			int i;
			for (i = 0; i < HookObjs.Count; i++)
			{
				lineRenderer.SetPosition(i, HookObjs[i].transform.position);
			}
			lineRenderer.SetPosition(i, mouthPivot.transform.position);
		}

		void CreateHookGameObject()
		{
			Vector2 pos2Create = mouthPivot.transform.position - lastNode.transform.position;
			pos2Create.Normalize();
			pos2Create *= distanceBetweenTheHingeJoints;
			pos2Create += (Vector2)lastNode.transform.position;

			GameObject go = (GameObject)Instantiate(extendablePrefab, pos2Create, Quaternion.identity);

			go.tag = "Grappling Hook";
			go.transform.SetParent(transform);
			lastNode.GetComponent<HingeJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();
			lastNode = go;

			HookObjs.Add(lastNode);
			vertexCount++;
		}

		//  private void DestroySelf()
		//  {
		//Destroy(gameObject);
		//  }

		private void OnTriggerEnter2D(Collider2D hitInfo)
		{
			//Instantiate(sparksEffect, transform.position, transform.rotation);
			Rigidbody2D gameObj = hitInfo.gameObject.GetComponent<Rigidbody2D>();
			if (hitInfo.gameObject.CompareTag("Pullable") && gameObj.mass < 10)
			{
				Invoke("ResetRope", 0.05f);
				GameObject hookedItem = hitInfo.gameObject;
				Vector2 directionOfObj = (Vector2)transform.position - (Vector2)hookedItem.transform.position;
				hookedItem.GetComponent<Rigidbody2D>().AddForce(directionOfObj.normalized * hookPullForce);
			}

			if (hitInfo.gameObject.CompareTag("Pushable") && gameObj.mass < 10)
			{
				Invoke("ResetRope", 0.05f);
				GameObject hookedItem = hitInfo.gameObject;
				Vector2 directionOfObj = (Vector2)transform.position - (Vector2)hookedItem.transform.position;
				hookedItem.GetComponent<Rigidbody2D>().AddForce(-directionOfObj.normalized * hookPushForce);
			}
		}
		void ResetRope()
		{
			Destroy(player.GetComponent<Hook>().curHook);
			player.GetComponent<Hook>().ropeActive = false;
		}
	}

}



