using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiralMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            MethodObject methodObj = new MethodObject();
            int itemsInMatrix = 10;

            int[][] matrix = new int[itemsInMatrix][];

            for (int i = 0; i < itemsInMatrix; i++)
            {
                matrix[i] = new int[itemsInMatrix];
            }

            methodObj.Method(matrix);
        }
    }

    public class MethodObject
    {
        string FileLocation = Path.Combine(Directory.GetCurrentDirectory(), "Test Files");
        string FileName = "Output.txt";

        void PrintArray(object array, StreamWriter writer = null)
        {
            if (typeof(int[][]) == array.GetType())
            {
                //Type pointer for easy method reading/ usage
                int[][] typedArray = (int[][])array;

                for (int i = 0; i < typedArray.Length; i++)
                {
                    if (writer != null)
                    {
                        writer.WriteLine(string.Join("", typedArray[i]));
                    }
                    
                    Console.WriteLine(string.Join("", typedArray[i]));
                }

                if (writer != null)
                {
                    writer.WriteLine();
                }
                
                Console.WriteLine();
                
            }
        }

        public void Method(int[][] matrix)
        {
            decimal elements = matrix.Length;
            int midPoint = Convert.ToInt32(Math.Round(elements / 2, 0, MidpointRounding.AwayFromZero));

            midPoint = midPoint % 1 == 0 ? midPoint : midPoint + 1;

            if (Directory.Exists(FileLocation) == false)
            {
                Directory.CreateDirectory(FileLocation);
            }

            using (StreamWriter writer = new StreamWriter(Path.Combine(FileLocation, FileName)))
            {
                try
                {
                    PrintArray(matrix, writer);
                    byte iteration = 1;
                    int totalRunThroughs = 0;

                    for (int i = midPoint, j = midPoint; i != (elements - 1) || j != 0; )
                    {
                        if (iteration == 1 && totalRunThroughs == 0)
                        {
                            matrix[i][j] = 1;
                            PrintArray(matrix, writer);
                        }

                        int finalValue = 0;

                        switch (iteration)
                        {
                            case 1:
                                i--;
                                for (int k = 0; k <= (totalRunThroughs > 0 ? totalRunThroughs * 2 : totalRunThroughs); k++)
                                {
                                    matrix[i - k][j] = 1;
                                    finalValue = i - k;

                                    PrintArray(matrix, writer);
                                }

                                i = finalValue;
                                break;
                            case 2:
                                j--;
                                for (int k = 0; k <= (totalRunThroughs > 0 ? totalRunThroughs * 2 : totalRunThroughs); k++)
                                {
                                    matrix[i][j - k] = 1;
                                    finalValue = j - k;

                                    PrintArray(matrix, writer);
                                }

                                j = finalValue;
                                break;
                            case 3:
                                i++;
                                for (int k = 0; k <= (totalRunThroughs > 0 ? totalRunThroughs * 2 : totalRunThroughs); k++)
                                {
                                    matrix[i + k][j] = 1;
                                    finalValue = i + k;

                                    PrintArray(matrix, writer);
                                }

                                i = finalValue;
                                break;
                            case 4:
                                if (elements % 2 == 1 && i == elements - 1)
                                {
                                    iteration = 0;
                                    i = 0;
                                    j = Convert.ToInt32(elements - 1);

                                    for (int k = 0; k <= (totalRunThroughs > 0 ? (totalRunThroughs * 2 + 2) : totalRunThroughs); k++)
                                    {
                                        matrix[i][j - k] = 1;
                                        finalValue = j - k;

                                        PrintArray(matrix, writer);
                                    }

                                    j = finalValue;
                                    i++;

                                    for (int k = 0; k <= (totalRunThroughs > 0 ? totalRunThroughs * 2 + 1 : totalRunThroughs); k++)
                                    {
                                        matrix[i + k][j] = 1;
                                        finalValue = i + k;

                                        PrintArray(matrix, writer);
                                    }

                                    i = Convert.ToInt32(elements - 1);
                                }
                                else
                                {
                                    i++;
                                    for (int k = 0; k <= (totalRunThroughs > 0 ? totalRunThroughs * 2 : totalRunThroughs) + 2; k++)
                                    {
                                        matrix[i][j + k] = 1;
                                        finalValue = j + k;

                                        PrintArray(matrix, writer);
                                    }
                                    iteration = 0;
                                    j = finalValue;
                                }

                                totalRunThroughs += 1;
                                break;
                        }

                        iteration++;
                    }
                }
                catch
                {
                    writer.Close();
                }
            }
        }
    }
}
