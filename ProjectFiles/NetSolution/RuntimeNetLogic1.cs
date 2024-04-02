#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.Retentivity;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.Core;
using FTOptix.NetLogic;
using FTOptix.WebUI;
#endregion

public class RuntimeNetLogic1 : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        selectedItems = LogicObject.Owner.Get<IUAVariable>("UISelectedItems");
        selectedItems.VariableChange += SelectedItems_VariableChange;

        selectedItemsDisplayValue = LogicObject.Get<IUAVariable>("SelectedItemsDisplayValue");
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    private void SelectedItems_VariableChange(object sender, VariableChangeEventArgs e)
    {
        var items = (NodeId[]) e.NewValue;
        var display = "";
        foreach (var i in items)
            display += Project.Current.Context.GetNode(i).BrowseName + "\n";
        selectedItemsDisplayValue.Value = display;
    }

    private IUAVariable selectedItems;
    private IUAVariable selectedItemsDisplayValue;
}
