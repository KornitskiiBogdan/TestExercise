using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WordProcessing
{
    public static class WordProcessing
    {
        private static bool RemoveString(StringBuilder sb, int length, ref int i, int lengthWord)
        {
            if (lengthWord > 0 && lengthWord <= length)
            {
                sb.Remove(i - lengthWord, lengthWord);
                i -= lengthWord + 1;
                return true;
            }
            return false;
        }
        public static async Task<EStatus> Calculate(string inputFilePath, string outputFilePath, int minLength, bool removePunctuation)
        {
            Task<EStatus> task = new Task<EStatus>(() => CalculateStatus(inputFilePath, outputFilePath, minLength, removePunctuation));
            task.Start();
            return await task;
        }
        private static EStatus CalculateStatus(string inputFilePath, string outputFilePath, int minLength, bool removePunctuation)
        {
            string ext = Path.GetExtension(inputFilePath);
            if (ext == ".txt")
            {
                using (StreamReader r = new StreamReader(inputFilePath))
                {
                    using (StreamWriter w = new StreamWriter(outputFilePath, true))
                    {
                        while (!r.EndOfStream)
                        {
                            StringBuilder sb = new StringBuilder(r.ReadLine());
                            int i = 0;
                            int lengthWord = 0;
                            while (i < sb.Length)
                            {
                                if (removePunctuation && char.IsPunctuation(sb[i]))
                                {
                                    sb.Remove(i, 1);
                                    if (!RemoveString(sb, minLength, ref i, lengthWord) && i > 0)
                                    {
                                        i--;
                                    }
                                    lengthWord = 0;
                                }
                                else if (!char.IsSeparator(sb[i]))
                                {
                                    lengthWord++;
                                }
                                else
                                {
                                    if (i > 0 && char.IsSeparator(sb[i - 1]))
                                    {
                                        sb.Remove(i, 1);
                                        if (i > 0)
                                        {
                                            i--;
                                        }
                                    }
                                    else
                                    {
                                        RemoveString(sb, minLength, ref i, lengthWord);
                                    }
                                    lengthWord = 0;
                                }
                                i++;
                            }
                            w.WriteLine(sb);
                        }
                        w.Close();
                        r.Close();
                    }

                }
                MessageBox.Show("Обработка закончена", "Обработка текста", MessageBoxButton.OK, MessageBoxImage.Information);
                return EStatus.Done;
            }
            else
            {
                MessageBox.Show("Файл данного типа не поддерживается", "Обработка текста", MessageBoxButton.OK, MessageBoxImage.Warning);
                return EStatus.Failed;
            }
        }
    }
}
