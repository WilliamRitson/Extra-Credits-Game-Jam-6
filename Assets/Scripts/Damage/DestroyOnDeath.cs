public class DestroyOnDeath : OnDeathBehavior
{
    protected override void OnDeath()
    {
        Destroy(gameObject);
    }
}
