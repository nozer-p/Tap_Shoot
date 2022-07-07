using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private bool isBoom = false;
	private float timeDestroy = 0.012f;

	public void Boom()
    {
		if (!isBoom)
		{
			transform.localScale *= 3.3f;
			isBoom = true;
		}
    }

    private void Update()
    {
		if (timeDestroy <= 0f && isBoom)
        {
			Destroy(gameObject);
		}
		else if (isBoom)
        {
			timeDestroy -= Time.deltaTime;
        }
    }
}