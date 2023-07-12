using System;
using System.Collections.Generic;
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
                        //int index = 0;
                        while (!r.EndOfStream)
                        {
                            char[] buffer = new char[1024];
                            if(r.Read(buffer) > 0)
                            {
                                //index += buffer.Length;
                                w.Write(Calculate(buffer.ToList(), minLength, removePunctuation));
                            }
                            
                        }
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
        private static bool RemoveString(List<char> line, int length, ref int i, int lengthWord)
        {
            if (lengthWord > 0 && lengthWord <= length)
            {
                line.RemoveRange(i - lengthWord, lengthWord);
                i -= lengthWord + 1;
                return true;
            }
            return false;
        }
        public static char[] Calculate(List<char> line, int minLength, bool removePunctuation)
        {
            int i = 0;
            int lengthWord = 0;
            while (i < line.Count)
            {
                char c = line[i];
                
                if (removePunctuation && char.IsPunctuation(c))
                {
                    line.Remove(c);
                    if (!RemoveString(line, minLength, ref i, lengthWord) && i > 0)
                    {
                        i--;
                    }
                    lengthWord = 0;
                }
                else if (char.IsLetterOrDigit(c))
                {
                    lengthWord++;
                }
                else
                {
                    if (i > 0 && char.IsSeparator(line[i - 1]))
                    {
                        line.Remove(c);
                        if (i > 0)
                        {
                            i--;
                        }
                    }
                    else
                    {
                        RemoveString(line, minLength, ref i, lengthWord);
                    }
                    lengthWord = 0;
                }
                i++;
            }
            return line.ToArray();
        }
    }
}
