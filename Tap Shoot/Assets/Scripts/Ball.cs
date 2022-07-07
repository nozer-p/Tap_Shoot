using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    private GameObject bullet;
    [SerializeField] private Transform posBull;
    [SerializeField] private Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 minScaleLife = new Vector3(2.5f, 2.5f, 2.5f);

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Jump()
    {
        animator.SetBool("isWin", true);
    }

    private void Update()
    {
        if (transform.localScale.x < minScaleLife.x)
        {
            Manager.instance.Again();
        }
    }

    public void Charge(Vector3 pos, float holdTime)
    {
        if (bullet == null)
            return;

        Vector3 scaleChange = Vector3.one * holdTime / 100f;
        bullet.transform.localScale += scaleChange;
        transform.localScale -= scaleChange;
    }

    public void Spawn()
    {
        bullet = Instantiate(bulletPrefab, posBull.position, Quaternion.identity, null);
        bullet.transform.localScale = minScale;
        transform.localScale -= minScale;
    }

    public void Shoot(Vector3 pos)
    {
        if (bullet == null)
        {
            return;
        }

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Vector3 velocity = (pos - rb.transform.position).normalized;
        velocity.y = 0.0f;
        rb.velocity = velocity.normalized * bulletSpeed;
        bullet = null;
    }

    public float GetPathWidth()
    {
        return transform.localScale.x + 0.3f;
    }

    public float GetBulletX()
    {
        return bullet != null ? bullet.transform.localScale.x : 0f;
    }
}
