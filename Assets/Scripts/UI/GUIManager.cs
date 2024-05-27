using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : Singleton<GUIManager>
{
    [SerializeField] private Dialog m_gameoverDialog;
    private Dialog m_activeDialog;

    public Dialog ActiveDialog { get => m_activeDialog; private set => m_activeDialog = value; }
    private void ShowDialog(Dialog dialog)
    {
        if (dialog == null) return;
        m_activeDialog = dialog;
        m_activeDialog.Show(true);

    }
    public void ShowGameoverDialog()
    {
        ShowDialog(m_gameoverDialog);
    }
}
