using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalOpener : MonoBehaviour
{
    public GameObject Modal;

    public void OpenModal()
    {
        if(Modal != null)
        {
            bool isActive = Modal.activeSelf;
            Modal.SetActive(!isActive);
        }
    }

    public void CloseModal()
    {
        Modal.SetActive(false);
    }
}
