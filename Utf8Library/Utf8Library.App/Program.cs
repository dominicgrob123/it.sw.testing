namespace Utf8Library.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("UTF-8 Encoder demo");
            using (var file = File.Open(@"test.bin", FileMode.OpenOrCreate))
            using (var utf = new Utf8Stream(file))
            {
                file.SetLength(0);

                utf.Write('☎');
                utf.Write(Environment.NewLine);
                utf.Write("Yay! 🚀");
                utf.Write(Environment.NewLine);
                utf.Write(48);
                utf.Write(181);
                utf.Write(9200);
                utf.Write(128521);

                utf.Flush();
                utf.Close();

                utf.ReadChar();
            }
            Console.WriteLine("UTF-8 Encoder demo ended");
        }
    }
}