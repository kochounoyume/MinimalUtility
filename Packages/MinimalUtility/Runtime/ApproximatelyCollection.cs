using System;
using System.Collections.Generic;

namespace MinimalUtility
{
    #pragma warning disable SA1402 // FileMayOnlyContainASingleType
    /// <content>
    /// <see cref="Dictionary{TKey,TValue}"/>の拡張メソッド.
    /// </content>
    public static partial class DictionaryExtensions
    {
        /// <summary>
        /// <see cref="Dictionary{TKey,TValue}"/>を<see cref="ApproximatelyCollection{TValue}"/>に変換します.
        /// </summary>
        /// <param name="dictionary">変換元の<see cref="Dictionary{TKey,TValue}"/>.</param>
        /// <param name="buffer">近似値を算出する値の範囲.</param>
        /// <typeparam name="TValue">値の型.</typeparam>
        /// <returns>変換後の<see cref="ApproximatelyCollection{TValue}"/>.</returns>
        public static ApproximatelyCollection<TValue> ToProximityCollection<TValue>(this Dictionary<float, TValue> dictionary, in int buffer = 3)
        {
            var result = new ApproximatelyCollection<TValue>(dictionary.Count, buffer);
            foreach (var pair in dictionary)
            {
                result.Add(pair);
            }
            return result;
        }
    }

    /// <summary>
    /// 近似値を算出するためのコレクション.
    /// </summary>
    /// <typeparam name="TValue">値の型.</typeparam>
    public sealed class ApproximatelyCollection<TValue> :
        IEnumerable<KeyValuePair<float, TValue>>,
        IReadOnlyCollection<KeyValuePair<float, TValue>>,
        IReadOnlyList<KeyValuePair<float, TValue>>
    {
        private readonly Comparer<KeyValuePair<float, TValue>> comparer
            = Comparer<KeyValuePair<float, TValue>>.Create(static (x, y) =>
            {
                if (Math.Approximately(x.Key, x.Key)) return 0;
                return (double)x.Key < (double)y.Key ? -1 : 1;
            });

        private readonly KeyValuePair<float, TValue>[] pairs;

        private readonly int capacity;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApproximatelyCollection{TValue}"/> class.
        /// </summary>
        /// <param name="capacity">コレクションの要素制限（後で変更する気はない）.</param>
        /// <param name="reach">近似値を算出する値の範囲.</param>
        public ApproximatelyCollection(in int capacity, in float reach) : this(capacity)
        {
            this.Reach = reach;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApproximatelyCollection{TValue}"/> class.
        /// </summary>
        /// <param name="capacity">コレクションの要素制限（後で変更する気はない）.</param>
        public ApproximatelyCollection(in int capacity)
        {
            this.pairs = new KeyValuePair<float, TValue>[capacity];
            this.capacity = capacity;
        }

        /// <inheritdoc/>
        public int Count { get; private set; }

        /// <summary>
        /// 近似値を算出する値の範囲.
        /// </summary>
        public float Reach { get; set; }

        /// <inheritdoc/>
        public KeyValuePair<float, TValue> this[int index] => pairs[index];

        /// <summary>
        /// インデクサ.
        /// </summary>
        /// <param name="key">キー.</param>
        public TValue[] this[float key]
        {
            get
            {
                var result = new List<TValue>();
                var pairsSpan = pairs.AsSpan();

                // key < (最小値)
                if (key < pairsSpan[0].Key)
                {
                    int minI = ~pairsSpan[..Count].BinarySearch(new KeyValuePair<float, TValue>(Reach, default), comparer);
                    foreach (var (_, value) in pairsSpan[..minI])
                    {
                        result.Add(value);
                    }
                    return result.ToArray();
                }

                // key > (最大値)
                if (key + Reach > pairsSpan[Count - 1].Key)
                {
                    float reachKey = pairsSpan[Count - 1].Key - Reach;
                    int maxI = ~pairsSpan[..Count].BinarySearch(new KeyValuePair<float, TValue>(reachKey, default), comparer);
                    foreach (var (_, value) in pairsSpan[maxI..Count])
                    {
                        result.Add(value);
                    }
                    return result.ToArray();
                }

                int beginI = ~pairsSpan[..Count].BinarySearch(new KeyValuePair<float, TValue>(key, default), comparer);
                int endI = ~pairsSpan[..Count].BinarySearch(new KeyValuePair<float, TValue>(key + Reach, default), comparer);
                foreach (var (_, value) in pairsSpan[beginI..endI])
                {
                    result.Add(value);
                }
                return result.ToArray();
            }
        }

        public static explicit operator Dictionary<float, TValue>(ApproximatelyCollection<TValue> collection) => collection.ToDictionary();

        public static explicit operator ReadOnlySpan<KeyValuePair<float, TValue>>(ApproximatelyCollection<TValue> collection) => collection.AsReadOnlySpan();

        /// <summary>
        /// 要素を追加します.
        /// </summary>
        /// <param name="key">実際に近似値を算出するために使用される値.</param>
        /// <param name="value">追加する値.</param>
        public void Add(in float key, TValue value) => Add(key, ref value);

        /// <summary>
        /// 要素を追加します.
        /// </summary>
        /// <param name="key">実際に近似値を算出するために使用される値.</param>
        /// <param name="value">追加する値.</param>
        public void Add(in float key, ref TValue value) => Add(new KeyValuePair<float, TValue>(key, value));

        /// <summary>
        /// 要素を追加します.
        /// </summary>
        /// <param name="pair">追加する要素.</param>
        public void Add(in KeyValuePair<float, TValue> pair)
        {
            if (Count == capacity)
            {
                const string message = "コレクションの要素数が上限に達しています";
                throw new InvalidOperationException(message);
            }
            Count++;

            var pairsSpan = pairs.AsSpan();

            // key が最小値より小さい場合
            if (pair.Key.CompareTo(pairsSpan[0].Key) < 0)
            {
                pairsSpan[..Count].CopyTo(pairsSpan[1..]);
                pairsSpan[0] = pair;
                return;
            }

            // key が最大値より大きい場合
            if (pair.Key.CompareTo(pairsSpan[Count - 1].Key) > 0)
            {
                pairsSpan[Count - 1] = pair;
                return;
            }

            int index = ~pairsSpan.BinarySearch(pair, comparer);

            pairsSpan[index..(Count - index)].CopyTo(pairsSpan[(index + 1)..]);
            pairsSpan[index] = pair;
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<float, TValue>> GetEnumerator()
        {
            foreach (var pair in pairs.AsSpan()[..Count].ToArray())
            {
                yield return pair;
            }
        }

        /// <inheritdoc/>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// <see cref="Dictionary{TKey,TValue}"/>に変換します.
        /// </summary>
        /// <returns>変換後の<see cref="Dictionary{TKey,TValue}"/>.</returns>
        public Dictionary<float, TValue> ToDictionary()
        {
            var result = new Dictionary<float, TValue>(Count);
            var pairsSpan = pairs.AsSpan();
            foreach (var (key, value) in pairsSpan[..Count])
            {
                result.Add(key, value);
            }
            return result;
        }

        /// <summary>
        /// <see cref="ReadOnlySpan{T}"/>に変換します.
        /// </summary>
        /// <returns>変換後の<see cref="ReadOnlySpan{T}"/>.</returns>
        public ReadOnlySpan<KeyValuePair<float, TValue>> AsReadOnlySpan() => new ReadOnlySpan<KeyValuePair<float, TValue>>(pairs)[..Count];
    }
}