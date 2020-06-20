using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleBehaviour : MonoBehaviour
{

    public int health = 1;

	public bool randomizeHealth = true;
	public uint minHealth;
	public uint maxHealth;

    public GameObject explosionPrefab;

    private int healthLeft;

	private TextMesh scoreText;
    // Start is called before the first frame update
    void Start()
    {
		if (!randomizeHealth)
		{
			healthLeft = health;
		} else
		{
			healthLeft = (int)Random.Range(minHealth, maxHealth);
		}

		scoreText = (TextMesh)gameObject.transform.Find("Health").gameObject.GetComponent(typeof(TextMesh));
		scoreText.text = healthLeft.ToString();
	}

    private void OnCollisionEnter(Collision collision)
    {
		// exit if there is a game manager and the game is over
		if (GameManager.gm)
		{
			if (GameManager.gm.gameIsOver)
				return;
		}

		// only do stuff if hit by a projectile
		if (collision.gameObject.tag == "Projectile")
		{

			if (healthLeft > 1)
            {
				healthLeft--;
				scoreText.text = healthLeft.ToString();
				return;
            }

			if (explosionPrefab)
			{
				// Instantiate an explosion effect at the gameObjects position and rotation
				Instantiate(explosionPrefab, transform.position, transform.rotation);
			}

			// destroy the projectile
			Destroy(collision.gameObject);

			// destroy self
			Destroy(gameObject);
		}
	}
}
