using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Water {

	public class DynamicWater2D : MonoBehaviour {

		[System.Serializable]
		public struct Bound {
			public float Top;
			public float Right;
			public float Bottom;
			public float Left;
		}

		[Header ("Water Settings")]
		public Bound bound;
		public int Quality;

		public Material waterMaterial;
		public GameObject splash;
		public GameObject splashSpawnPoint;

		private Vector3[] vertices;

		private Mesh mesh;

		[Header ("Physics Settings")]
		public float Springconstant = 0.02f;
		public float Damping = 0.1f;
		public float Spread = 0.1f;
		public float CollisionVelocityFactor = 0.04f;

		float[] velocities;
		float[] accelerations;
		float[] leftDeltas;
		float[] rightDeltas;

		private float timer;

		private void Start () {
			InitializePhysics ();
			GenerateMesh ();
			SetBoxCollider2D ();
		}

		private void InitializePhysics () {
			velocities = new float[Quality];
			accelerations = new float[Quality];
			leftDeltas = new float[Quality];
			rightDeltas = new float[Quality];
		}

		private void GenerateMesh () {
			float range = (bound.Right - bound.Left) / (Quality - 1);
			vertices = new Vector3[Quality * 2];

			// generate vertices
			// top vertices
			for (int i = 0; i < Quality; i++) {
				vertices[i] = new Vector3 (bound.Left + (i * range), bound.Top, 0);
			}
			// bottom vertices
			for (int i = 0; i < Quality; i++) {
				vertices[i + Quality] = new Vector2 (bound.Left + (i * range), bound.Bottom);
			}

			// generate tris. the algorithm is messed up but works. lol.
			int[] template = new int[6];
			template[0] = Quality;
			template[1] = 0;
			template[2] = Quality + 1;
			template[3] = 0;
			template[4] = 1;
			template[5] = Quality + 1;

			int marker = 0;
			int[] tris = new int[((Quality - 1) * 2) * 3];
			for (int i = 0; i < tris.Length; i++) {
				tris[i] = template[marker++]++;
				if (marker >= 6) marker = 0;
			}

			// generate mesh
			MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer> ();
			if (waterMaterial) meshRenderer.sharedMaterial = waterMaterial;

			MeshFilter meshFilter = gameObject.AddComponent<MeshFilter> ();

			mesh = new Mesh ();
			mesh.vertices = vertices;
			mesh.triangles = tris;
			mesh.RecalculateNormals ();
			mesh.RecalculateBounds ();

			// set up mesh
			meshFilter.mesh = mesh;
		}

		private void SetBoxCollider2D () {
			BoxCollider2D col = gameObject.AddComponent<BoxCollider2D> ();
			col.isTrigger = true;
			col.usedByEffector = true;
		}

		private void Update () {
			// optimization. we don't want to calculate all of this on every update.
			if(timer <= 0) return;
			timer -= Time.deltaTime;

			// updating physics
			for (int i = 0; i < Quality; i++) {
				float force = Springconstant * (vertices[i].y - bound.Top) + velocities[i] * Damping;
				accelerations[i] = -force;
				vertices[i].y += velocities[i];
				velocities[i] += accelerations[i];
			}

			for (int i = 0; i < Quality; i++) {
				if (i > 0) {
					leftDeltas[i] = Spread * (vertices[i].y - vertices[i - 1].y);
					velocities[i - 1] += leftDeltas[i];
				}
				if (i < Quality - 1) {
					rightDeltas[i] = Spread * (vertices[i].y - vertices[i + 1].y);
					velocities[i + 1] += rightDeltas[i];
				}
			}

			// updating mesh
			mesh.vertices = vertices;
		}

		private void OnTriggerEnter2D(Collider2D col) {
			Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
			Splash(col, rb.velocity.y * CollisionVelocityFactor);
		}

		public void Splash (Collider2D col, float force) {
			timer = 3f;
			float radius = col.bounds.max.x - col.bounds.min.x;
			Vector2 center = new Vector3(col.bounds.center.x,bound.Top) ;
			// instantiate splash particle
			GameObject splashGO = Instantiate(splash, new Vector3(center.x, col.bounds.center.y, 0), Quaternion.Euler(0,0,60));
			splashGO.transform.SetParent(splashSpawnPoint.transform);
			Destroy(splashGO, 2f);

			// applying physics
			for (int i = 0; i < Quality; i++) {
				if (PointInsideCircle (vertices[i], center, radius)) {
					
					velocities[i] = force;
				}
			}
		}

		bool PointInsideCircle (Vector2 point, Vector2 center, float radius) {
			return Vector2.Distance (point, center) < radius;
		}

        private void OnDrawGizmos()
        {
			
			//Gizmos.DrawMesh(mesh);
        }

    }

}