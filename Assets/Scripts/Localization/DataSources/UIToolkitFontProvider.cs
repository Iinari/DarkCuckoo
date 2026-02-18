using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

[CreateAssetMenu(menuName = "Localization/UI Toolkit Font Provider")]
public class UIToolkitFontProvider : ScriptableObject
{
    [SerializeField]
    private LocalizedFont localizedFont;

    public Font CurrentFont { get; private set; }

    private void OnEnable()
    {
        if (localizedFont != null)
            localizedFont.AssetChanged += OnFontChanged;
    }

    private void OnDisable()
    {
        if (localizedFont != null)
            localizedFont.AssetChanged -= OnFontChanged;
    }

    private void OnFontChanged(Font font)
    {
        CurrentFont = font;
    }
}
