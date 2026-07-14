using System;
using System.Collections.Generic;

public static class Arrays
{
    public static double[] MultiplesOf(double number, int length)
    {
        double[] results = new double[length];
        
        for (int i = 0; i < length; i++)
        {
            results[i] = number * (i + 1);
        }
        
        return results;
    }

    public static void RotateListRight(List<int> data, int amount)
    {
        if (data.Count == 0) return; 

        int rotationAmount = amount % data.Count;
        
        if (rotationAmount == 0) 
        {
            return;
        }

        int splitIndex = data.Count - rotationAmount;
        
        List<int> tailElements = data.GetRange(splitIndex, rotationAmount);
        data.RemoveRange(splitIndex, rotationAmount);
        data.InsertRange(0, tailElements);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Test MultiplesOf(3, 5) ---");
        double[] multiples = Arrays.MultiplesOf(3, 5);
        Console.WriteLine(string.Join(", ", multiples)); 

        Console.WriteLine("\n--- Test RotateListRight ---");
        
        List<int> data = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Console.WriteLine("Original list: " + string.Join(", ", data));
        
        Arrays.RotateListRight(data, 5);
        Console.WriteLine("After rotating right by 5: " + string.Join(", ", data)); 
    }
}