using Unity.Properties;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "FontDataSource", menuName = "Scriptable Objects/FontDataSource")]
public class FontDataSource : ScriptableObject
{
    [SerializeField]
    LocalizedFont m_Font;
    
    [CreateProperty] 
    public Font defaultFont { get; set; }

    private void OnEnable()
    {
        m_Font.AssetChanged += f => defaultFont = f;
    }
}
