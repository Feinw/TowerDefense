﻿using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;
	public GameObject ImapctEffect;

	public float speed = 70f;
	public int damage = 1;
	public float EXPLOSION = 0f;

	public void seek(Transform _target){
		target = _target;
	}

	// Update is called once per frame
	void Update () {
		if(target == null){
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed*Time.deltaTime;

		if(dir.magnitude <= distanceThisFrame){
			HitTarget();
			return;
		}
		transform.Translate (dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);
	}
	void HitTarget(){
		GameObject effectin = (GameObject) Instantiate(ImapctEffect,transform.position,transform.rotation);
		Destroy(effectin, 2f);

		if(EXPLOSION > 0){
			Explode();
		} else{
			Damage(target);
		}
		Destroy(gameObject);
	}

	void Explode(){
		Collider[] colliders = Physics.OverlapSphere(transform.position, EXPLOSION);
		foreach(Collider collider in colliders){
			if(collider.tag == "Enemy"){
				Damage(collider.transform);
			}
		}
	}
		
	void Damage(Transform enemyGameObject){
		enemy e = enemyGameObject.GetComponent<enemy>();

		if(e != null){
			e.takeDamage(damage);
		}
	}

}