using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharButton : MonoBehaviour
{
    [System.Serializable]
    public class CharPressedEvent : UnityEvent<GameObject> { }
    public CharPressedEvent OnCharButtonPressed;
    private Button charButton;
    public void CharButtonPressed ()
    {
        OnCharButtonPressed.Invoke(gameObject);
    }

    private void OnEnable()
    {
        if(charButton == null)
        {
            charButton = GetComponent<Button>();
        }
        charButton.onClick.AddListener(CharButtonPressed);
    }

    private void OnDisable()
    {
        charButton.onClick.RemoveAllListeners();
    }

}
