using UnityEngine;

public class HouseOwner : MonoBehaviour
{
    [SerializeField]
    private HousePicture housePicture;

    [SerializeField]
    private Gate gate;

    private ReactInteraction reactInteraction;

    private void Start()
    {
        reactInteraction = GetComponent<ReactInteraction>();
    }

    public void Interact()
    {
        if (reactInteraction)
            reactInteraction.React();
    }

    public void InvestigateFirstNoneItem()
    {
        DialogueSystem.Instance.StartDialogue("First_Lady");
    }

    public void InvestigateRetryNoneItem()
    {
        DialogueSystem.Instance.StartDialogue("Many_Times_Lady");
    }

    public void UseKey()
    {
        housePicture.UnlockGate();

        DialogueSystem.Instance.StartDialogue("Give_Key_Lady", OpenGate);
        GameManager.Instance.Inventory.DeleteItem(EItemType.CHAPTER1_KEY);
    }

    private void OpenGate()
    {
        SoundSystem.Instance.PlaySFX("HouseDoor", transform.position);
        DialogueSystem.Instance.StartDialogue("Open_Gate", EnterHouse);
    }

    public void EnterHouse()
    {
        housePicture.ChangePicture(EHousePictureType.Inside, AfterEnterHouse);
    }

    private void AfterEnterHouse()
    {
        DialogueSystem.Instance.StartDialogue("After_Change_House", DeactiveGate);
    }

    private void DeactiveGate()
    {
        housePicture.gate = false;
        gate.gameObject.SetActive(false);
    }
}
