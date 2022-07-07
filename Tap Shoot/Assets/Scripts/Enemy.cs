using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private Bullet bullet;
    private void Start()
    {
        Manager.instance.enemies.Add(this);
    }

	private void OnDestroy()
	{
		Manager.instance.enemies.Remove(this);
		Manager.instance.way.enemiesInRange.Remove(this);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bullet"))
		{
			if (other != null)
			{
				bullet = other.GetComponent<Bullet>();
				bullet.Boom();
			}
			Destroy(gameObject);
		}
	}
}
