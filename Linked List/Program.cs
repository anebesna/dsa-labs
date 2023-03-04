using System;
using static System.Console;

namespace asd_lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            SLList list = new SLList();
            Write("Enter your space-separated list: ");
            string nodes = ReadLine();
            string[] arr = nodes.Split(' ');
            foreach (string i in arr) list.AddLast(i); 
            while (true)
            {
                try
                {
                    Write("\nEnter the command or press /help to see the available commands: ");
                    string command = ReadLine();
                    if (command == "/help")
                    {
                        Write("/help\n/addfirst\n/addlast\n/addatposition\n/deletefirst\n/deletelast\n/deleteatposition\n/print\n/personaltask\n/exit");
                    }
                    else if (command == "/addfirst")
                    {
                        Write("Enter data: ");
                        string data = ReadLine();
                        list.AddFirst(data);
                    }
                    else if (command == "/addlast")
                    {
                        Write("Enter data: ");
                        string data = ReadLine();
                        list.AddLast(data);
                    }
                    else if (command == "/addatposition")
                    {
                        Write("Enter data: ");
                        string data = ReadLine();
                        Write("Enter the position: ");
                        int pos = int.Parse(ReadLine());
                        list.AddAtPosition(data, pos);
                    }
                    else if (command == "/deletefirst") list.DeleteFirst();
                    else if (command == "/deletelast") list.DeleteLast();
                    else if (command == "/deleteatposition")
                    {
                        Write("Enter the position: ");
                        int pos = int.Parse(ReadLine());
                        list.DeleteAtPosition(pos);
                    }
                    else if (command == "/print") list.Print();
                    else if (command == "/personaltask")
                    {
                        Write("Enter data: ");
                        string data = ReadLine();
                        list.Task(data);
                    }
                    else if (command == "/exit") Environment.Exit(0);
                    else WriteLine("Wrong command");
                    list.Print();
                }
                catch
                {
                    WriteLine("Error");
                }
            }
        }
    }
    class SLList
    {
        public Node head;
        public class Node
        {
            public string data;
            public Node next;
            public Node(string data)
            {
                this.data = data;
            }
        }
        public SLList()
        {
            head = null;
        }
        public void AddFirst(string data)
        {
            Node newnode = new Node(data);
            newnode.next = head;
            head = newnode;
        }
        public void AddAtPosition(string data, int pos)
        {
            if (pos <=0)
            {
                AddFirst(data);
            }
            else
            {
                Node newnode = new Node(data);
                Node current = head;
                for (int i = 1; i < pos - 1; i++)
                {
                    current = current.next;
                    if (current == null)
                    {
                        AddLast(data);
                        break;
                    }
                }
                newnode.next = current.next;
                current.next = newnode;
            }
        }
        public void AddLast(string data)
        {
            Node current = head;
            if (head == null)
            {
                AddFirst(data);
            }
            else
            {
                if (head.next == null)
                {
                    head.next = new Node(data);
                }
                else
                {
                    while (current.next != null)
                    {
                        current = current.next;
                    }
                    current.next = new Node(data);
                }
            }
        }
        public void DeleteFirst()
        {
            head = head.next;
            if (head == null) Write("Your list is empty.");
        }
        public void DeleteAtPosition(int pos)
        {
            if (head == null) Write("Your list is empty.");
            if (pos == 0) DeleteFirst();
            else
            {
                Node current = head;
                for (int i = 1; i < pos - 1; i++)
                {
                    current = current.next;
                    if (current == null) break;
                }
                Node temp = current.next.next;
                current.next = temp;
            }
        }
        public void DeleteLast()
        {
            if (head == null) Write("Your list is empty.");
            Node current = head;
            while (current.next.next != null)
            {
                current = current.next;
            }
            current.next = null;
        }
        public void Print()
        {
            Write("\nCurrent content of the list: ");
            if (head == null) Write("List is empty.");
            else if (head.next == null)
            {
                WriteLine(head.data);
            }
            else
            {
                Node current = head;
                while (current.next != null)
                {
                    Write(current.data + "->");
                    current = current.next;
                }
                Write(current.data);
            } 
        }
        public void Task(string data)
        { 
            Node current = head;
            Node newnode = new Node(data);
            while(current.next != null && current.next.data != newnode.data)
            {
                current = current.next;
            }
            if (current.next == null) AddFirst(data);
            else
            {
                newnode.next = current.next;
                current.next = newnode;
            }
        }
    }
}