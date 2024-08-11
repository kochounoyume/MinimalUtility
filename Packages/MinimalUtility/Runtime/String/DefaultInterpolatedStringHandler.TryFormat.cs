#if !NET6_0_OR_GREATER
#nullable enable

namespace System.Runtime.CompilerServices
{
    public ref partial struct DefaultInterpolatedStringHandler
    {
        public void AppendFormatted(bool value)
        {
            // If there's a custom formatter, always use it.
            if (_hasCustomFormatter)
            {
                AppendCustomFormatter(value, format: null);
                return;
            }

            int charsWritten;
            while (!value.TryFormat(_chars.Slice(_pos), out charsWritten)) // constrained call avoiding boxing for value types
            {
                Grow();
            }

            _pos += charsWritten;
        }

        public void AppendFormatted(bool value, int alignment)
        {
            int startingPos = _pos;
            AppendFormatted(value);
            if (alignment != 0)
            {
                AppendOrInsertAlignmentIfNeeded(startingPos, alignment);
            }
        }

        public void AppendFormatted(bool value, string? _)
        {
            // If there's a custom formatter, always use it.
            if (_hasCustomFormatter)
            {
                AppendCustomFormatter(value, format: null);
                return;
            }

            int charsWritten;
            while (!value.TryFormat(_chars.Slice(_pos), out charsWritten)) // constrained call avoiding boxing for value types
            {
                Grow();
            }

            _pos += charsWritten;
        }

        public void AppendFormatted(bool value, int alignment, string? format)
        {
            int startingPos = _pos;
            AppendFormatted(value, format);
            if (alignment != 0)
            {
                AppendOrInsertAlignmentIfNeeded(startingPos, alignment);
            }
        }

        public void AppendFormatted(Guid value)
        {
            // If there's a custom formatter, always use it.
            if (_hasCustomFormatter)
            {
                AppendCustomFormatter(value, format: null);
                return;
            }

            int charsWritten;
            while (!value.TryFormat(_chars.Slice(_pos), out charsWritten, null)) // constrained call avoiding boxing for value types
            {
                Grow();
            }

            _pos += charsWritten;
        }

        public void AppendFormatted(Guid value, int alignment)
        {
            int startingPos = _pos;
            AppendFormatted(value);
            if (alignment != 0)
            {
                AppendOrInsertAlignmentIfNeeded(startingPos, alignment);
            }
        }

        public void AppendFormatted(Guid value, string? format)
        {
            // If there's a custom formatter, always use it.
            if (_hasCustomFormatter)
            {
                AppendCustomFormatter(value, format);
                return;
            }

            int charsWritten;
            while (!value.TryFormat(_chars.Slice(_pos), out charsWritten, format)) // constrained call avoiding boxing for value types
            {
                Grow();
            }

            _pos += charsWritten;
        }

        public void AppendFormatted(Guid value, int alignment, string? format)
        {
            int startingPos = _pos;
            AppendFormatted(value, format);
            if (alignment != 0)
            {
                AppendOrInsertAlignmentIfNeeded(startingPos, alignment);
            }
        }
    }
}

#endif