﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{

    public float updateFreq = 0.1f;
    public int laserDist;
    public string bounceT;
    public string splitT;
    public string sBeamtag;
    public int mBounce;
    public int mSplit;
    private float time = 0;

    private LineRenderer lLineRenderer;
    public PuzzleManager puzzleM;


    // Use this for initialization
    void Start()
    {

        time = 0;
        lLineRenderer = gameObject.GetComponent<LineRenderer>();
        StartCoroutine(PLight());



    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.tag != sBeamtag)
        {
            if (time >= updateFreq)
            {
                time = 0;
                foreach (GameObject lightSplit in GameObject.FindGameObjectsWithTag(sBeamtag))
                    Destroy(lightSplit);

                StartCoroutine(PLight());
            }
            time = +Time.deltaTime;
        }
        else
        {
            lLineRenderer = gameObject.GetComponent<LineRenderer>();
            StartCoroutine(PLight());
        }
    }

    IEnumerator PLight()
    {
        int lSplit = 1;
        int reflect = 1;
        int counter = 1;
        bool active = true;

        Vector3 direc = transform.forward;
        Vector3 lPos = transform.localPosition;

        lLineRenderer.SetVertexCount(1);
        lLineRenderer.SetPosition(0, transform.position);
        RaycastHit hit;

        while (active)
        {
            if (Physics.Raycast(lPos, direc, out hit, laserDist) && ((hit.transform.gameObject.tag == bounceT) || (hit.transform.gameObject.tag == splitT)))
            {
                reflect++;
                counter += 3;
                lLineRenderer.SetVertexCount(counter);
                lLineRenderer.SetPosition(counter - 3, Vector3.MoveTowards(hit.point, lPos, 0.01f));
                lLineRenderer.SetPosition(counter - 2, hit.point);
                lLineRenderer.SetPosition(counter - 1, hit.point);

                lLineRenderer.startWidth = lLineRenderer.endWidth = 0.4f;
                lPos = hit.point;
                Vector3 pDirec = direc;
                direc = Vector3.Reflect(direc, hit.normal);

                if (hit.transform.gameObject.tag == splitT)
                {
                    if (lSplit >= mSplit)
                    {
                        Debug.Log("Max Split");
                    }
                    else
                    {
                        lSplit++;
                        Object go = Instantiate(gameObject, hit.point, Quaternion.LookRotation(pDirec));
                        go.name = sBeamtag;
                        ((GameObject)go).tag = sBeamtag;

                    }
                }
            }

            else
            {
                if (hit.transform == null)
                {
                    reflect++;
                    counter++;
                    lLineRenderer.SetVertexCount(counter);
                    Vector3 lastPos = lPos + (direc.normalized * laserDist);
                    lLineRenderer.SetPosition(counter - 1, lPos + (direc.normalized * laserDist));

                }
                else
                {
                    reflect++;
                    counter += 3;
                    lLineRenderer.SetVertexCount(counter);
                    lLineRenderer.SetPosition(counter - 3, Vector3.MoveTowards(hit.point, lPos, 0.01f));
                    lLineRenderer.SetPosition(counter - 2, hit.point);
                    lLineRenderer.SetPosition(counter - 1, hit.point);
                    lLineRenderer.SetWidth(0.4f, 0.4f);
                    lPos = hit.point;
                    Vector3 pDirec = direc;
                    direc = Vector3.Reflect(direc, hit.normal);

                    if (hit.transform.gameObject.tag == "Target")
                    {
                        puzzleM.hitTarget(true);
                    }
                    else
                    {
                        puzzleM.hitTarget(false);
                    }
                }
                active = false;
            }
            if (reflect > mBounce)
                active = false;
        }

        yield return new WaitForEndOfFrame();
    }
}
    
