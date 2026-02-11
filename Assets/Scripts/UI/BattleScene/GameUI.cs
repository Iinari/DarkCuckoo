using Mono.Cecil.Cil;
using SnIProductions;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    private UIDocument document;
    [SerializeField] private VisualTreeAsset cardTemplate;

    [SerializeField] private Button endTurnBtn;

    private VisualElement handContainer;

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.pickingMode = PickingMode.Ignore;

        handContainer = document.rootVisualElement.Q("HandContainer");

        endTurnBtn = root.Q<Button>("BtnEndTurn");

        endTurnBtn.RegisterCallback<ClickEvent>(EndTurnButtonClicked);
    }

    private void EndTurnButtonClicked(ClickEvent clickEvent)
    {
        FindFirstObjectByType<BattleSystem>().EnemyTurn();
    }

    public void AddCard(CardData data)
    {
        var card = cardTemplate.Instantiate();
        card.Q<Label>("Title").text = data.cardName;


        handContainer.Add(card);
    }
}
