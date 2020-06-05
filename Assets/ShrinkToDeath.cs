using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkToDeath : MonoBehaviour
{
    [Header("General")]
    [SerializeField] int hitPoints = 50;
    [SerializeField] GameObject elephantParent;
    [SerializeField] float moveDownOnShrinkFudge = 3f;

    [Header("Scales")]
    [SerializeField][Tooltip("Full Health")] float fullHealthScale = 1f;
    [SerializeField][Tooltip("2/3 Health")] float twoThirdsHealthScale = .75f;
    [SerializeField][Tooltip("1/3 Health")] float oneThirdHealthScale = .50f;

    Dictionary<int, float> hitPointsToScale = new Dictionary<int, float>();

    // Start is called before the first frame update
    void Start()
    {
        BuildHitPointsToScaleDic();
    }

    private void BuildHitPointsToScaleDic()
    {
        int fullHealth = hitPoints;
        print(fullHealth);
        int twoThirdsHealth = Convert.ToInt32(fullHealth * .66666f);
        int oneThirdHealh = Convert.ToInt32(fullHealth * .333333f);

        hitPointsToScale.Add(fullHealth, fullHealthScale);
        hitPointsToScale.Add(twoThirdsHealth, twoThirdsHealthScale);
        hitPointsToScale.Add(oneThirdHealh, oneThirdHealthScale);
    }

    void OnTriggerEnter(Collider other)
    {
        ProcessHit();
        if (hitPoints <= 0) ProcessDeath();
    }

    void OnCollisionEnter(Collision collision)
    {
        ProcessHit();
        if (hitPoints <= 0) ProcessDeath();
    }

    private void ProcessHit()
    {
        hitPoints--;

        bool isReadyToScale = hitPointsToScale.ContainsKey(hitPoints);
        if (isReadyToScale)
        {
            LowerParent();
            ProcessShrink();
        }
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
        Destroy(gameObject);
    }
}
