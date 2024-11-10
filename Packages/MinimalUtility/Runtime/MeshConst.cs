using System.Runtime.CompilerServices;
using UnityEngine;

namespace MinimalUtility
{
    /// <summary>
    /// プログラムで扱う頻度が高そうな<see cref="Mesh"/>の定数群.
    /// </summary>
    public static class MeshConst
    {
        /// <summary>
        /// プリミティブキューブ.
        /// </summary>
        public static Mesh Cube
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Mesh()
                {
                    vertices = new[]
                    {
                        new Vector3(0.5f, -0.5f, 0.5f), new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f),
                        new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, -0.5f), new Vector3(-0.5f, 0.5f, -0.5f),
                        new Vector3(0.5f, -0.5f, -0.5f), new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.5f, 0.5f, 0.5f),
                        new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, -0.5f), new Vector3(-0.5f, 0.5f, -0.5f),
                        new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0.5f, -0.5f, 0.5f), new Vector3(-0.5f, -0.5f, 0.5f),
                        new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(-0.5f, 0.5f, 0.5f),
                        new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.5f, -0.5f, -0.5f),
                        new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, -0.5f, 0.5f)
                    },
                    triangles = new[]
                    {
                        0, 2, 3, 0, 3, 1,
                        8, 4, 5, 8, 5, 9,
                        10, 6, 7, 10, 7, 11,
                        12, 13, 14, 12, 14, 15,
                        16, 17, 18, 16, 18, 19,
                        20, 21, 22, 20, 22, 23
                    },
                    normals = new[]
                    {
                        Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward,
                        Vector3.up, Vector3.up, Vector3.back, Vector3.back,
                        Vector3.up, Vector3.up, Vector3.back, Vector3.back,
                        Vector3.down, Vector3.down, Vector3.down, Vector3.down,
                        Vector3.left, Vector3.left, Vector3.left, Vector3.left,
                        Vector3.right, Vector3.right, Vector3.right, Vector3.right
                    },
                    uv = new[]
                    {
                        Vector2.zero, Vector2.right, Vector2.up, Vector2.one, Vector2.up, Vector2.one,
                        Vector2.up, Vector2.one, Vector2.zero, Vector2.right, Vector2.zero, Vector2.right,
                        Vector2.zero, Vector2.up, Vector2.one, Vector2.right, Vector2.zero, Vector2.up,
                        Vector2.one, Vector2.right, Vector2.zero, Vector2.up, Vector2.one, Vector2.right
                    },
                };
            }
        }
    }
}