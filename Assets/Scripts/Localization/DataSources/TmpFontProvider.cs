using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

[CreateAssetMenu(menuName = "Localization/TMP Font Provider")]
public class TmpFontProvider : ScriptableObject
{
    [SerializeField]
    private LocalizedTmpFont localizedFont;

    public TMP_FontAsset CurrentFont { get; private set; }

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

    private void OnFontChanged(TMP_FontAsset font)
    {
        CurrentFont = font;
    }
}
