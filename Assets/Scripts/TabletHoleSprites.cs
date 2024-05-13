using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

[RequireComponent(typeof(TabletHole))]
public class TabletHoleSprites : MonoBehaviour
{
    [SerializeField] private SpriteRenderer backgroundImage;
    [FormerlySerializedAs("agglutinatedImage")]
    [SerializeField] private SpriteRenderer stirredSprite;
    [SerializeField] private List<Sprite> agglutinatedSprites;
    [SerializeField] private List<Sprite> stirredSprites;
    [SerializeField] private Color plasmaColor;
    [SerializeField] private Color formedElements;
    [SerializeField] private Color monoclonalColor;
    [SerializeField] private Color erythrocyteColor;
    [SerializeField] private Color mixedColor;
    [SerializeField] private Color agglutinatedColor;
    private TabletHole _tabletHole;
    private readonly Random _random = new();
    private Sprite _selectedStirredSprite;
    private Sprite _selectedAgglutinatedSprite;
    

    private void Awake()
    {
        _tabletHole = GetComponent<TabletHole>();
    }

    public void Update()
    {
        bool reagent = false, blood = false;
        var tabletHoleContent = _tabletHole.Content;
        stirredSprite.gameObject.SetActive(false);
        if (tabletHoleContent.Count == 0)
        {
            backgroundImage.gameObject.SetActive(false);
            return;
        }
        backgroundImage.gameObject.SetActive(true);
        
        if (tabletHoleContent.Contains(DropperContent.AntiA) || tabletHoleContent.Contains(DropperContent.AntiB) || tabletHoleContent.Contains(DropperContent.AntiD))
        {
            backgroundImage.color = monoclonalColor;
            reagent = true;
        }
        if (tabletHoleContent.Contains(DropperContent.ErythrocyteA) || tabletHoleContent.Contains(DropperContent.ErythrocyteB) || tabletHoleContent.Contains(DropperContent.Erythrocyte0))
        {
            backgroundImage.color = erythrocyteColor;
            reagent = true;
        }
        if (tabletHoleContent.Contains(DropperContent.Plasma))
        {
            backgroundImage.color = plasmaColor;
            blood = true;
        }
        if (tabletHoleContent.Contains(DropperContent.FormedElements))
        {
            backgroundImage.color = formedElements;
            blood = true;
        }
        if (reagent && blood)
        {
            if (_tabletHole.IsStirred)
            {
                stirredSprite.gameObject.SetActive(true);
                backgroundImage.color = Color.clear;
                _selectedStirredSprite ??= stirredSprites[_random.Next(stirredSprites.Count)];
                stirredSprite.sprite = _selectedStirredSprite;
            }
            else
            {
                backgroundImage.color = mixedColor;
            }
        }
        if (_tabletHole.IsAgglutinated)
        {
            stirredSprite.gameObject.SetActive(true);
            backgroundImage.color = agglutinatedColor;
            _selectedAgglutinatedSprite ??= agglutinatedSprites[_random.Next(agglutinatedSprites.Count)];
            stirredSprite.sprite = _selectedAgglutinatedSprite;
        }
    }
}
