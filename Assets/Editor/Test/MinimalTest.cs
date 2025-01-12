using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;

namespace MinimalUtility.Test
{
    using WebRequest;
    using TestUtils = MinimalUtility.Editor.TestUtils;

    public class MinimalTest
    {
        [UnityTest, Order(0), Timeout(300000)]
        public IEnumerator WebRequestTest()
        {
#if ENABLE_UNITASK
            return Cysharp.Threading.Tasks.UniTask.ToCoroutine(async () =>
            {
                const string uri = "https://httpbin.org/post";

                using var client = new HttpClient(new UnityWebRequestHttpMessageHandler());
                using var request = new HttpRequestMessage(HttpMethod.Post, uri)
                {
                    Content = new ByteArrayContent(new byte[] {1, 2, 3, 4, 5})
                    {
                        Headers =
                        {
                            ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json")
                        }
                    }
                };
                using var response = await client.SendAsync(request);
                await TestContext.Out.WriteLineAsync(await response.Content.ReadAsStringAsync());
            });
#else
            yield break;
#endif
        }


        [UnityTest, Order(1), Timeout(300000)]
        public IEnumerator UniTaskPackageRemoveTest()
        {
            yield return PackageRemoveTest("com.cysharp.unitask");
            Assert.Pass();
        }

        [UnityTest, Order(2), Timeout(300000)]
        public IEnumerator R3PackageRemoveTest()
        {
            yield return PackageRemoveTest("com.cysharp.r3");
            Assert.Pass();
        }

        [UnityTest, Order(3), Timeout(300000)]
        public IEnumerator VContainerPackageRemoveTest()
        {
            yield return PackageRemoveTest("jp.hadashikick.vcontainer");
            Assert.Pass();
        }

        [UnityTest, Order(4), Timeout(300000)]
        public IEnumerator UGUIPackageRemoveTest()
        {
            yield return PackageRemoveTest("com.unity.ugui");
            Assert.Pass();
        }

        [Test, Order(5)]
        public void CompileCheckTest()
        {
            var result = TestUtils.SuccessCompile(UnityEditor.BuildTarget.StandaloneOSX);
            Assert.IsTrue(result);
        }

        [Test, Order(6)]
        public void XEnumTest()
        {
            var fruits = Fruits.Apple | Fruits.Banana | Fruits.Orange;
            foreach (var fruit in fruits.AsFlags())
            {
                TestContext.Out.WriteLine(XEnum.GetName(fruit));
            }
            Assert.Pass();
        }

        [Test, Order(7), Performance]
        public void ListSpanTest()
        {
            var list = new List<int> {1, 2, 3, 4, 5};
            list.AsSpan()[1] = -555;
            foreach (var i in list.AsSpan())
            {
                TestContext.Out.WriteLine(i.ToString());
            }
            Assert.Pass();
        }

        private static IEnumerator PackageRemoveTest(string packageName)
        {
            var request = UnityEditor.PackageManager.Client.Remove(packageName);
            while (!request.IsCompleted)
            {
                yield return null;
            }
            if (request.Status != UnityEditor.PackageManager.StatusCode.Success)
            {
                throw new System.Exception("Failed to remove package: " + packageName);
            }
        }

        [Flags]
        public enum Fruits
        {
            Apple = 1 << 0,
            Banana = 1 << 1,
            Orange = 1 << 2,
        }
    }
}