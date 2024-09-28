using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Trigger_Controller : MonoBehaviour
{
    [SerializeField] GameObject gunObject;
    [SerializeField] GameObject shieldObject;
    [SerializeField] GameManager gameManager;

    private WaitForSeconds delay;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gunObject.SetActive(false);
        shieldObject.SetActive(false);

        delay = new WaitForSeconds(5f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gun"))
        {
            gunObject.SetActive(true);
            Destroy(collision.gameObject);
            StartCoroutine(DeactivateGun());
        }
        else if (collision.CompareTag("Shield"))
        {
            shieldObject.SetActive(true);
            Destroy(collision.gameObject);
            gameManager.ActivateShield();
            StartCoroutine(DeactivateShield());
        }
        else if (collision.CompareTag("Laser") || collision.CompareTag("Bomb") || collision.CompareTag("Planet"))
        {
            gameManager.TakeDamage();
        }
        else if (collision.CompareTag("Coin"))
        {
            CollectCoin(collision);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            GameManager.Instance.TakeDamage();
        }
    }

    private IEnumerator DeactivateGun()
    {
        yield return delay;
        gunObject.SetActive(false);
    }

    private IEnumerator DeactivateShield()
    {
        yield return delay;
        shieldObject.SetActive(false);
        gameManager.DeactivateShield();
    }

    private void CollectCoin(Collider2D collision)
    {
        bool isBronze = collision.gameObject.name.Contains("Bronze");
        bool isSilver = collision.gameObject.name.Contains("Silver");
        bool isGold = collision.gameObject.name.Contains("Gold");

        int points = 0;

        if (isBronze)
        {
            points = 50;
        }
        else if (isSilver)
        {
            points = 70;
        }
        else if (isGold)
        {
            points = 100;
        }

        gameManager.AddScore(points);
        Destroy(collision.gameObject);
    }
}
