using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Editor.Test
{
    public abstract class PackageTestBase
    {
        protected abstract string packageName { get; }

        public virtual IEnumerator PackageRemoveTest()
        {
            var request = UnityEditor.PackageManager.Client.Remove(packageName);
            while (!request.IsCompleted)
            {
                yield return null;
            }
            Assert.IsTrue(request.Status == UnityEditor.PackageManager.StatusCode.Success);
        }

        public virtual void CompileCheckTest()
        {
            var result = MinimalUtility.Editor.TestUtils.SuccessCompile(UnityEditor.BuildTarget.StandaloneOSX);
            Assert.IsTrue(result);
        }
    }

    public class UniTaskPackageTest : PackageTestBase
    {
        protected override string packageName => "com.cysharp.unitask";

        [UnityTest, Order(0), Timeout(300000)]
        public override IEnumerator PackageRemoveTest()
        {
            return base.PackageRemoveTest();
        }

        [Test, Order(1)]
        public override void CompileCheckTest()
        {
            base.CompileCheckTest();
        }
    }

    public class R3PackageTest : PackageTestBase
    {
        protected override string packageName => "com.cysharp.r3";

        [UnityTest, Order(0), Timeout(300000)]
        public override IEnumerator PackageRemoveTest()
        {
            return base.PackageRemoveTest();
        }

        [Test, Order(1)]
        public override void CompileCheckTest()
        {
            base.CompileCheckTest();
        }
    }

    public class VContainerPackageTest : PackageTestBase
    {
        protected override string packageName => "jp.hadashikick.vcontainer";

        [UnityTest, Order(0), Timeout(300000)]
        public override IEnumerator PackageRemoveTest()
        {
            return base.PackageRemoveTest();
        }

        [Test, Order(1)]
        public override void CompileCheckTest()
        {
            base.CompileCheckTest();
        }
    }

    public class UGUIPackageTest : PackageTestBase
    {
        protected override string packageName => "com.unity.ugui";

        [UnityTest, Order(0), Timeout(300000)]
        public override IEnumerator PackageRemoveTest()
        {
            return base.PackageRemoveTest();
        }

        [Test, Order(1)]
        public override void CompileCheckTest()
        {
            base.CompileCheckTest();
        }
    }

    public class TMPPackageTest : PackageTestBase
    {
        protected override string packageName => "com.unity.textmeshpro";

        [UnityTest, Order(0), Timeout(300000)]
        public override IEnumerator PackageRemoveTest()
        {
            return base.PackageRemoveTest();
        }

        [Test, Order(1)]
        public override void CompileCheckTest()
        {
            base.CompileCheckTest();
        }
    }
}