using System;
using Kendo.Mvc.UI;

namespace PE.HMIWWW.Services.ViewModel
{
  public class TreeElement : TreeViewItemModel
  {
    public TreeElement(long treeId, long pid, string nodeName, string vmAccessString, bool isDevice = false)
    {
      Id = pid.ToString();
      Text = nodeName;
      IsDevice = isDevice;
      if (treeId > 0)
      {
        Url = String.Format("javascript:RenderItem({0},'{1}')", treeId, vmAccessString);
      }
    }

    private bool IsDevice { get; }
  }
}
