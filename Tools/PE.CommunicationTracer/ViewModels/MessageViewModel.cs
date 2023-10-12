using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using SMF.Core.DC;

namespace PE.CommunicationTracer.ViewModels
{
  public class MessageViewModel : BindableBase
  {
    public DateTime TimeStamp { get; set; }

    public string Module { get; set; }

    public bool Income { get; set; }
    
    public Guid CorrelationId { get; set; }
    
    public string MethodName { get; set; }
    
    public int CallNumber { get; set; }
    

    private long _executionTime;
    public long ExecutionTime
    {
      get { return _executionTime; }
      set { SetProperty(ref _executionTime, value); }
    }

    public Type RequestType { get; set; }

    public DataContractBase Request { get; set; }

    public Type ResponseType { get; set; }

    private DataContractBase _response;
    public DataContractBase Response
    {
      get { return _response; }
      set { SetProperty(ref _response, value); }
    }

    private Exception _exception;
    public Exception Exception
    {
      get { return _exception; }
      set { SetProperty(ref _exception, value); }
    }
  }
}
