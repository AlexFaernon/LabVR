using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Button getTubeButton;
    [SerializeField] private GameObject tutorialStartWindow;
    private bool _tubeSpawned;
    public bool NextButtonClicked { get; set; }
    private BloodSample _bloodSample;
    private TubeLabeler _tubeLabeler;
    private ResultList _resultList;
    public static bool IsTutorial { get; private set; }

    private void Awake()
    {
        _tubeLabeler = FindObjectOfType<TubeLabeler>();
        _resultList = FindObjectOfType<ResultList>();
        tutorialStartWindow.GetComponentInChildren<Button>().onClick.AddListener(() => StartCoroutine(TutorialSequence()));
        getTubeButton.onClick.AddListener(OnTubeSpawn);
    }

    private IEnumerator TutorialSequence()
    {
        foreach (var bloodSample in FindObjectsOfType<BloodSample>())
        {
            Destroy(bloodSample.gameObject);
        }
        
        _tubeSpawned = false;
        NextButtonClicked = false;
        tutorialStartWindow.SetActive(false);
        IsTutorial = true;
        
        var currentTutorial = transform.GetChild(0).gameObject;
        currentTutorial.SetActive(true);
        yield return new WaitUntil(() => _tubeSpawned);
        yield return new WaitForEndOfFrame();
        _bloodSample = FindObjectOfType<BloodSample>();
        currentTutorial.SetActive(false);
        
        currentTutorial = transform.GetChild(1).gameObject;
        currentTutorial.SetActive(true);
        yield return new WaitUntil(() => _bloodSample.isSeparated);
        currentTutorial.SetActive(false);
        
        currentTutorial = transform.GetChild(2).gameObject;
        currentTutorial.SetActive(true);
        yield return new WaitUntil(() => NextButtonClicked);
        NextButtonClicked = false;
        currentTutorial.SetActive(false);
        
        currentTutorial = transform.GetChild(3).gameObject;
        currentTutorial.SetActive(true);
        yield return new WaitUntil(() => NextButtonClicked);
        NextButtonClicked = false;
        currentTutorial.SetActive(false);
        
        currentTutorial = transform.GetChild(4).gameObject;
        currentTutorial.SetActive(true);
        yield return new WaitUntil(() => _tubeLabeler.BloodSample is not null);
        currentTutorial.SetActive(false);
        
        currentTutorial = transform.GetChild(5).gameObject;
        currentTutorial.SetActive(true);
        var oldResultCount = _resultList.Results.Count;
        yield return new WaitUntil(() => _resultList.Results.Count > oldResultCount);
        currentTutorial.SetActive(false);
        
        currentTutorial = transform.GetChild(6).gameObject;
        currentTutorial.SetActive(true);
        yield return new WaitUntil(() => NextButtonClicked);
        NextButtonClicked = false;
        currentTutorial.SetActive(false);
        
        tutorialStartWindow.SetActive(true);
        IsTutorial = false;
    }

    private void OnTubeSpawn()
    {
        if (IsTutorial)
        {
            _tubeSpawned = true;
            getTubeButton.interactable = false;
        }
    }
}
