﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Test1
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
        public static EStatus CalculateStatus(string inputFilePath, string outputFilePath, int minLength, bool removePunctuation)
        {
            string extInput = Path.GetExtension(inputFilePath);
            if (extInput != ".txt" || outputFilePath != ".txt")
            {
                return EStatus.Failed;
            }
            try
            {                
                using (StreamReader r = new StreamReader(inputFilePath))
                {
                    using (StreamWriter w = new StreamWriter(outputFilePath, false))
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
                return EStatus.Done;
            }
            catch
            {

            }
            return EStatus.Failed;
        }
    }
}
