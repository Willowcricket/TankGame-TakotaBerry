[System.Serializable]
public class PowerUp
{
    public float speedMod;
    public float healthMod;
    public float maxHealthMod;
    public float fireRateMod;

    public float durration;
    public bool isPermanent;

    public void OnActivate(TankData target)
    {
        target.moveSpeed += speedMod;
        target.currentHealth += healthMod;
        target.maxHealth += maxHealthMod;
        target.reloadDelay -= fireRateMod;
    }

    public void OnDeactivate(TankData target)
    {
        target.moveSpeed -= speedMod;
        target.currentHealth -= healthMod;
        target.maxHealth -= maxHealthMod;
        target.reloadDelay += fireRateMod;
    }
}
