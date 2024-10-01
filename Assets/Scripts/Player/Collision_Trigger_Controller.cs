using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision_Trigger_Controller : MonoBehaviour
{
    [SerializeField] GameObject gunObject;
    [SerializeField] GameObject shieldObject;

    private WaitForSeconds delay;

    private void Start()
    {
        gunObject.SetActive(false);
        shieldObject.SetActive(false);

        delay = new WaitForSeconds(5f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gun"))
        {
            SoundManager.Instance.PlayItemSound();
            gunObject.SetActive(true);
            Destroy(collision.gameObject);
            StartCoroutine(DeactivateGun());
        }

        if (collision.CompareTag("BossGun"))
        {
            SoundManager.Instance.PlayItemSound();
            gunObject.SetActive(true);
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("Shield"))
        {
            SoundManager.Instance.PlayItemSound();
            shieldObject.SetActive(true);
            Destroy(collision.gameObject);
            GameManager.Instance.ActivateShield();
            StartCoroutine(DeactivateShield());
        }
        else if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Laser") || collision.CompareTag("Bomb") || collision.CompareTag("Planet"))
        {
            GameManager.Instance.TakeDamage();
        }
        else if (collision.CompareTag("Coin"))
        {
            SoundManager.Instance.PlayCoinSound();
            CollectCoin(collision);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("RandomParticle"))
        {
            GameManager.Instance.TakeDamage();
        }

        if (other.CompareTag("BossRoomPortal"))
        {
            SceneManager.LoadScene("BossRroom");
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
        GameManager.Instance.DeactivateShield();
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

        GameManager.Instance.AddScore(points);
        Destroy(collision.gameObject);
    }
}
