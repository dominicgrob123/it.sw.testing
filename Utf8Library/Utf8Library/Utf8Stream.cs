using System.Security.Cryptography;

namespace Utf8Library
{
    public class Utf8Stream : Stream
    {
        private readonly Stream _stream;

        public Utf8Stream(Stream stream)
        {
            _stream = stream;
        }

        /// <summary>
        /// Writes the integer passed through <paramref name="value"/> in UTF-8 
        /// encoding to the stream.
        /// </summary>
        /// <param name="value">Integer to be written</param>
        public void Write(int value)
        {
            // Process each digit of the integer directly
            do
            {
                int digit = value % 10;          // Get the last digit
                value /= 10;                     // Remove the last digit

                int codePoint = 0x30 + digit;    // Convert digit to Unicode code point (U+0030 to U+0039)

                // Encode the code point in UTF-8 (single byte for ASCII range)
                if (codePoint <= 0x7F)
                {
                    _stream.WriteByte((byte)codePoint);
                }
                else
                {
                    // For completeness, handle multi-byte sequences if needed
                    // (not necessary for digits 0-9, but included for demonstration)
                    if (codePoint <= 0x7FF)
                    {
                        _stream.WriteByte((byte)(0xC0 | ((codePoint >> 6) & 0x1F)));
                        _stream.WriteByte((byte)(0x80 | (codePoint & 0x3F)));
                    }
                    else if (codePoint <= 0xFFFF)
                    {
                        _stream.WriteByte((byte)(0xE0 | ((codePoint >> 12) & 0x0F)));
                        _stream.WriteByte((byte)(0x80 | ((codePoint >> 6) & 0x3F)));
                        _stream.WriteByte((byte)(0x80 | (codePoint & 0x3F)));
                    }
                    else
                    {
                        _stream.WriteByte((byte)(0xF0 | ((codePoint >> 18) & 0x07)));
                        _stream.WriteByte((byte)(0x80 | ((codePoint >> 12) & 0x3F)));
                        _stream.WriteByte((byte)(0x80 | ((codePoint >> 6) & 0x3F)));
                        _stream.WriteByte((byte)(0x80 | (codePoint & 0x3F)));
                    }
                }
            } while (value > 0);
        }

        /// <summary>
        /// Writes the character passed through <paramref name="character"/> in 
        /// UTF-8 encoding to the stream.
        /// </summary>
        /// <param name="character">Character to be written</param>
        public void Write(char character)
        {
            int code = character;

            if (code <= 0x7F)
            {
                // 1-byte UTF-8 (ASCII)
                _stream.WriteByte((byte)code);
            }
            else if (code <= 0x7FF)
            {
                // 2-byte UTF-8
                _stream.WriteByte((byte)(0xC0 | ((code >> 6) & 0x1F)));
                _stream.WriteByte((byte)(0x80 | (code & 0x3F)));
            }
            else
            {
                // 3-byte UTF-8
                _stream.WriteByte((byte)(0xE0 | ((code >> 12) & 0x0F)));
                _stream.WriteByte((byte)(0x80 | ((code >> 6) & 0x3F)));
                _stream.WriteByte((byte)(0x80 | (code & 0x3F)));
            }
        }

        /// <summary>
        /// Writes the string passed through <paramref name="str"/> as a sequence 
        /// of UTF-8 encoded characters to the underlying stream.
        /// </summary>
        /// <param name="str">String to be written</param>
        public void Write(string str)
        {
            foreach (char c in str)
            {
                int code = c;

                if (code <= 0x7F)
                {
                    // 1-byte UTF-8
                    _stream.WriteByte((byte)code);
                }
                else if (code <= 0x7FF)
                {
                    // 2-byte UTF-8
                    _stream.WriteByte((byte)(0xC0 | ((code >> 6) & 0x1F)));
                    _stream.WriteByte((byte)(0x80 | (code & 0x3F)));
                }
                else
                {
                    // 3-byte UTF-8
                    _stream.WriteByte((byte)(0xE0 | ((code >> 12) & 0x0F)));
                    _stream.WriteByte((byte)(0x80 | ((code >> 6) & 0x3F)));
                    _stream.WriteByte((byte)(0x80 | (code & 0x3F)));
                }
            }
        }

        /// <summary>
        /// Reads the next UTF-8 encoded integer from the underlying stream.
        /// </summary>
        /// <returns>Value read from the stream</returns>
        /// <exception cref="EndOfStreamException">Nothing more can be read</exception>
        public int ReadInt()
        {
            int result = 0;
            while (true)
            {
                int readByte = _stream.ReadByte();
                if (readByte == -1)
                {
                    if (result == 0)
                    {
                        throw new EndOfStreamException("No data found.");
                    }
                    break; // End of stream, return what was read
                }

                char c = (char)readByte;
                if (c >= '0' && c <= '9')
                {
                    // Update result from digit value
                    result = result * 10 + (c - '0');
                }
                else
                {
                    // Stop reading on first non-digit character
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Reads the next UTF-8 encoded character from the underlying stream.
        /// </summary>
        /// <returns>Value read from the stream</returns>
        /// <exception cref="EndOfStreamException">Nothing more can be read</exception>
        public char ReadChar()
        {
            int firstByte = _stream.ReadByte();
            if (firstByte == -1) throw new EndOfStreamException();

            int codePoint;
            if ((firstByte & 0x80) == 0x00)
            {
                // 1-byte sequence: 0xxxxxxx
                codePoint = firstByte;
            }
            else if ((firstByte & 0xE0) == 0xC0)
            {
                // 2-byte sequence: 110xxxxx 10yyyyyy
                int secondByte = _stream.ReadByte();
                if (secondByte == -1 || (secondByte & 0xC0) != 0x80) throw new EndOfStreamException();
                codePoint = ((firstByte & 0x1F) << 6) | (secondByte & 0x3F);
            }
            else if ((firstByte & 0xF0) == 0xE0)
            {
                // 3-byte sequence: 1110xxxx 10yyyyyy 10zzzzzz
                int secondByte = _stream.ReadByte();
                int thirdByte = _stream.ReadByte();
                if (secondByte == -1 || thirdByte == -1 ||
                    (secondByte & 0xC0) != 0x80 ||
                    (thirdByte & 0xC0) != 0x80)
                    throw new EndOfStreamException();
                codePoint = ((firstByte & 0x0F) << 12) |
                            ((secondByte & 0x3F) << 6) |
                             (thirdByte & 0x3F);
            }
            else
            {
                // 4-byte sequence (outside BMP, requires surrogate pairs for C# char)
                int secondByte = _stream.ReadByte();
                int thirdByte = _stream.ReadByte();
                int fourthByte = _stream.ReadByte();
                if (secondByte == -1 || thirdByte == -1 || fourthByte == -1 ||
                    (secondByte & 0xC0) != 0x80 ||
                    (thirdByte & 0xC0) != 0x80 ||
                    (fourthByte & 0xC0) != 0x80)
                    throw new EndOfStreamException();
                codePoint = ((firstByte & 0x07) << 18) |
                            ((secondByte & 0x3F) << 12) |
                            ((thirdByte & 0x3F) << 6) |
                             (fourthByte & 0x3F);

                // For full Unicode > BMP, C# char cannot represent beyond 0xFFFF
                // You would need a string or a pair of chars for surrogate pairs here
                throw new NotImplementedException("Surrogate pairs not supported in char return");
            }

            return (char)codePoint;
        }

        /// <summary>
        /// Reads the underlying steram as UTF-8 encoded characters.
        /// </summary>
        /// <returns>The stream's content as string</returns>
        /// <exception cref="EndOfStreamException">Nothing more can be read</exception>
        public string ReadString()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Separates the string given into a list of unicode sclars. 
        /// As an unicode string consists of a sequence of characters which are 
        /// - by definition - stored in UTF-16, measures have to be taken to convert
        /// characters not fitting into a single UTF-16 value into an UTF-32 scalar.
        /// </summary>
        /// <param name="str">String to be split</param>
        /// <returns>An enumerable sequence of UTF-32 scalars ("UTF-32 characters")</returns>
        private static IEnumerable<int> UnicodeScalarsFromString(string str)
        {
            var uc_scalars = new List<int>();
            for (int i = 0; i < str.Length; i++)
            {
                uc_scalars.Add(char.ConvertToUtf32(str, i));
                if (char.IsHighSurrogate(str[i]))
                {
                    i += 1;
                }
            }
            return uc_scalars;
        }

        #region Stream Interface
        public override bool CanRead => _stream.CanRead;

        public override bool CanSeek => _stream.CanSeek;

        public override bool CanWrite => _stream.CanWrite;

        public override long Length => _stream.Length;

        public override long Position
        {
            set
            {
                _stream.Position = value;
            }
            get
            {
                return _stream.Position;
            }
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count); 
        }
        #endregion
    }
}
