﻿using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("Sujeet's Grade Book");
            
            Console.WriteLine("Enter a grade or q to quit: ");
            string? input = Console.ReadLine();
            
            while(input != "q")
            {
                try
                {
                    var isSuccess = double.TryParse(input, out double grade);
                    if(isSuccess)
                    {
                        book.AddGrade(grade);
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("finally done");
                }
                Console.WriteLine("Enter a grade or q to quit:");
                input = Console.ReadLine();
            }

            var stats = book.GetStatistics();
            Console.WriteLine($"The lowest grade is { stats.Low }");
            Console.WriteLine($"The highest grade is { stats.High }");
            Console.WriteLine($"The avreage grade is { stats.Average:N1}");
            Console.WriteLine($"The letter grade is {stats.Letter}");
        }
    }
}