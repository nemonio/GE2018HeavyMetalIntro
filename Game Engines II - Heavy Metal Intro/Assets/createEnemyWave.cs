using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createEnemyWave : MonoBehaviour {

	public GameObject enemyPrefab;

	public Transform Player;

	public Vector3 offsetFromPlayer;

	public List<GameObject> EnemiesList;

	public List<GameObject> BulletsList;

	public GameObject bulletsFolder;


	public Cinemachine.CinemachineVirtualCamera leaderCamera;

	public GameObject bullet;

	public float speed = 10;
	public float shootDistance = 10;


	public float gap = 20;
	public float followers = 4;

	private GameObject tempLeader;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		ShootIfCloseToPlayer ();
		destroyFarAwayBullets ();
		
	}

	void ShootIfCloseToPlayer () {

		foreach (GameObject myEnemy in EnemiesList) {

			if (Vector3.Distance(myEnemy.transform.position, Player.position) < shootDistance)
			{
				GameObject myBullet = Instantiate (bullet);

				BulletsList.Add (myBullet);

				myBullet.transform.SetParent (bulletsFolder.transform);

				myBullet.GetComponent<Rigidbody>().velocity = (Player.position - myEnemy.transform.position) * speed;
			}
		}

	}

	void destroyFarAwayBullets () {

		foreach (GameObject myBullet in BulletsList) {

			if (Vector3.Distance(myBullet.transform.position, Player.position) > shootDistance*2)
			{

				BulletsList.Remove (myBullet);
				Destroy (myBullet);

			}
		}

	}



	void OnEnable () {

		for (int row = 0; row <= followers; row++) {

			GameObject myGameObjectInTheLeft = Instantiate (enemyPrefab);

			Vector3 leftOffset = new Vector3 (-row * gap, 0, -row * gap);

			myGameObjectInTheLeft.transform.position = Player.position + leftOffset + offsetFromPlayer;

			//myGameObjectInTheLeft.transform.rotation = this.transform.rotation;

			myGameObjectInTheLeft.transform.SetParent (this.transform);


			//myGameObjectInTheLeft.Destroy(Seek) ();

			EnemiesList.Add (myGameObjectInTheLeft);






			if (row != 0) {


				myGameObjectInTheLeft.AddComponent<OffsetPursue> ();

				myGameObjectInTheLeft.GetComponent<OffsetPursue> ().leader = tempLeader.GetComponent<Boid>();







				GameObject myGameObjectInTheRight = Instantiate (enemyPrefab);


				EnemiesList.Add (myGameObjectInTheRight);




				Vector3 rightOffset = new Vector3 (row * gap, 0, -row * gap);

				myGameObjectInTheRight.transform.position = Player.position + rightOffset + offsetFromPlayer;

				//myGameObjectInTheRight.transform.rotation = this.transform.rotation;

				myGameObjectInTheRight.transform.SetParent (this.transform);

				myGameObjectInTheRight.AddComponent<OffsetPursue> ();

				myGameObjectInTheRight.GetComponent<OffsetPursue> ().leader = tempLeader.GetComponent<Boid>();



				//Change to 
				//SteeringBehaviour followerSeek = myGameObjectInTheLeft.AddComponent<Seek> ();


			} else {

				myGameObjectInTheLeft.gameObject.name = "Leader";

				//myGameObjectInTheLeft.Destroy(Seek) ();

				myGameObjectInTheLeft.AddComponent<Seek> ();

				myGameObjectInTheLeft.GetComponent<Seek> ().targetGameObject = Player.gameObject;


				tempLeader = myGameObjectInTheLeft;

				leaderCamera.Follow = tempLeader.transform;

				leaderCamera.LookAt = tempLeader.transform;

				//leaderCamera.transform.SetParent (tempLeader.transform);


				//SteeringBehaviour leaderSeek = myGameObjectInTheLeft.AddComponent<Seek> ();

			}



		}

	}
}
