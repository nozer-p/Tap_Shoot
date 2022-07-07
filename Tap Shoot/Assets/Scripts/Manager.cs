using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
	/*
	public static event OnSwipeInput SwipeEvent;
	public delegate void OnSwipeInput(Vector2 direction, float delta);
	*/
	public static Manager instance = null;

	[System.NonSerialized] public List<Enemy> enemies = new List<Enemy>();

	public Way way;
	[SerializeField] private LineRenderer lineForShoot;
	[SerializeField] private LayerMask whatIsGround;
	[SerializeField] private Ball ball;
	[SerializeField] private GameObject tap;
	[SerializeField] private GameObject win;
	[SerializeField] private GameObject lose;

	private bool isMobile;
	private bool isTouch;
	private bool isPlaying = false;
	private bool isWin = false;
	private bool isLose = false;
	private float timeStart;

	private float holdTime = 0;
	private Vector3 lastTouchPos;

	private void Awake()
	{
		way.SetWidth(ball.GetPathWidth());
		lineForShoot.gameObject.SetActive(false);
		instance = this;
	}

	private void Start()
	{
		timeStart = 4.4f;
		isMobile = Application.isMobilePlatform;
		isPlaying = true;
		isWin = false;
		isLose = false;
	}
	
	private void Update()
	{		
		if (!isPlaying && isWin)
        {
			if (timeStart <= 0)
            {
				Application.LoadLevel(Application.loadedLevel);
			}
			else
            {
				timeStart -= Time.deltaTime;
            }
        }

		if (!isPlaying && isLose)
		{
			if (timeStart <= 0)
			{
				Application.LoadLevel(Application.loadedLevel);
			}
			else
			{
				timeStart -= Time.deltaTime;
			}
		}

		if (way.EnemiesCount == 0 && isPlaying)
		{
			Win();
		}
		
		if (!isMobile && isPlaying)
		{
			if (Input.GetMouseButtonDown(0))
			{
				SpawnBullet();
			}
			else if (Input.GetMouseButtonUp(0))
			{
				ShootBullet();
			}
		}
		else if (isMobile && isPlaying)
		{
			if (Input.touchCount > 0)
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					SpawnBullet();
				}
				else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
				{
					ShootBullet();
				}
			}
		}

		if (isTouch && isPlaying)
        {
			ChargeBullet();
        }
	}

	private void SpawnBullet()
    {
		isTouch = true;
		lineForShoot.gameObject.SetActive(true);
		holdTime = 0.0f;
		ball.Spawn();
	}

	private void ChargeBullet()
    {
		holdTime += Time.deltaTime;
		Ray ray;
		if (isMobile)
        {
			ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
		}
        else
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		}
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 50f, whatIsGround))
		{
			lastTouchPos = new Vector3(hit.point.x, 0.01f, hit.point.z);
			lineForShoot.SetPosition(1, lastTouchPos);
			lineForShoot.startWidth = lineForShoot.endWidth = ball.GetBulletX();
			way.SetWidth(ball.GetPathWidth());
		}

		ball.Charge(lastTouchPos, holdTime);
	}

	private void ShootBullet()
    {
		isTouch = false;
		lineForShoot.gameObject.SetActive(false);
		ball.Shoot(lastTouchPos);
	}

	public void Win()
	{
		ball.Shoot(lastTouchPos);
		isPlaying = false;
		isWin = true;
		lineForShoot.gameObject.SetActive(false);
		ball.Jump();
		win.SetActive(true);
	}

	public void Again()
	{
		isPlaying = false;
		isLose = true;
		lose.SetActive(true);
	}
}
