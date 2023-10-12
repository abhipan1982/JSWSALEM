using System;
using System.ComponentModel.DataAnnotations;
using PE.BaseDbEntity.Models;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.ViewModel;
using SMF.HMIWWW.Attributes;

namespace PE.HMIWWW.ViewModel.System
{
  public class VM_Crew : VM_Base
  {
    #region properties

    public virtual long? CrewId { get; set; }

    [SmfDisplay(typeof(VM_Crew), "CreatedTs", "NAME_DateTimeCreated")]
    public virtual DateTime CreatedTs { get; set; }

    [SmfDisplay(typeof(VM_Crew), "LastUpdateTs", "NAME_DateTimeLastUpdate")]
    public virtual DateTime LastUpdateTs { get; set; }

    //[Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired", ErrorMessageResourceType = typeof(VM_Resources))]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfDisplay(typeof(VM_Crew), "CrewName", "NAME_Name")]
    [SmfRequired]
    public virtual String CrewName { get; set; }

    [Required(ErrorMessageResourceName = "FORM_ATTRIBUTE_FieldIsRequired",
      ErrorMessageResourceType = typeof(VM_Resources))]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    [SmfDisplay(typeof(VM_Crew), "Description", "NAME_Description")]
    public virtual String CrewDescription { get; set; }

    public virtual String Color { get; set; }

    [SmfDisplay(typeof(VM_Crew), "LeaderName", "NAME_Leader")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "", ErrorMessageResourceName = "GLOB_StringLength",
      ErrorMessageResourceType = typeof(VM_Resources))]
    public virtual String LeaderName { get; set; }

    #endregion

    #region ctor

    public VM_Crew()
    {
    }

    public VM_Crew(EVTCrew entity)
    {
      CrewId = entity.CrewId;
      //TODOMN exclude this
      //this.CreatedTs = entity.CreatedTs;
      //this.LastUpdateTs = entity.LastUpdateTs;
      CrewName = entity.CrewName;
      CrewDescription = entity.CrewDescription;
      LeaderName = entity.LeaderName;


      Color = "#ff917e"; //Temporary for test
    }

    #endregion
  }
}
