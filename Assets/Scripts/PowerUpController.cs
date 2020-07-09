using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
public class PowerUpController : MonoBehaviour
{
    private TankData data;

    public List<PowerUp> powerUps;

    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<TankData>();
        powerUps = new List<PowerUp>();
    }

    // Update is called once per frame
    void Update()
    {
        List<PowerUp> expiredPowerUps = new List<PowerUp>();
        foreach (PowerUp power in powerUps)
        {
            power.durration -= Time.deltaTime;
            if (power.durration <= 0)
            {
                expiredPowerUps.Add(power);
            }
        }
        foreach (PowerUp expiredPower in expiredPowerUps)
        {
            expiredPower.OnDeactivate(data);
            powerUps.Remove(expiredPower);
        }
        expiredPowerUps.Clear();
    }

    public void AddPowerUp(PowerUp power)
    {
        power.OnActivate(data);
        if (!power.isPermanent)
        {
            powerUps.Add(power);
        }
    }
}
