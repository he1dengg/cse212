using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue mixed priorities and dequeue once.
    // Expected Result: The item with the highest priority is removed.
    // Defect(s) Found: Loop condition was < _queue.Count - 1, missing the last item. Also, items were never removed from the list.
    public void TestPriorityQueue_HighestPriorityRemoved()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High", 5);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("High", result);
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with the SAME high priority.
    // Expected Result: FIFO rules apply. The first item added is removed first.
    // Defect(s) Found: Used >= in the priority check loop, which caused it to return the newest item instead of the oldest one.
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("FirstHigh", 5);
        priorityQueue.Enqueue("SecondHigh", 5);
        
        var result1 = priorityQueue.Dequeue();
        var result2 = priorityQueue.Dequeue();
        
        Assert.AreEqual("FirstHigh", result1);
        Assert.AreEqual("SecondHigh", result2);
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue.
    // Expected Result: InvalidOperationException with "The queue is empty."
    // Defect(s) Found: None. Exception logic was working as intended.
    public void TestPriorityQueue_EmptyQueueThrowsException()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }
}