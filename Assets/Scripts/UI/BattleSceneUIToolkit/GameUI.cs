using Mono.Cecil.Cil;
using SnIProductions;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    private UIDocument document;
    [SerializeField] private VisualTreeAsset cardTemplate;

    [SerializeField] private Button endTurnBtn;

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.pickingMode = PickingMode.Ignore;

        endTurnBtn = root.Q<Button>("BtnEndTurn");

        endTurnBtn.RegisterCallback<ClickEvent>(EndTurnButtonClicked);
    }

    private void EndTurnButtonClicked(ClickEvent clickEvent)
    {
        FindFirstObjectByType<BattleSystem>().EnemyTurn();
    }

}
