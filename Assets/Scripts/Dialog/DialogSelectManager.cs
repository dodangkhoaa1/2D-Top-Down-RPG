using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSelectManager : Singleton<DialogSelectManager>
{
    [SerializeField] private Dialog m_alertCharterSelection;
    [SerializeField] private Dialog m_alertMapSelection;


    private Dialog m_activeDialog;

    public Dialog ActiveDialog { get => m_activeDialog; private set => m_activeDialog = value; }

    private void ShowDialog(Dialog dialog)
    {
        if (dialog == null) return;
        m_activeDialog = dialog;
        m_activeDialog.Show(true);

    }
    public void ShowAlertCharterSelection()
    {
        ShowDialog(m_alertCharterSelection);
    }
    public void ShowAlertMapSelection()
    {
        ShowDialog(m_alertMapSelection);
    }
}
