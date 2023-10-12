using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SMF.Core.Notification;

namespace PE.Core.UniversalValidator
{ 
  public abstract class ValidatorBase<T> where T : ValidationResultBase, new()
  {
    private List<Func<Task<T>>> _executionPlanAsync = new List<Func<Task<T>>>();
    private List<Func<T>> _executionPlan = new List<Func<T>>();
    protected T Result { get; private set; } = new T();
    
    
    public ValidatorBase<T> AddValidatorAsync(Func<Task<T>> validationInstance)
    {
      _executionPlanAsync.Add(validationInstance);
      
      return this;
    }
    public ValidatorBase<T> AddValidator(Func<T> validationInstance)
    {
      _executionPlan.Add(validationInstance);
      
      return this;
    }

    public async Task<T> RunAsync()
    {
      foreach (var execution in _executionPlanAsync)
      {
        try
        {
          var executionResult = await execution();

          Result = executionResult;

          if (!executionResult.IsValid)
            break;
        }
        catch (Exception e)
        {
          ExceptionHandler(e, nameof(execution));
          break;
        }
      }

      return Result;
    }
    
    public T Run()
    {
      foreach (var execution in _executionPlan)
      {
        try
        {
          var executionResult = execution();

          Result = executionResult;

          if (!executionResult.IsValid)
            break;
        }
        catch (Exception e)
        {
          ExceptionHandler(e, nameof(execution));
          break;
        }
      }

      return Result;
    }
    
    protected void ExceptionHandler(Exception ex, string validationMethod)
    {
      Result.SetValid(false);
      NotificationController.LogException(ex, $"Something unexpected happened while validate {validationMethod}");
    }
  }
}
