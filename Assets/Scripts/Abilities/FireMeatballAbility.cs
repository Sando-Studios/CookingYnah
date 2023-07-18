using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asyncoroutine;
using TMPro;

public class FireMeatballAbility : ArtifactAbility
{
    [Header("Meteor Attack")]
    [SerializeField] private float delay = 2.0f;


    [Header("Orbit Spawn")]
    [SerializeField][Min(0)] private int ballsInOrbit = 2;
    [SerializeField] private float rotationSpeed = 50.0f;
    [SerializeField] private float orbitDistance = 5.0f;
    [SerializeField] private float orbitDuration = 60.0f;
    private GameObject orbitTarget;


    private async void SpawnMeatballOrbit()
    {
        GameObject orbitCenter = new GameObject("Center");
        orbitCenter.transform.SetParent(transform);
        orbitCenter.transform.localPosition = new Vector3(0, 0, 0);

        float angleStep = 360f / ballsInOrbit;
        for (int i = 0; i < ballsInOrbit; i++)
        {
            Quaternion rotation = Quaternion.Euler(0f, angleStep * i, 0f);
            Vector3 spawnOffset = rotation * Vector3.forward * orbitDistance;
            Vector3 spawnPosition = orbitCenter.transform.position + spawnOffset;
            GameObject clone = SpawnSingleMeatball(spawnPosition);
            clone.transform.SetParent(orbitCenter.transform);

            Meteor objectMovement = clone.AddComponent<Meteor>();
            objectMovement.SetDamageValue(GetComponent<Player>().GetPlayerData().RawDamage, GetComponent<Player>().GetPlayerData().Strength);
        }
        orbitTarget = orbitCenter;

        await new WaitForSeconds(orbitDuration);

        orbitCenter.SetActive(false);
        orbitTarget = null;
        Destroy(orbitCenter);

    }

    public async void SpawnMeatballMeteor(Vector3 targetPos, float damage)
    {
        Vector3 spawnPosition = transform.position + Vector3.up * 2;
        GameObject clone = SpawnSingleMeatball(spawnPosition);
        Meteor objectMovement = clone.GetComponent<Meteor>();
        objectMovement.SetTarget(targetPos);
        objectMovement.SetDamageValue(damage); 

        await new WaitForSeconds(delay);

        objectMovement.TriggerMove();
    }

    private GameObject SpawnSingleMeatball(Vector3 location)
    {
        return Instantiate(attackPrefab, location, Quaternion.identity);
    }

    protected override void UseSpecialAttack()
    {
        SpawnMeatballOrbit();
    }

    protected override void Update()
    {
        base.Update();
        if (orbitTarget)
        {
            orbitTarget.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}