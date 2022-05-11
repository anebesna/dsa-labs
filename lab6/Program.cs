using System;
using static System.Console;
using System.Threading;

namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue q = new Queue();
            while (true)
            {
                try
                {
                    Write("\nEnter the command or write /help to see the available commands: ");
                    string command = ReadLine();
                    if (command == "/help")
                    {
                        Write("/help\n/initq\n/destroyq\n/enq\n/deq\n/size\n/head\n/isEmpty\n/example\n/exit");
                    }
                    else if (command == "/initq")
                    {
                        Write("Enter the size of the queue: ");
                        int size = int.Parse(ReadLine());
                        while(size < 1)
                        {
                            WriteLine("The size can't be equal or less than zero, try again: ");
                            size = int.Parse(ReadLine());
                        }
                        q.InitQueue(size);
                    }
                    else if (command == "/destroyq")
                    {
                        q.DestroyQueue();
                    }
                    else if (command == "/enq")
                    {
                        Write("Add an element to the queue: ");
                        int s = int.Parse(ReadLine());
                        while (s < 0)
                        {
                            Write("\nThis is unnatural number. Try again: ");
                            s = int.Parse(ReadLine());
                        }
                        q.Enqueue(s);
                    }
                    else if (command == "/deq") q.Dequeue();
                    else if (command == "/head") q.Head();
                    else if (command == "/size") Write(q.Size());
                    else if (command == "/exit") Environment.Exit(0);
                    else if (command == "/isEmpty") Write(q.isEmpty());
                    else if (command == "/example") q.Example();
                    else WriteLine("Wrong command");
                }
                catch
                {
                    WriteLine("You might forgot to initialize the queue");
                }
            }
        }
    }

    class Queue
    {
        int[] array;
        int head, tail, count;
        int len;
        public void InitQueue(int size)
        {
            array = new int[size];
            head = 0;
            tail = -1;
            count = 0;
            WriteLine($"The queue of size {size} has been created!");
            len = size;
        }

        public void DestroyQueue()
        {
            array = new int[0];
            WriteLine("The queue has been destroyed!");
        }

        public void Enqueue(int data)
        {
            int size;
            //check if full
            if (count == len)
            {
                WriteLine("Your queue is full! Change it to a bigger size: ");
                size = int.Parse(ReadLine());
                while (size <= len)
                {
                    Write("This size is less or equal to current one, try again: ");
                    size = int.Parse(ReadLine());
                }
                len = size;
                ChangeSize(size);
            }
            if (data == 0)
            {
                Showq();
                WriteLine();
                Dequeue();
                Thread.Sleep(500);
                WriteLine();
                Dequeue();
                Thread.Sleep(500);
                WriteLine();
                Dequeue();
                WriteLine();
            }
            else
            {
                tail = (tail + 1) % len;
                array[tail] = data;
                count++;
                Showq();
            }
        }

        public void Dequeue()
        {
            //check if empty
            if (count == 1)
            {
                WriteLine("Your queue is empty!");
                Environment.Exit(0);
            }
            else
            {
                head = (head + 1) % len;
                count--;
                Showq();
            }
        }

        public void Head()
        {
            Write($"The head is {array[head]} at index {head}");
        }

        public int Size() => count;

        public bool isEmpty() => count == 0; 

        public void ChangeSize(int size)
        {
            int[] buf = array;
            array = new int[size];
            int j;
            if (head > tail)
            {
                j = head;
                head = size - buf.Length + head;
                for (int i = 0; i <= tail; i++)
                {
                    array[i] = buf[i];
                }
                for (int i = head; i < len; i++)
                {
                    array[i] = buf[j];
                    j++;
                }
            }
            else
            {
                j = 0;
                for (int i = head; i <= tail; i++)
                {
                    array[j] = buf[i];
                    j++;
                }
            }
        }

        public void Showq()
        {
            WriteLine("Current queue is: ");
            if (head > tail)
            { 
                for (int i = head; i < len; i++)
                {
                    Write($"{array[i]}\t");
                }
                for (int i = 0; i <= tail; i++)
                {
                    Write($"{array[i]}\t");
                }
            }
            else
            {
                for (int i = head; i <= tail; i++)
                {
                    Write($"{array[i]}\t");
                }
            }
        }

        public void Example()
        {
            WriteLine("Queue's size is 4");
            array = new int[4];
            head = 0;
            tail = -1;
            count = 0;
            len = array.Length;
            for (int i = 1; i < 5; i++)
            {
                WriteLine($"\nLet's enqueue {i}");
                Enqueue(i);
                Thread.Sleep(1000);
            }
            WriteLine("\nLet's dequeue");
            Dequeue();
            Thread.Sleep(1000);
            WriteLine("\nLet's enqueue 5");
            Enqueue(5);
            Thread.Sleep(1000);
            WriteLine("\nLet's change size to 5");
            len = 5;
            ChangeSize(5);
            Thread.Sleep(1000);
            WriteLine("\nLet's enqueue 6");
            Enqueue(6);
            Thread.Sleep(1000);
            WriteLine("\nLet's dequeue");
            Dequeue();
            Thread.Sleep(1000);
            WriteLine("\nLet's enqueue 0");
            Enqueue(0);
            Thread.Sleep(1000);
            WriteLine("\nLet's dequeue");
            Dequeue();
        }

    }
}
