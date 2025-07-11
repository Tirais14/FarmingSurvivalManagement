using System;
using UnityEngine;
using UTIRLib.Patterns.Composite;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Zenject
{
    public interface ICompositeDiContainer : IComposite<DiContainer>
    {
        object[] TryResolve(Type contractType);
        object[] TryResolve<TContract>();

        object[] Resolve(Type contractType);
        object[] Resolve<TContract>();

        T InstantiatePrefab<T>(T prefab) where T : Object;
        T InstantiatePrefab<T>(T prefab, Transform parent) where T : Object;
        T InstantiatePrefab<T>(T prefab, Vector3 position, Quaternion rotation, Transform parentTransform) where T : Object;
    }
}
