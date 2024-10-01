using UnityEngine;

//ÃÑÀ» ½î´Â Àû
public class GunEnemyController : BaseEnemyController
{
    [SerializeField] private GameObject armObject;
    [SerializeField] private GameObject gunObject;

    protected override void Start()
    {
        base.Start();
        armObject.SetActive(true);
        gunObject.SetActive(true);
    }

    public override void Die()
    {
        base.Die();
        armObject.SetActive(false);
        gunObject.SetActive(false);
    }

    public void Reset()
    {
        base.Initialize();
        armObject.SetActive(true);
        gunObject.SetActive(true);
    }
}
