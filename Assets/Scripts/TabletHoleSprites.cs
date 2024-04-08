using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(TabletHole))]
public class TabletHoleSprites : MonoBehaviour
{
    [SerializeField] private SpriteRenderer backgroundImage;
    [SerializeField] private SpriteRenderer agglutinatedImage;
    [SerializeField] private List<Sprite> agglutinatedSprites;
    [SerializeField] private Color plasmaColor;
    [SerializeField] private Color formedElements;
    [SerializeField] private Color monoclonalColor;
    [SerializeField] private Color erythrocyteColor;
    [SerializeField] private Color mixedColor;
    [SerializeField] private Color agglutinatedColor;
    private TabletHole _tabletHole;
    private readonly Random _random = new();
    private Sprite _selectedAgglutinatedSprite;

    private void Awake()
    {
        _tabletHole = GetComponent<TabletHole>();
    }

    public void Update()
    {
        bool reagent = false, blood = false;
        var tabletHoleContent = _tabletHole.Content;
        if (tabletHoleContent.Count == 0)
        {
            backgroundImage.gameObject.SetActive(false);
            agglutinatedImage.gameObject.SetActive(false);
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
            backgroundImage.color = mixedColor;
        }
        if (_tabletHole.IsAgglutinated)
        {
            agglutinatedImage.gameObject.SetActive(true);
            backgroundImage.color = agglutinatedColor;
            _selectedAgglutinatedSprite ??= agglutinatedSprites[_random.Next(agglutinatedSprites.Count)];
            agglutinatedImage.sprite = _selectedAgglutinatedSprite;
        }
    }
}
