﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkToDeath : MonoBehaviour
{
    [Header("General")]
    [SerializeField] string friendlyTagName;
    [SerializeField] int hitPoints = 50;
    [SerializeField] GameObject elephantParent;
    [SerializeField] float moveDownOnShrinkFudge = 3f;
    [SerializeField] ParticleSystem collisionParticles;

    [Header("Scales")]
    [SerializeField][Tooltip("Full Health")] float fullHealthScale = 1f;
    [SerializeField][Tooltip("2/3 Health")] float twoThirdsHealthScale = .75f;
    [SerializeField][Tooltip("1/3 Health")] float oneThirdHealthScale = .50f;

    Dictionary<int, float> hitPointsToScale = new Dictionary<int, float>();
    bool isAlive = true;
    int fullHealth;

    // Start is called before the first frame update
    void Start()
    {
        if (collisionParticles)
        {
            var collisionEmission = collisionParticles.emission;
            collisionEmission.enabled = false;
        }
        BuildHitPointsToScaleDic();
    }

    public bool GetIsAlive()
    {
        return isAlive;
    }

    private void BuildHitPointsToScaleDic()
    {
        fullHealth = hitPoints;
        print(fullHealth);
        int twoThirdsHealth = Convert.ToInt32(fullHealth * .66666f);
        int oneThirdHealh = Convert.ToInt32(fullHealth * .333333f);

        hitPointsToScale.Add(fullHealth, fullHealthScale);
        hitPointsToScale.Add(twoThirdsHealth, twoThirdsHealthScale);
        hitPointsToScale.Add(oneThirdHealh, oneThirdHealthScale);
    }

    void OnTriggerEnter(Collider other)
    {
        ProcessHit(other);
        if (hitPoints <= 0) ProcessDeath();
    }

/*    void OnCollisionEnter(Collision collision)
    {
        ProcessHit();
        if (hitPoints <= 0) ProcessDeath();
    }*/

    private void ProcessHit(Collider other)
    {
        if (other.tag == friendlyTagName) { return; }

        if (collisionParticles)
        {
            StartCoroutine(ProcessHitEffect());
        }

        hitPoints--;

        bool isReadyToScale = fullHealth != hitPoints && hitPointsToScale.ContainsKey(hitPoints);
        if (isReadyToScale)
        {
            LowerParent();
            ProcessShrink();
        }

        
    }

    IEnumerator ProcessHitEffect()
    {
        print("HEY");
        var particleEmission = collisionParticles.emission;
        particleEmission.enabled = true;

        yield return new WaitForSeconds(1f);

        particleEmission.enabled = false;
    }

    private void LowerParent()
    {
        elephantParent.gameObject.transform.position = new Vector3(
            elephantParent.gameObject.transform.position.x,
            elephantParent.gameObject.transform.position.y - moveDownOnShrinkFudge,
            elephantParent.gameObject.transform.position.z
        );
    }

    private void ProcessShrink()
    {
        float scaleToShrink = hitPointsToScale[hitPoints];

        Vector3 newScale = elephantParent.gameObject.transform.localScale;
        newScale.Set(scaleToShrink, scaleToShrink, scaleToShrink);
        elephantParent.transform.localScale = newScale;
    }

    private void ProcessDeath()
    {
        var particleEmission = collisionParticles.emission;
        particleEmission.enabled = false;

        isAlive = false;
    }
}
