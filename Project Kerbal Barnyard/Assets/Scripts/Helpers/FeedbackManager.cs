using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour
{
    [SerializeField] private PopupText _noMoneyPopup;
    [SerializeField] private PopupText _durabilityPopup;

    private void OnEnable()
    {
        DurabilityEvents.OnDurabilityEvent += DurabilityPopupText;
        PartPanel.OnFailedPurchace += NoMoneyPopupText;
    }
    private void OnDisable()
    {
        DurabilityEvents.OnDurabilityEvent -= DurabilityPopupText;
        PartPanel.OnFailedPurchace -= NoMoneyPopupText;
    }

    public void DurabilityPopupText(int eventID)
    {
        if(_durabilityPopup != null)
        {
            _durabilityPopup.Popup("Breaching atmosphere!");
        }
    }
    public void NoMoneyPopupText(PartPanel partPanel)
    {
        if(_noMoneyPopup != null)
        {
            _noMoneyPopup.transform.position = partPanel.transform.position;
            _noMoneyPopup.Popup("Not enough money!");
        }
    }
}
