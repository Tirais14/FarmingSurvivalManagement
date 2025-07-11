using System;
using UnityEngine;
using UTIRLib.Collections;
using Object = UnityEngine.Object;

#nullable enable
namespace Core
{
    public static class GameDebug
    {
        private static readonly Flags<LogType> enabledLogs = new(){
            LogType.Log,
            LogType.Warning,
            LogType.Error,
            LogType.Exception,
        };

        public static void Enable(params LogType[] logTypes)
        {
            for (int i = 0; i < logTypes.Length; i++)
                enabledLogs.TryAdd(logTypes[i]);
        }

        public static void Disable(params LogType[] logTypes)
        {
            for (int i = 0; i < logTypes.Length; i++)
                enabledLogs.Remove(logTypes[i]);
        }

        public static void Log(object message, object? context = null)
        {
            LogInternal(message, context, LogType.Log);
        }

        public static void Warning(object message, object? context = null)
        {
            LogInternal(message, context, LogType.Warning);
        }

        public static void Error(object message, object? context = null)
        {
            LogInternal(message, context, LogType.Error);
        }

        public static void Exception(Exception exception, object? context = null)
        {
            LogInternal(exception, context, LogType.Exception);
        }

        public static void Assert(bool condition, object message, object? context = null)
        {
            AssertInternal(condition, message, context, LogType.Error);
        }

        public static void AssertWarning(bool condition, object message, object? context = null)
        {
            AssertInternal(condition, message, context, LogType.Warning);
        }

        private static void LogInternal(object message, object? context, LogType logType)
        {
            if (!enabledLogs.Contains(logType))
                return;

            switch (logType)
            {
                case LogType.Error:
                    Debug.LogError(message, context as Object);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(message, context as Object);
                    break;
                case LogType.Log:
                    Debug.Log(message, context as Object);
                    break;
                case LogType.Exception:
                    Debug.LogException((Exception)message, context as Object);
                    break;
                default:
                    break;
            }
        }

        private static void AssertInternal(bool condition,
                                           object message,
                                           object? context,
                                           LogType logType)
        {
            if (!condition)
                return;

            LogInternal(message, context, logType);
        }
    }
}
