using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class CS255_HW2
    {

        private static int MIN_INTEGER = System.Int32.MinValue;
        private static int MAX_INTEGER = System.Int32.MaxValue;

        //-----------------------------------------------------------//
        //-----------------------------------------------------------//

        static void Main(string[] args)
        {
            int Q1_n, Q6_n, Q8_n, Q10_n;//----- n in T(n)
            int min;
            int max;
            int loc;
            int search_val;
            int Q9_numb_calls;
            int[] list_of_numbers;
            int[] Q1_array, Q10_array;
            Tuple<int, int, int> Q8_Answer;

            //--------------------------------------------------------------------------------------//
            //                                  Question #1                                         //
            //--------------------------------------------------------------------------------------//
            
            //----- Create unsorted array
            Q1_n = 15;
            Q1_array = Create_Int_Array(Q1_n, 10, 30);

            Console.WriteLine("Question #1 - Take the unsorted array below and sort it...");
            PrintArray(Q1_array, Q1_n);
            
            //----- Sort the array then print the results.
            Q1_Insertion_Sort(Q1_array, Q1_n-1);
            Console.WriteLine("The sorted array is: ");
            PrintArray(Q1_array, Q1_n);
            Console.Write("\n\n\n\n");

            //--------------------------------------------------------------------------------------//
            //                                  Question #6                                         //
            //--------------------------------------------------------------------------------------//

            Q6_n = 10;
            min = int.MaxValue; //----- Define so always overwritten
            max = int.MinValue; //----- Define so always overwritten
            list_of_numbers = Create_Int_Array(Q6_n, 0, 50);

            //----- Peform Divide and conquer step
            Q6_Find_Min_and_Max(list_of_numbers, 0, Q6_n - 1, ref min, ref max);

            //----- Print the results
            Console.WriteLine("Question #6: Define a recursive function to find the minimum and maximum values in an array.  The array is:");
            PrintArray(list_of_numbers, Q6_n);
            Console.WriteLine("The minimum value in the array was: " + Convert.ToString(min));
            Console.WriteLine("The maximum value in the array was: " + Convert.ToString(max) + "\n\n\n");

            //--------------------------------------------------------------------------------------//
            //                                  Question #8                                         //
            //--------------------------------------------------------------------------------------//

            Q8_n = 20;
            list_of_numbers = Create_Int_Array(Q8_n, 0, 50);
            Q8_Answer = Q8_Brute_Force_Maximum_Subarray(list_of_numbers, Q8_n);

            //----- Print the results
            Console.WriteLine("Question #8: Create code to calculate maximum sub array using the brute force approach:");
            PrintArray(list_of_numbers, Q8_n);
            Console.WriteLine("The maximum subbarray in the array was: " + Convert.ToString(Q8_Answer.Item1) );
            Console.WriteLine("The maximum subarray started at index {0} and ended at index {1}.\n\n\n", Q8_Answer.Item2, Q8_Answer.Item3);


            //--------------------------------------------------------------------------------------//
            //                                  Question #9                                         //
            //--------------------------------------------------------------------------------------//

            search_val = 20;
            Q9_numb_calls = 0;
            Console.Write("Using the sorted array from question #1 (i.e. insertion sort).  Searching for the number \"" + Convert.ToString(search_val) + "\".\n");
            loc = Q9_Find_Element(Q1_array, search_val, 0, Q1_n - 1, ref Q9_numb_calls);

            if (loc == -1)
                Console.WriteLine(Convert.ToString(search_val) + " was not found in the array.");
            else
                Console.WriteLine(Convert.ToString(search_val) + " was in the array at position " + Convert.ToString(loc) + ".");

            Console.WriteLine("The number of calls to determine the position of " + Convert.ToString(search_val) + " was " + Convert.ToString(Q9_numb_calls) + ".\n\n");


            //--------------------------------------------------------------------------------------//
            //                                  Question #1                                         //
            //--------------------------------------------------------------------------------------//

            //----- Create unsorted array
            Q10_n = 15;
            Q10_array = Create_Int_Array(Q10_n, 30, 40);

            Console.WriteLine("Question #10 - Take the unsorted array below and sort it...");
            PrintArray(Q10_array, Q10_n);

            //----- Sort the array then print the results.
            Q10_Quick_Sort_Prime(Q10_array, 0, Q10_n - 1);
            Console.WriteLine("The sorted array is through quick sort is: ");
            PrintArray(Q10_array, Q10_n);
            Console.Write("\n\n\n\n");


            //----- Cleanup and prevent memory leaks
            Q1_array = null;
            Q10_array = null;
            list_of_numbers = null;

        }



        //-----------------------------------------------------------------------//
        //  Homework #2 Question #1 - Exercise 2.3-4, page 39 from the textbook. //
        //  Express insertion sort as a recursive problem as follows: in  order  //
        //  to sort A[1..n], we recursively sort A[1..n-1] then insert // A[n]   //
        //  into the sort array A[1..n-1].                                       //
        //-----------------------------------------------------------------------//


        static void Q1_Insertion_Sort(int[] search_array, int right)
        {

            //----- Recursion stop condition
            if (right == 0) return;

            //----- Divide Step
            Q1_Insertion_Sort(search_array, right - 1);

            //---- Combine (i.e. insertion step)
            while (right > 0 && search_array[right] < search_array[right - 1])
            {
                Swap<int>(ref search_array[right], ref search_array[right - 1]);
                right--;
            }
            

        }



        static void Q6_Find_Min_and_Max(int[] search_array, int left, int right, ref int min, ref int max)
        {
            int mid;

            //----- Recursive stop condition
            if (right < left) return;

            //----- Divide step
            mid = (right + left) / 2;
            Q6_Find_Min_and_Max(search_array, left, mid - 1, ref min, ref max); //----- Left split
            Q6_Find_Min_and_Max(search_array, mid + 1, right, ref min, ref max); //---- Right split

            //----- Combine step
            if (search_array[mid] < min) min = search_array[mid];
            if (search_array[mid] > max) max = search_array[mid];

            return;
        }

        static Tuple<int, int, int> Q8_Brute_Force_Maximum_Subarray(int[] search_array, int n)
        {
            int i, j;
            int max_value = System.Int32.MinValue;
            int max_start =-1;
            int max_end = -1;
            int cur_sum;
            int[] diff_array;
            Tuple<int, int, int> output;


            //---- Calculate the difference on a daily basis (O(n)).
            diff_array = new int[n];
            for(i=1; i<n; i++)
                diff_array[i] = search_array[i]-search_array[i-1];

            //----- Embedded for loop for loops to calculate maximum subarray
            for(i=0; i < n-1; i++){
                cur_sum = 0; //---- Reset the counter

                //----- inner loop and check each min.
                for (j = i + 1; j < n; j++)
                {
                    cur_sum += diff_array[j];
                    if(cur_sum > max_value){
                        max_value = cur_sum;
                        max_start = i;
                        max_end = j;
                    }
                }
            }

            output = new Tuple<int, int, int>(max_value, max_start, max_end);
            return output;

        }

        static int Q9_Find_Element(int[] search_array, int search_element, int left, int right, ref int numb_calls)
        {
            int mid;
            //----- Recursion halt condition
            if (right < left) return -1;

            numb_calls++;

            mid = (left + right) / 2;
            if (search_element == search_array[mid]) 
                return mid;
            else if(search_element > search_array[mid])
                return Q9_Find_Element(search_array, search_element, mid + 1, right, ref numb_calls);
            else //if(search_element < search_array[mid])
                return Q9_Find_Element(search_array, search_element, left, mid - 1, ref numb_calls);

        }


        static void Q10_Quick_Sort_Prime(int[] A, int p, int r)
        {
            Tuple<int, int> partition_locations;
            
            //----- Recursion halt condition
            if(p>=r) return;

            //----- Divide
            partition_locations = Q10_Randomized_Partition_Prime(A, p, r);


            //---- Conquer
            Q10_Quick_Sort_Prime(A, p, partition_locations.Item1 - 1);
            Q10_Quick_Sort_Prime(A, partition_locations.Item2 + 1, r);

        }


        static Tuple<int, int> Q10_Randomized_Partition_Prime(int[] A, int p, int r)
        {
            Random rand = new Random();
            Tuple<int, int> Q10_partition_locations;
            int i;

            i = rand.Next(p,r+1);//---- Need to add 1 to r to make it inclusive
            Swap<int>(ref A[i], ref A[r]);

            //----- Perform partition and return the tuple
            Q10_partition_locations = Q10_Partition_Prime(A, p, r);
            return Q10_partition_locations;

        }

        static Tuple<int, int> Q10_Partition_Prime(int[] A, int p, int r)
        {
            int q, t, i, j, end, numb_swapped;
            Tuple<int, int> output;


            i = p - 1;
            end = r;
            j = p;
            while(j<end)
            {
                if( A[j] == A[r] )
                {
                    end--;
                    Swap<int>(ref A[j], ref A[end]);
                    continue;//----- Go to top of the loop to prevent an increment
                }

                if (A[j] < A[r])
                {
                    i++;
                    Swap<int>(ref A[i], ref A[j]);
                }
                j++;
            }

            //----- Move q into its place split between the two halfs
            q = ++i;
            numb_swapped = r - end + 1;
            for (j = 0; j < numb_swapped; j++)
            {
                Swap<int>(ref A[i+j], ref A[end+j]);
            }
            t = i+j-1;

            //------ Create the output and return the result
            output = Tuple.Create<int, int>(q, t);
            return output;
        }

        static int[] Create_Int_Array(int n, int min_value, int max_value)
        {
            int cnt;
            int[] list_of_numbers;
            Random rand = new Random();

            //------ Populate Memory
            list_of_numbers = new int[n];
            for (cnt = 0; cnt < n; cnt++)
                list_of_numbers[cnt] = rand.Next(min_value, max_value - 1);

            return list_of_numbers;

        }


        static void Swap<T>(ref T left, ref T right)
        {
            T temp_var;
            temp_var = left;
            left = right;
            right = temp_var;
        }

        static void PrintArray(int[] print_array, int n)
        {
            int cnt;

            Console.Write("[ ");
            for (cnt = 0; cnt < n; cnt++)
            {
                Console.Write(Convert.ToString(print_array[cnt]));
                if (cnt + 1 != n) Console.Write(", ");
                else Console.WriteLine(" ]");
            }
        }

    }
}
