// See https://aka.ms/new-console-template for more information

using MinimalUtility;

Console.WriteLine("Hello, World!");
Fruits fruits = Fruits.Apple | Fruits.Banana | Fruits.Orange;

// Console.WriteLine(Fruits.Apple.ToXEnumString());
foreach (var f in fruits.AsFlags())
{
    // var str = XEnum.GetName(f);
    // Console.WriteLine(str);
    Console.WriteLine(f);
}

[Flags]
public enum Fruits
{
    Apple = 1 << 0,
    Banana = 1 << 1,
    Orange = 1 << 2,
}

namespace MinimalUtility
{
    public class ForEachAttribute : Attribute
    {
        public ForEachAttribute() { }
    }
}