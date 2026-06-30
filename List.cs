using System;

LinkedList<int> MyList = new LinkedList<int>();

MyList.AddLast(10);
MyList.AddLast(11);
MyList.AddLast(9);
MyList.AddLast(15);

for (int i = 0; i < MyList.Count; i++) {
    Console.WriteLine(MyList[i]);
}

Console.WriteLine();
MyList.RemoveLast();
MyList.RemoveLast();
MyList.AddLast(20);
MyList.AddFirst(5);

for (int i = 0; i < MyList.Count; i++) {
    Console.WriteLine(MyList[i]);
}

Console.WriteLine();
MyList.Clear();
MyList.AddLast(10);
MyList.AddFirst(5);
MyList[0] = 2;
MyList.AddLast(3);
MyList.AddLast(1);
MyList.Insert(7, 3);
for (int i = 0; i < MyList.Count; i++) {
    Console.WriteLine(MyList[i]);
}

public class LinkedList<T>
{
	private Node<T> Head = null;
    private Node<T> Last = null;
    private Node<T> Tail = null;

    private uint LastIndex = 0;
    private uint Size = 0;

    public uint Count => this.Size;

    public T this[int index]
    {
        get
        {
            return GetByIndex(index).val;
        }
        set
        {
            GetByIndex(index).val = value;
        }
    }

    public void AddFirst(T value)
    {
        if (this.Head == null) {
            this.Head = new Node<T>(value);

            this.Last = this.Head;
            this.Tail = this.Head;

            this.LastIndex = 0;
            this.Last = Head;
            this.Size++;
            return;
        }

        Node<T> node = new Node<T>(value);
        this.Head.Previous = node;

        Node<T> curr = this.Head;
        this.Head = node;
        this.Head.Next = curr;

        this.LastIndex = 0;
        this.Last = Head;
        this.Size++;
    }

    public void AddLast(T value)
    {
        if (this.Head == null) {
            this.Head = new Node<T>(value);

            this.Last = this.Head;
            this.Tail = this.Head;
            this.Head = Head;

            this.Size++;
            return;
        }

        Node<T> Prev = this.Tail;
        this.Tail = new Node<T>(value);

        Prev.Next = this.Tail;
        this.Tail.Previous = Prev;

        this.Last = Head;
        this.LastIndex = 0;
        this.Size++;
    }

    public void RemoveLast()
    {
        if (this.Head == this.Tail) {
            this.Head = null;
            this.Tail = null;
            this.Last = null;

            this.LastIndex = 0;
            this.Size = 0;
            return;
        }

        this.Tail = this.Tail.Previous;
        this.Tail.Next = null;

        this.Last = Head;
        this.LastIndex = 0;
        this.Size--;
    }
    
    public void Insert(T value, int index)
    {
        if (this.Head == null || index < 0 || index > this.Size-1) {
            throw new ArgumentOutOfRangeException("Index out of range");
        }

        Node<T> Actual = GetByIndex(index);
        Node<T> NewNode = new Node<T>(value);

        NewNode.Next = Actual;
        NewNode.Previous = Actual.Previous;

        Actual.Previous.Next = NewNode;
        Actual.Previous = NewNode;

        this.Size++;
    }

    public void Clear()
    {
        if (this.Head == null) {
            return;
        }

        this.Head = null;
        this.Tail = null;
        this.Last = null;

        this.Size = 0;
        this.LastIndex = 0;
    }
    
    private Node<T> GetByIndex(int index)
    {
        //Validates the Index
        if (this.Head == null || index < 0 || index > this.Size-1) {
            throw new ArgumentOutOfRangeException("Index out of range");
        }
        
        if (this.LastIndex == index) {
            return this.Last;
        }

        Node<T> Current = null;

        uint Start = 0;
        uint End = this.Size-1;

        Node<T> StartNode = this.Head;
        Node<T> EndNode = this.Tail;

        if (index > this.LastIndex) {
            Start = this.LastIndex;
            StartNode = this.Last;
        } else {
            End = this.LastIndex;
            EndNode = this.Last;
        }

        int distance = (int)(index - Start);
        if ((End - index) < distance) {
            Current = EndNode;

            for (uint i = End; i > index; i--) {
                Current = Current.Previous;
            }

        } else {
            Current = StartNode;

            for (uint i = Start; i < index; i++) {
                Current = Current.Next;
            }
        }

        this.Last = Current;
        this.LastIndex = (uint) index;
        return Current;
    }
}

public class Node<T>(T value)
{
	public Node<T> Next = null;
    public Node<T> Previous = null;

    public T val = value;
}