using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSRandomizer : BaseClass {
    SkinnedMeshRenderer skinnedMeshRe;
    float[] currShapes;
    float[] targetShapes;

    float currTime = 0.0f;
    public float interval = 0.6f;
    public float interpolationSpeed = 2f;
	// Use this for initialization
	void Start () {
		
	}

    public override void Initialize()
    {
        base.Initialize();
        skinnedMeshRe = GetComponent<SkinnedMeshRenderer>();
        currShapes = new float[skinnedMeshRe.sharedMesh.blendShapeCount];
        targetShapes = new float[skinnedMeshRe.sharedMesh.blendShapeCount];

        StartCoroutine(RandomBlend());
    }

    // Update is called once per frame
    void Update () {
		for(int i = 0; i < targetShapes.Length; i++)
        {
            float newValue = Mathf.Lerp(currShapes[i], targetShapes[i], (Time.time - currTime) * interpolationSpeed);
            skinnedMeshRe.SetBlendShapeWeight(i, newValue);
        }
	}

    IEnumerator RandomBlend()
    {
        while(this != null)
        {
            for(int i = 0; i < targetShapes.Length; i++)
            {
                currShapes[i] = skinnedMeshRe.GetBlendShapeWeight(i);
                targetShapes[i] = Random.Range(0.0f, 100.0f);
                currTime = Time.time;
            }
            yield return new WaitForSeconds(interval);
        }
    }
}
