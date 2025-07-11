using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UTIRLib.Diagnostics;
using UTIRLib.Patterns.Composite;
using UTIRLib.Zenject;
using Zenject;
using Object = UnityEngine.Object;

#nullable enable
namespace Game.Zenject
{
    [Obsolete]
    public class CompositeDiContainer : Composite<DiContainer>
    {
        protected readonly List<object> dependencies = new();
        protected DiContainer FirstContainer => childs[0];
        public bool IsSingleContaier => Count == 1;

        public CompositeDiContainer(params DiContainer[] containers) : base(containers)
        { }

        public void Inject(object target)
        {
            if (target.IsNull()) {
                throw new ArgumentNullException(nameof(target));
            }

            if (IsSingleContaier) {
                FirstContainer.Inject(target);
            }
            else {
                //DiContainerHelper.InjectDependencies(target, childs);
            }
        }

        public object[] TryResolve(Type contractType)
        {
            dependencies.Clear();
            DiContainerHelper.TryResolveDependencies<MemberInfo>(contractType, childs, dependencies);

            return dependencies.ToArray();
        }
        public object[] TryResolve<TContract>() => TryResolve(typeof(TContract));

        public object[] Resolve(Type contractType)
        {
            dependencies.Clear();
            DiContainerHelper.ResolveDependencies(contractType, childs, dependencies);

            return dependencies.ToArray();
        }
        public object[] Resolve<TContract>() => Resolve(typeof(TContract));

        public T InstantiatePrefab<T>(T prefab) where T : Object =>
            InstantiatePrefabInternal(prefab, position: null, rotation: null, parentTransform: null);
        public T InstantiatePrefab<T>(T prefab, Transform parent) where T : Object =>
            InstantiatePrefabInternal(prefab, position: null, rotation: null, parent);
        public T InstantiatePrefab<T>(T prefab, Vector3 position, Quaternion rotation, Transform parentTransform)
            where T : Object => InstantiatePrefabInternal(prefab, position, rotation, parentTransform);

        private T InstantiatePrefabInternal<T>(T prefab, Vector3? position, Quaternion? rotation,
            Transform? parentTransform)
            where T : Object
        {
            T result;
            if (position.HasValue && rotation.HasValue && parentTransform != null) {
                result = Object.Instantiate(prefab, position.Value, rotation.Value, parentTransform);
            }
            else if (parentTransform != null) {
                result = Object.Instantiate(prefab, parentTransform);
            }
            else {
                result = Object.Instantiate(prefab);
            }

            Inject(result);
            return result;
        }
    }
}
