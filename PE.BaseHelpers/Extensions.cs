using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SMF.Core.Notification;

namespace PE.Helpers
{
  public static class Extensions
  {
    /// <summary>
    ///   Create power set for giving list.
    ///   Ex: for input <code>{a, b, c}</code> it will generate
    ///   <code>{{a, b, c}, {a, b}, {a}, {b, c}, {b}, {a, c}, {c}, {}}</code>
    /// </summary>
    public static IEnumerable<IEnumerable<T>> PowerSet<T>(this IEnumerable<T> elements)
    {
      if (!elements.Any())
      {
        return new[] { elements };
      }

      T first = elements.First();
      IEnumerable<IEnumerable<T>> rest = PowerSet(elements.Skip(1));
      IEnumerable<IEnumerable<T>> tmp = rest.Select(el => new[] { first }.Concat(el));

      return tmp.Concat(rest);
    }

    public static DateTime ExcludeMiliseconds(this DateTime date)
    {
      return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
    }

    public static string Truncate(this string value, int maxLength)
    {
      if (string.IsNullOrEmpty(value)) return "";
      return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }

    public static void FireAndForget(this Task task, string errorMessage = null)
    {
      async Task ForgetAwaited(Task t)
      {
        try
        {
          await t;
        }
        catch (Exception ex)
        {
          NotificationController.LogException(ex,
            errorMessage ?? $"Something went wrong while execute FireAndForget task");
        }
      }

      // Only care about tasks that may fault or are faulted,
      // so fast-path for SuccessfullyCompleted and Cancelled tasks
      if (!task.IsCompleted || task.IsFaulted)
      {
        _ = ForgetAwaited(task);
      }
    }
  }
}
