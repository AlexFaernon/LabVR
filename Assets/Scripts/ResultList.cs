using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ResultList : MonoBehaviour
{
    [FormerlySerializedAs("bloodTypeResult")]
    [SerializeField] private TMP_Text bloodTypeResultLabel;
    [FormerlySerializedAs("rhesusResultlabel")]
    [FormerlySerializedAs("rhesusResult")]
    [SerializeField] private TMP_Text rhesusResultLabel;
    [FormerlySerializedAs("bloodTypeAnswer")]
    [SerializeField] private TMP_Text bloodTypeAnswerLabel;
    [FormerlySerializedAs("rhesusAnswer")]
    [SerializeField] private TMP_Text rhesusAnswerLabel;
    [SerializeField] private TMP_Text resultNumberLabel;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    public readonly List<(BloodClass, BloodClass)> Results = new();
    private int _currentResultNumber;

    public int CurrentResultNumber
    {
        get => _currentResultNumber;
        set
        {
            _currentResultNumber = value;
            SetResultPage();
        }
    }

    private void Awake()
    {
        nextButton.onClick.AddListener(() => CurrentResultNumber++);
        previousButton.onClick.AddListener(() => CurrentResultNumber--);
    }

    private void Update()
    {
        nextButton.interactable = CurrentResultNumber < Results.Count - 1;
        previousButton.interactable = CurrentResultNumber > 0;
    }

    public void AddResult(BloodSample bloodSample)
    {
        Results.Insert(0, (bloodSample.AssumedBloodClass, bloodSample.BloodClass));
        CurrentResultNumber = 0;
    }
    
    private void SetResultPage()
    {
        var bloodTypeResult = Results[CurrentResultNumber].Item1.BloodType;
        var rhResult = Results[CurrentResultNumber].Item1.Rh;
        var bloodTypeAnswer = Results[CurrentResultNumber].Item2.BloodType;
        var rhAnswer = Results[CurrentResultNumber].Item2.Rh;
        
        bloodTypeResultLabel.text = $"Группа крови: {bloodTypeResult}";
        rhesusResultLabel.text = $"Резус-фактор: {(rhResult ? "Положительный" : "Отрицательный")}";
        bloodTypeAnswerLabel.text = $"{bloodTypeAnswer}";
        rhesusAnswerLabel.text = $"{(rhAnswer ? "Положительный" : "Отрицательный")}";
        
        bloodTypeResultLabel.color = bloodTypeResult == bloodTypeAnswer ? Color.green : Color.red;
        rhesusResultLabel.color = rhResult == rhAnswer ? Color.green : Color.red;

        resultNumberLabel.text = $"{CurrentResultNumber + 1}/{Results.Count}";
    }
}
