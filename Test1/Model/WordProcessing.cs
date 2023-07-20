using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Test1.ViewModel;

namespace Test1.Model
{
    public static class WordProcessing
    {
        
        public static EStatus CalculateStatus(string inputFilePath, string outputFilePath, int minLength, bool removePunctuation)
        {
            string extInput = Path.GetExtension(inputFilePath);
            string extOutput = Path.GetExtension(outputFilePath);
            if (extInput != ".txt" || extOutput != ".txt")
            {
                return EStatus.Failed;
            }
            try
            {
                using (StreamReader r = new StreamReader(inputFilePath))
                {
                    using (StreamWriter w = new StreamWriter(outputFilePath, false))
                    {
                        Process(r, w, minLength, removePunctuation);
                        w.Close();
                        r.Close();
                    }

                }
                return EStatus.Done;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Обработка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return EStatus.Failed;
        }
        private static void Process(StreamReader r, StreamWriter w, int minLength, bool removePunctuation)
        {
            bool writeInFile = false;
            bool emptyString = true;
            StringBuilder stringBuffer = new StringBuilder();
            while (!r.EndOfStream)
            {
                int intSymbol = r.Read();
                if(intSymbol <= -1)
                {
                    return;
                }
                char c = (char)intSymbol;
                if (char.IsPunctuation(c))
                {
                    if (!removePunctuation)
                    {
                        w.Write(c);
                        emptyString = false;
                    }
                    writeInFile = false;
                    stringBuffer.Clear();
                }
                else if (c.Equals('\n') || c.Equals('\r') || char.IsSeparator(c))
                {
                    if (c.Equals('\n') || c.Equals('\r'))
                    {
                        if (!emptyString)
                        {
                            w.Write(c);
                            emptyString = true;
                        }
                    }
                    else
                    {
                        if (!emptyString)
                        {
                            w.Write(c);
                        }
                    }
                    stringBuffer.Clear();
                    writeInFile = false;
                }
                else
                {
                    if (stringBuffer.Length < minLength - 1 && !writeInFile)
                    {
                        stringBuffer.Append(c);
                    }
                    else
                    {
                        writeInFile = true;
                        w.Write(stringBuffer);
                        w.Write(c);
                        emptyString = false;
                        stringBuffer.Clear();
                    }
                }
            }
        }
    }
}
