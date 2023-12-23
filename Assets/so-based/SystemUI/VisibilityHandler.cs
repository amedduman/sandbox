using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SystemUI
{
    public class VisibilityHandler : MonoBehaviour
    {
        [SerializeField] List<UI_Element> _uiElements;

        public void Show(UI_ID id, ShowMethod showMethod = ShowMethod.Single)
        {
            switch (showMethod)
            {
                case ShowMethod.Single:
                    ShowSingle(id);
                    break;
                case ShowMethod.Additive:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(showMethod), showMethod, null);
            }
        }

        void ShowSingle(UI_ID id)
        {
            foreach (var element in _uiElements)
            {
                if (element.ID != id)
                {
                    element.Hide();
                }
                else
                {
                    element.Show();
                }
            }
        }
    }
}
 
public enum ShowMethod
{
    Single,
    Additive,
}