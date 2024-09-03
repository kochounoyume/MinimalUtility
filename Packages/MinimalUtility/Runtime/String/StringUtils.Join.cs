#nullable enable

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MinimalUtility.String
{
    public static partial class StringUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join(string? separator, IEnumerable<string?> values) => Join(separator.AsSpan(), values);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join(char separator, IEnumerable<string?> values)
        {
            char[] separatorArray = ArrayPool<char>.Shared.Rent(1);
            separatorArray[0] = separator;
            string result = Join(new ReadOnlySpan<char>(separatorArray), values);
            ArrayPool<char>.Shared.Return(separatorArray);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join(ReadOnlySpan<char> separator, IEnumerable<string?> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            using (IEnumerator<string?> en = values.GetEnumerator())
            {
                if (!en.MoveNext())
                {
                    return "";
                }

                string? firstValue = en.Current;

                if (!en.MoveNext())
                {
                    return firstValue ?? "";
                }

                DefaultInterpolatedStringHandler result = new ();

                if (firstValue != null)
                {
                    result.AppendLiteral(firstValue);
                }

                do
                {
                    result.AppendFormatted(separator);
                    if (en.Current != null)
                    {
                        result.AppendLiteral(en.Current);
                    }
                }
                while (en.MoveNext());

                return result.ToString();
            }
        }
    }
}