using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorMate_Brickwork
{
    public class Brickwork
    {
        public static void Calculate()
        {

            int[,] InputBrickkWall = Input(); //filling the input matrix / brickwall

            if (Validate(InputBrickkWall)) // validating there are no bricks with span of 3 rows or columns
            {
                throw new Exception("Three row/colums brick!");
            }

            int[,] OutputBrickWall = new int [InputBrickkWall.GetLength(0), InputBrickkWall.GetLength(1)]; // output matrix / brickwall
            
            for (int i = 0; i < InputBrickkWall.GetLength(0) - 1; i += 2) // looping through each element of the input brickwall using nested for loops
            {
                for (int j = 0; j < InputBrickkWall.GetLength(1); j++)
                {
                    if (j + 1 < InputBrickkWall.GetLength(1))  // checking if this is the rightmost element of the row
                    {
                        if (InputBrickkWall[i, j] == InputBrickkWall[i, j + 1]) // case where the element equals the right one
                        {
                            OutputBrickWall[i, j] = InputBrickkWall[i, j];
                            OutputBrickWall[i + 1, j] = InputBrickkWall[i, j];
                        }
                        else // case where the element is not equal to the right one 
                        {
                            OutputBrickWall[i, j] = InputBrickkWall[i, j + 1];
                            OutputBrickWall[i, j + 1] = InputBrickkWall[i, j + 1];

                            OutputBrickWall[i + 1, j] = InputBrickkWall[i + 1, j];
                            OutputBrickWall[i + 1, j + 1] = InputBrickkWall[i + 1, j];
                            j++;

                        }
                    }
                    else //case where this the element is the rightmost of the row
                    {
                        if (InputBrickkWall[i, j] != InputBrickkWall[i+1,j])
                        {
                            OutputBrickWall[i, j] = InputBrickkWall[i+1, j];
                            OutputBrickWall[i + 1, j] = InputBrickkWall[i+1, j];
                        }
                    }
                }
            }

            PrintBrickWall(OutputBrickWall); // printing the second layer of the brickwall / output matrix
        }
        private static int[,] Input() // input data method 
        {
            
            bool flag = false;
            int M=0, N=0; // size of the matrix

            while (!flag) {

                Console.WriteLine("Enter the size of the matrix(Even numbers only):");

                string LinesAndColumns = Console.ReadLine(); //reading the input
                string[] array = LinesAndColumns.Trim().Split(); // splitting the input into array of strings

                if (array.Length != 2) { continue; }

                M = int.Parse(array[0]);
                N = int.Parse(array[1]);

                if (M%2==0 && N%2==0 && M<99 && M>1 && N < 99 && N > 1) // validating the dimensions of the matrix are even numbers,greater than 1 and less than 99
                {
                    flag = true;
                }
            }
            int[,] mArray = new int[M,N];

            for (int i = 0; i < M; i++) // using nested loops to input the data into the matrix
            {
                Console.WriteLine("Enter bricks on row:{0}", i + 1);
                string LinesAndColumns2 = Console.ReadLine();
                string[] array2 = LinesAndColumns2.Split();


                for (int j = 0; j < N; j++)
                {
                    mArray[i, j] = int.Parse(array2[j]);
                }
            }
            return mArray; // returning the matrix
        }

        private static void PrintBrickWall(int[,] BrickWall)
        {

            Console.WriteLine("\nLayer 2 : \n");
            Console.WriteLine(new string ('*', 2*BrickWall.GetLength(1)+1)); 

            var builder = new StringBuilder(); // using the builder to output the borders between the horizontal lines of the matrix

            for (int i = 0; i < BrickWall.GetLength(0); i++) // using nested loops to print every element of the matrix and the border if there is supposed to be one
            {
                builder.Append("*");
                Console.Write("*");
                for (int j = 0; j < BrickWall.GetLength(1); j++)
                {

                    if (i + 1 < BrickWall.GetLength(0) && j + 1 < BrickWall.GetLength(1)) // case where the element is not the rightmost and not from the lowest row
                    {
                        if (BrickWall[i, j] == BrickWall[i, j + 1])
                        {
                            Console.Write(BrickWall[i, j] + " ");
                            builder.Append("*");

                        }
                        else if (BrickWall[i, j] != BrickWall[i, j + 1] && BrickWall[i, j] != BrickWall[i + 1, j])
                        {
                            Console.Write(BrickWall[i, j] + "*");
                            builder.Append("*");
                        }
                        else
                        {
                            Console.Write(BrickWall[i, j] + "*");
                            builder.Append(" ");
                        }
                        builder.Append("*");
                    }

                    else if (i + 1 == BrickWall.GetLength(0) && j + 1 < BrickWall.GetLength(1)) // case where the element is from the lowest row, but not the rightmost
                    {
                        if (BrickWall[i, j] == BrickWall[i, j + 1])
                        {
                            Console.Write(BrickWall[i, j] + " ");
                        }
                        else
                        {
                            Console.Write(BrickWall[i, j] + "*");
                        }
                        builder.Append("**");
                    }

                    else if (i + 1 < BrickWall.GetLength(0) && j + 1 == BrickWall.GetLength(1)) //case where the element is the rightmost, but not from the lowest row
                    {
                        if (BrickWall[i, j] == BrickWall[i + 1, j])
                        {
                            Console.Write(BrickWall[i, j] + "*");
                            builder.Append(" ");

                        }
                        else
                        {
                            Console.Write(BrickWall[i, j] + "*");
                            builder.Append("*");
                        }

                        builder.Append("*");
                    }

                    else
                    {
                        Console.Write(BrickWall[i, j] + "*"); // case where the element is from the lowest row and the rightmost (the last element from the matrix)
                        builder.Append("**");
                    }
                }
                Console.WriteLine("\n" + builder);
                builder.Clear();
            }
        }
        private static bool Validate(int [,] Brickwall) // Validate method for bricks spanning 3 rows/columns
        {
            for(int i = 0; i < Brickwall.GetLength(0); i++)
            {
                for(int j = 0; j < Brickwall.GetLength(1); j++)
                {
                    if (i + 2 < Brickwall.GetLength(0))
                    {
                        if (Brickwall[i, j] == Brickwall[i + 2, j])
                            return true;
                    }
                    if (j + 2 < Brickwall.GetLength(1))
                    {
                        if (Brickwall[i, j] == Brickwall[i , j + 2])
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
