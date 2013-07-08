namespace MockupBuilder.Shell
{
  using System;
  using System.Collections.Generic;
  using Microsoft.Practices.Prism.Logging;

  public class CallbackLogger : ILoggerFacade
  {
    private readonly Queue<Tuple<string, Category, Priority>> savedLogs = new Queue<Tuple<string, Category, Priority>>();

    public Action<string, Category, Priority> Callback { get; set; }

    public void Log(string message, Category category, Priority priority)
    {
      if (Callback != null)
        Callback(message, category, priority);
      else
        savedLogs.Enqueue(new Tuple<string, Category, Priority>(message, category, priority));
    }

    public void ReplaySavedLogs()
    {
      if (Callback != null)
      {
        while (savedLogs.Count > 0)
        {
          var log = savedLogs.Dequeue();
          Callback(log.Item1, log.Item2, log.Item3);
        }
      }
    }
  }
}
