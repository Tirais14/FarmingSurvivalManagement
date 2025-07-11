using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UTIRLib.Diagnostics;
using UTIRLib.Extensions;

#nullable enable

namespace UTIRLib.TickerX
{
    public abstract class Ticker : MonoX,
        ITicker<ITickable>,
        ITicker<IFixedTickable>,
        ITicker<ILateTickable>,
        ITicker<ITickable<float>>,
        ITicker<ILateTickable<float>>
    {
        [Flags]
        private enum EnabledTickables
        {
            Tickables = 1,
            FixedTickables = 2,
            LateTickables = 4,
            TickablesFloat = 8,
            LateTickablesFloat = 16
        }

        protected const int MAX_TICKS_PER_FRAME_COUNT = 500;
        protected const int DEFAULT_TICKABLES_ARRAY_CAPACITY = 16;
        protected const int TICKABLES_ARRAY_RESIZER_MULTIPLIER_DECREASE_THRESHOLD = 200;
        public const float MIN_TIME_SPEED = 0.1f;
        public const float MAX_TIME_SPEED = 100f;

        private EnabledTickables enabledTickables;
        protected readonly List<ITickable> tickables = new();
        protected readonly List<IFixedTickable> fixedTickables = new();
        protected readonly List<ILateTickable> lateTickables = new();
        protected readonly List<ITickable<float>> tickablesFloat = new();
        protected readonly List<ILateTickable<float>> lateTickablesFloat = new();
        protected int tickablesCount;
        protected int fixedTickablesCount;
        protected int lateTickablesCount;
        protected int tickablesFloatCount;
        protected int lateTickablesFloatCount;

        protected int ticksPerFrameCount;
        protected float passedTimeValue;
        [SerializeField] protected float timeSpeed = 1f;

        public float TimeSpeed {
            get => timeSpeed;
            set => SetTimeSpeed(value);
        }

        public int Count => tickablesCount + fixedTickablesCount + lateTickablesCount + tickablesFloatCount
            + lateTickablesFloatCount;

        /// <exception cref="ArgumentNullException"></exception>
        public virtual void Register(ITickableBase tickableBase)
        {
            if (tickableBase.IsNull())
            {
                throw new ArgumentNullException(nameof(tickableBase));
            }

            if (tickableBase is ITickable tickable)
            {
                tickables.Add(tickable);
            }
            else if (tickableBase is IFixedTickable fixedTickable)
            {
                fixedTickables.Add(fixedTickable);
            }
            else if (tickableBase is ILateTickable lateTickable)
            {
                lateTickables.Add(lateTickable);
            }
            else if (tickableBase is ITickable<float> tickableFloat)
            {
                tickablesFloat.Add(tickableFloat);
            }
            else if (tickableBase is ILateTickable<float> lateTickableFloat)
            {
                lateTickablesFloat.Add(lateTickableFloat);
            }

            UpdateTickablesInfo();
        }

        public void Register(ITickable tickable) => Register((ITickableBase)tickable);

        public void Register(IFixedTickable tickable) => Register((ITickableBase)tickable);

        public void Register(ILateTickable tickable) => Register((ITickableBase)tickable);

        public void Register(ITickable<float> tickable) => Register((ITickableBase)tickable);

        public void Register(ILateTickable<float> tickable) => Register((ITickableBase)tickable);

        public virtual bool Unregister(ITickableBase? tickableBase)
        {
            if (tickableBase.IsNull())
            {
                return false;
            }

            bool result = false;
            if (tickableBase is ITickable tickable)
            {
                result = tickables.Remove(tickable);
            }
            else if (tickableBase is IFixedTickable fixedTickable)
            {
                result = fixedTickables.Remove(fixedTickable);
            }
            else if (tickableBase is ILateTickable lateTickable)
            {
                result = lateTickables.Remove(lateTickable);
            }
            else if (tickableBase is ITickable<float> tickableFloat)
            {
                result = tickablesFloat.Remove(tickableFloat);
            }
            else if (tickableBase is ILateTickable lateTickableFloat)
            {
                result = lateTickables.Remove(lateTickableFloat);
            }

            UpdateTickablesInfo();
            return result;
        }

        public bool Unregister(ITickable? tickable) => Unregister((ITickableBase?)tickable);

        public bool Unregister(IFixedTickable? tickable) => Unregister((ITickableBase?)tickable);

        public bool Unregister(ILateTickable? tickable) => Unregister((ITickableBase?)tickable);

        public bool Unregister(ITickable<float>? tickable) => Unregister((ITickableBase?)tickable);

        public bool Unregister(ILateTickable<float>? tickable) => Unregister((ITickableBase?)tickable);

        public bool CanRegister(Type type)
        {
            if (!TickableHelper.IsTickable(type))
            {
                return false;
            }

            Type tickableBaseInterfaceType = typeof(ITickableBase);

            return tickableBaseInterfaceType.IsAny(typeof(ITickable), typeof(IFixedTickable),
                typeof(ILateTickable), typeof(ITickable<float>), typeof(ILateTickable<float>));
        }

        public bool CanRegister<T>() => CanRegister(typeof(T));

        public bool CanRegister(ITickableBase tickableBase) => CanRegister(tickableBase.GetType());

        public bool Contains(ITickableBase? tickableBase) =>
            tickableBase switch {
                ITickable => tickables.Contains(tickableBase),
                IFixedTickable => fixedTickables.Contains(tickableBase),
                ILateTickable => lateTickables.Contains(tickableBase),
                ITickable<float> => tickablesFloat.Contains(tickableBase),
                ILateTickable<float> => lateTickablesFloat.Contains(tickableBase),
                _ => false,
            };

        public void UnregisterAll()
        {
            tickables.Clear();
            fixedTickables.Clear();
            lateTickables.Clear();
            tickablesFloat.Clear();
            lateTickablesFloat.Clear();
        }

        public virtual void ResetTimeSpeed()
        {
            timeSpeed = 1;
            passedTimeValue = 0;
        }

        public virtual void SetTimeSpeed(float value)
        {
            if (value > MAX_TIME_SPEED)
            {
                timeSpeed = MAX_TIME_SPEED;
            }
            else if (value < MIN_TIME_SPEED)
            {
                timeSpeed = MIN_TIME_SPEED;
            }
            else
            {
                timeSpeed = value;
            }
        }

        protected virtual void IncreaseTickCounter() => ticksPerFrameCount++;

        protected virtual void ResetTickCounter() => ticksPerFrameCount = 0;

        protected virtual void IncreasePassedTime() => passedTimeValue += Time.deltaTime * timeSpeed;

        protected virtual void ResetPassedTime() => passedTimeValue = 0;

        protected virtual bool IsEndlessTick() =>
            ticksPerFrameCount >= MAX_TICKS_PER_FRAME_COUNT;

        protected virtual bool TickAllowed()
        {
            if (passedTimeValue >= Time.deltaTime)
            {
                return true;
            }

            return false;
        }

        protected void BeginTick()
        {
            ResetTickCounter();
            IncreasePassedTime();
        }

        protected void EndTick()
        {
            ResetPassedTime();
            IncreaseTickCounter();
        }

        protected void UpdateTickablesInfo()
        {
            tickablesCount = tickables.Count;
            fixedTickablesCount = fixedTickables.Count;
            lateTickablesCount = lateTickables.Count;
            tickablesFloatCount = tickablesFloat.Count;
            lateTickablesFloatCount = lateTickablesFloat.Count;

            if (tickables.Count > 0)
            {
                enabledTickables |= EnabledTickables.Tickables;
            }
            else
            {
                enabledTickables &= ~EnabledTickables.Tickables;
            }
            if (fixedTickables.Count > 0)
            {
                enabledTickables |= EnabledTickables.FixedTickables;
            }
            else
            {
                enabledTickables &= ~EnabledTickables.FixedTickables;
            }
            if (lateTickables.Count > 0)
            {
                enabledTickables |= EnabledTickables.LateTickables;
            }
            else
            {
                enabledTickables &= ~EnabledTickables.LateTickables;
            }
            if (tickablesFloat.Count > 0)
            {
                enabledTickables |= EnabledTickables.TickablesFloat;
            }
            else
            {
                enabledTickables &= ~EnabledTickables.TickablesFloat;
            }
            if (lateTickablesFloat.Count > 0)
            {
                enabledTickables |= EnabledTickables.LateTickablesFloat;
            }
            else
            {
                enabledTickables &= ~EnabledTickables.LateTickablesFloat;
            }
        }

        protected virtual void Tick()
        {
            if (!IsTickableEnabled(EnabledTickables.Tickables) &&
                !IsTickableEnabled(EnabledTickables.TickablesFloat))
            {
                return;
            }

            ResetTickCounter();
            IncreasePassedTime();

            LoopPredicate cyclePredicate = new(TickAllowed);
            while (cyclePredicate.Invoke())
            {
                for (int i = 0; i < tickablesCount; i++)
                {
                    tickables[i].OnTick();
                }
                for (int i = 0; i < tickablesFloatCount; i++)
                {
                    tickablesFloat[i].OnTick(passedTimeValue);
                }
                ResetPassedTime();
                IncreaseTickCounter();
            }
        }

        protected virtual void FixedTick()
        {
            if (!IsTickableEnabled(EnabledTickables.FixedTickables))
            {
                return;
            }

            ResetTickCounter();
            IncreasePassedTime();

            LoopPredicate cyclePredicate = new(TickAllowed);
            while (cyclePredicate.Invoke())
            {
                for (int i = 0; i < tickablesCount; i++)
                {
                    fixedTickables[i].OnFixedTick();
                }
                ResetPassedTime();
                IncreaseTickCounter();
            }
        }

        protected virtual void LateTick()
        {
            if (!IsTickableEnabled(EnabledTickables.LateTickables) &&
                !IsTickableEnabled(EnabledTickables.LateTickablesFloat))
            {
                return;
            }

            ResetTickCounter();
            IncreasePassedTime();

            LoopPredicate cyclePredicate = new(TickAllowed);
            while (cyclePredicate.Invoke())
            {
                for (int i = 0; i < tickablesCount; i++)
                {
                    lateTickables[i].OnLateTick();
                }
                for (int i = 0; i < tickablesFloatCount; i++)
                {
                    lateTickablesFloat[i].OnLateTick(passedTimeValue);
                }
                ResetPassedTime();
                IncreaseTickCounter();
            }
        }

        private bool IsTickableEnabled(EnabledTickables tickable) => (enabledTickables & tickable) == tickable;
    }
}