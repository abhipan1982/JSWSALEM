using System;

namespace PE.HMIWWW.ViewModel
{
  [Serializable]
  public class TextLookupModel
  {
    public TextLookupModel(string id, string name)
    {
      Id = id;
      Name = name;
    }

    public TextLookupModel()
    {
      Id = "";
      Name = "";
    }

    public string Name { get; set; }
    public string Id { get; set; }
  }

  //public class TextLookupModelList : VM_BaseList<TextLookupModel> { }
}
