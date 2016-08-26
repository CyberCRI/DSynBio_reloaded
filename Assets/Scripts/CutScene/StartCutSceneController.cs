﻿using UnityEngine;
using System.Collections;

public class StartCutSceneController : CutScene {

    [SerializeField]
    private iTweenEvent _iTweenEventBacteria;
    [SerializeField]
    private iTweenEvent _itweenEventBacteria2;
    [SerializeField]
    private GameObject _dnaTube;
    [SerializeField]
    private iTweenEvent _iTweenEventDNA;
    [SerializeField]
    private TweenScale _tweenScale;
    private Vector3 _originFromTweenScale;
    private Vector3 _originToTweenScale;

	// Use this for initialization
	public override void initialize()
    {

        _originFromTweenScale = _tweenScale.from;
        _originToTweenScale = _tweenScale.to;
        
        start();
    }

    void Update()
    {
        _cellControl.freezePlayer(true);
    }

    void ReverseAnim()
    {
        _tweenScale.from = _originToTweenScale;
        _tweenScale.to = _originFromTweenScale;
        _tweenScale.duration = 1.5f;
        _tweenScale.enabled = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "dummyPlayer")
        {
            Destroy(col.gameObject);
            end ();
        }
    }
    
    public override void endCutScene()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    }

    public override void startCutScene()
    {
        _cellControl.freezePlayer(true);
        _iTweenEventBacteria.enabled = true;
        StartCoroutine(WaitForSecondPart());
    }

    void SecondPart()
    {
        _dnaTube.SetActive(true);
        StartCoroutine(WaitForThirdPart());
    }

    void ThirdPart()
    {
        _iTweenEventDNA.enabled = true;
        _iTweenEventDNA.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(WaitForFourthPart());
    }

    void FourthPart()
    {
        ReverseAnim();
        //_iTweenEventDNA.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(WaitForFifthPart());
    }

    void FifthPart()
    {
        _itweenEventBacteria2.enabled = true;

        StartCoroutine(WaitForEnd());
    }

    IEnumerator WaitForSecondPart()
    {
        yield return new WaitForSeconds(5);
        SecondPart();
        yield return null;
    }

    IEnumerator WaitForThirdPart()
    {
        yield return new WaitForSeconds(1.5f);
        ThirdPart();
        yield return null;
    }

    IEnumerator WaitForFourthPart()
    {
        yield return new WaitForSeconds(2);
        FourthPart();
        yield return null;
    }

    IEnumerator WaitForFifthPart()
    {
        yield return new WaitForSeconds(0.5f);
        FifthPart();
        yield return null;
    }

    IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(2f);
        end();
        yield return null;
    }
}