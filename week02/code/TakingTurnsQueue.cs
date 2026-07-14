using System;

public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }
        
        Person person = _people.Dequeue();
        
        if (person.Turns > 1)
        {
            person.Turns -= 1;
            _people.Enqueue(person);
        }
        else if (person.Turns <= 0) 
        {
            // Fixed: Make sure people with 0 or negative turns (infinite) are added back to the queue.
            _people.Enqueue(person);
        }

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}