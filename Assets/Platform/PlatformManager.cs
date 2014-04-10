using UnityEngine;
using System.Collections.Generic;

/** 
 * Manages the platforms of the runner game.
 * Adapted from online tutorial.
 * 
 * @author EECS 290 Team 2
 * @author Shaun Howard
 */

public class PlatformManager : MonoBehaviour {

	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	public Vector3 minSize, maxSize, minGap, maxGap;
	public float minY, maxY;
	public Material[] materials;
	public PhysicMaterial[] physicMaterials;

	//booster object of runner
	public Booster booster;

	//invincibility object of runner
	public Invincibility invincibility;
	[Range(0, 100)] //range of spawns

	//change that platform will move in numeric form
	public int platformMoveChance;

	//next position of platform
	private Vector3 nextPosition;

	//queue of platforms to spawn
	private Queue<Transform> objectQueue;

	//dictionary with key as platforms, value as whether moving or not
	private Dictionary<Transform, bool> movingPlatforms;

	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;

		//instantiate queue given the number of objects desired
		objectQueue = new Queue<Transform>(numberOfObjects);

		//instantiate dictionary of transform and bool, to determine if given platform moves or not
		movingPlatforms = new Dictionary<Transform, bool> ();

		//add all platforms to queue, given platform prefab and starting position
		for (int i = 0; i < numberOfObjects; i++) {
			objectQueue.Enqueue((Transform)Instantiate(
				prefab, new Vector3(0f, 0f, -100f), Quaternion.identity));
		}

		enabled = false;
	}

	void Update () {

		if(objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled){
			Recycle();
		}

		//store keys in a list to iterate over
		List<KeyValuePair<Transform, bool>> listOfKeys = new List<KeyValuePair<Transform, bool>>(movingPlatforms);

		//iterate through list of keys, modify dictionary
		foreach(KeyValuePair<Transform, bool> platform in listOfKeys){
			Vector3 position = platform.Key.localPosition;
			float positionRange = (maxY - minY) / 2 ;
			float increment = positionRange / 400;
			bool direction = true;
			direction = platform.Value;

			//check if platform is moving, if not, set key to false
			if(position.y > maxY - increment && direction){
				movingPlatforms.Remove (platform.Key);
				movingPlatforms.Add (platform.Key, false);
			}

			//otherwise, if platform is moving, set key to true
			else if(position.y < minY + increment && !direction){
				movingPlatforms.Remove (platform.Key);
				movingPlatforms.Add (platform.Key, true);
			}

			//Check direction, if in upward direction, increment vertical position
			if(direction){
				position.y += increment;
			} else { //otherwise decrement vertical position
				position.y -= increment;
			}

			//local position of this platform is at given position
			platform.Key.localPosition = position;
		}
	}

	private void Recycle () {
		Vector3 scale = new Vector3(
			Random.Range(minSize.x, maxSize.x),
			Random.Range(minSize.y, maxSize.y),
			Random.Range(minSize.z, maxSize.z));

		Vector3 position = nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;

		//dequeues object from queue
		Transform o = objectQueue.Dequeue();

		//checks if dictionary contains dequeued object
		//if so, removes object from dictionary
		if(movingPlatforms.ContainsKey(o)){
			movingPlatforms.Remove (o);
		}

		o.localScale = scale;
		o.localPosition = position;
		int materialIndex = Random.Range(0, materials.Length);

		//when platform move chance is higher than random number in [0,100] interval
		//add as moving platform to dictionary
		if (platformMoveChance > Random.Range (0, 100)) {
			movingPlatforms.Add (o, true);
		} 

		//otherwise, if booster cannot spawn, spawn invincibility at random position
		else if (!booster.SpawnIfAvailable (position)) {
			invincibility.SpawnIfAvailable (position);
		}

		o.renderer.material = materials[materialIndex];
		o.collider.material = physicMaterials[materialIndex];
		objectQueue.Enqueue(o);

		nextPosition += new Vector3(
			Random.Range(minGap.x, maxGap.x) + scale.x,
			Random.Range(minGap.y, maxGap.y),
			Random.Range(minGap.z, maxGap.z));

		if(nextPosition.y < minY){
			nextPosition.y = minY + maxGap.y;
		}

		else if(nextPosition.y > maxY){
			nextPosition.y = maxY - maxGap.y;
		}
	}
	
	private void GameStart () {
		nextPosition = startPosition;

		for(int i = 0; i < numberOfObjects; i++){
			Recycle();
		}

		enabled = true;
	}

	private void GameOver () {
		enabled = false;
	}

}