using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: The Enqueue function shall add an item (which contains both data and priority) to the back of the queue. The Dequeue function shall remove the item with the highest priority and return its value.
    // Expected Result: tim, sue, bob
    // Defect(s) Found: Dequeue doesn't remove the item when it runs. Doesn't view the last item in the list. Fixed by adjusting Dequeue().
    public void TestPriorityQueue_1()
    {
        var bob = new PriorityItem("Bob", 2);
        var tim = new PriorityItem("Tim", 5);
        var sue = new PriorityItem("Sue", 3);

        PriorityItem[] expectedResult = [tim, sue, bob];

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue(bob.Value, bob.Priority);
        priorityQueue.Enqueue(tim.Value, tim.Priority);
        priorityQueue.Enqueue(sue.Value, sue.Priority);

        int i = 0;
        while (priorityQueue.Count > 0)
        {
            if (i >= expectedResult.Length)
                Assert.Fail("Queue should have ran out of items by now.");

            var value = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResult[i].Value, value);
            i++;
        }
    }

    [TestMethod]
    // Scenario: If there are more than one item with the highest priority, then the item closest to the front of the queue will be removed and its value returned.
    // Expected Result: tim, john, sue, bob
    // Defect(s) Found: John is queued first instead of Tim. The error is in Dequeue(). I changed >= to > so that John will only replace Tim's position if he has a higher priority not just higher than or equal to.
    public void TestPriorityQueue_2()
    {

        var bob = new PriorityItem("Bob", 2);
        var tim = new PriorityItem("Tim", 5);
        var sue = new PriorityItem("Sue", 3);
        var john = new PriorityItem("John", 5);

        PriorityItem[] expectedResult = [tim, john, sue, bob];

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue(bob.Value, bob.Priority);
        priorityQueue.Enqueue(tim.Value, tim.Priority);
        priorityQueue.Enqueue(sue.Value, sue.Priority);
        priorityQueue.Enqueue(john.Value, john.Priority);

        int i = 0;
        while (priorityQueue.Count > 0)
        {
            if (i >= expectedResult.Length)
                Assert.Fail("Queue should have ran out of items by now.");

            var value = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResult[i].Value, value);
            i++;
        }
    }

    [TestMethod]
    // Scenario: If the queue is empty, then an error exception shall be thrown. This exception should be an InvalidOperationException with a message of "The queue is empty."
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: none
    public void TestPriorityQueue_3()
    {

        var priorityQueue = new PriorityQueue();

        var ex = Assert.ThrowsException<InvalidOperationException>(() =>
        {
            priorityQueue.Dequeue();
        });

        Assert.AreEqual("The queue is empty.", ex.Message);
    }
}