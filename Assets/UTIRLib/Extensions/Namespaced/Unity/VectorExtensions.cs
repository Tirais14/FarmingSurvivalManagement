using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEngine;

#nullable enable

namespace UTIRLib.UExtensions
{
    [SuppressMessage("Minor Code Smell", "S4136:Method overloads should be grouped together", Justification = "<Pending>")]
    public static class VectorExtensions
    {
        #region Vector2

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int FloorToVector2Int(this Vector2 vector)
        {
            return new(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y));
        }

        /// <exception cref="ArgumentNullException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 With(this ref Vector2 value, Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            value.Set(transform.position.x, transform.position.y);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToVector3(this Vector2 value) => new(value.x, value.y);

        #endregion Vector2

        #region Vector2Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ToVector3Int(this Vector2Int vector2Int) => new(vector2Int.x, vector2Int.y);

        #endregion Vector2Int

        #region Vector3

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int FloorToVector2Int(this Vector3 vector)
        {
            return new(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int FloorToVector3Int(this Vector3 vector)
        {
            return new(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y), Mathf.FloorToInt(vector.z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ToVector2Int(this Vector3 vector)
        {
            return new(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
        }

        /// <exception cref="ArgumentNullException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void With(this ref Vector3 vector, Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            Vector3 newPos = transform.position;
            vector.Set(newPos.x, newPos.y, newPos.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetDirectionRelative(this Vector3 position, Vector3 target)
        {
            return (target - position).normalized;
        }

        #endregion Vector3

        #region Vector3Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ToVector2Int(this Vector3Int vector3Int)
        {
            return new(vector3Int.x, vector3Int.y);
        }

        #endregion Vector3Int
    }
}