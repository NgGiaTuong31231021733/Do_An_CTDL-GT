using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace do_an_CTDL_GT
{
    public class Node
    {
        public object element; 
        public Node link; 

        public Node()
        {
            this.element = null;
            this.link = null;
        }

        public Node(object element)
        {
            this.element = element;
            this.link = null;
        }
    }

    public class LinkedList
    {
        public Node header;

        public LinkedList()
        {
            header = new Node("header");
        }

        public Node Find(object ele)
        {
            Node current = header;
            while (!current.element.Equals(ele) && current.link != null)
                current = current.link;
            return current;
        }

        public void Insert(object newele, object after)
        {
            Node current = Find(after);
            Node newNode = new Node(newele);
            newNode.link = current.link;
            current.link = newNode;
        }

        public Node FindPrev(object ele)
        {
            Node current = header;
            while (current.link != null && !current.link.element.Equals(ele))
                current = current.link;
            return current;
        }

        public void Remove(object ele)
        {
            Node current = FindPrev(ele);
            if (current.link != null)
                current.link = current.link.link;
        }

        public void Print()
        {
            Node current = header.link;
            while (current != null)
            {
                Console.WriteLine(current.element);
                current = current.link;
            }
        }
    }

    class TrelloBoard
    {
        public LinkedList toDoList;
        public LinkedList inProgressList;
        public LinkedList doneList;
        public List<LinkedList> allLists;
        public TrelloBoard()
        {
            toDoList = new LinkedList();
            inProgressList = new LinkedList();
            doneList = new LinkedList();
            allLists = new List<LinkedList> { toDoList, inProgressList, doneList };

        }

        public void AddCard(string title, string listName)
        {
            switch (listName.ToLower())
            {
                case "to do":
                    toDoList.Insert(title, "header");
                    break;
                case "in progress":
                    inProgressList.Insert(title, "header");
                    break;
                case "done":
                    doneList.Insert(title, "header");
                    break;
                default:
                    Console.WriteLine("Invalid list name.");
                    break;

            }
        }

        public void RemoveCard(string title)
        {
            toDoList.Remove(title);
            inProgressList.Remove(title);
            doneList.Remove(title);
            foreach (var list in allLists)
            {
                if (list != toDoList && list != inProgressList && list != doneList)
                {
                    list.Remove(title);
                }
            }
        }
    

        public void AddList(string listName)
        {
            if (listName != "")
            {
                LinkedList newList = new LinkedList();
                newList.header.element = listName;
                allLists.Add(newList);
                Console.WriteLine($"List '{listName}' được thêm vào bảng.");
            }
            else
            {
                Console.WriteLine("List name cannot be empty.");
            }

        }

        public void AddCardToNewList(string title, string newListName)
        {
            LinkedList newList = allLists.FirstOrDefault(list => list.header.element.ToString().ToLower() == newListName.ToLower());

            if (newList == null)
            {
                Console.WriteLine($"Danh sách '{newListName}' không tồn tại.");
                return;
            }

            newList.Insert(title, "header");
        }


        public void PrintBoard()
        {
            Console.WriteLine("Trello Board:");
            Console.WriteLine("\n-----------------------------------");
            Console.WriteLine("To Do:");
            toDoList.Print();
            Console.WriteLine("\n-----------------------------------");
            Console.WriteLine("In Progress:");
            inProgressList.Print();
            Console.WriteLine("\n-----------------------------------");
            Console.WriteLine("Done:");
            doneList.Print();
            foreach (var list in allLists)
            {
                if (list != toDoList && list != inProgressList && list != doneList)
                {
                    Console.WriteLine("\n-----------------------------------");
                    Console.WriteLine($"\n{list.header.element}:");
                    list.Print();
                    Console.WriteLine("\n-----------------------------------");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            System.Console.OutputEncoding = Encoding.UTF8;
            System.Console.InputEncoding = Encoding.UTF8;

            TrelloBoard board = new TrelloBoard();

            // Các thẻ trong Trello Board
            board.AddCard("Task 1", "To Do");
            board.AddCard("Task 2", "In Progress");
            board.AddCard("Task 3", "Done");

            // Hiển thị bảng Trello
            board.PrintBoard();

            // ++Thêm danh sách mới
            Console.WriteLine("\nTạo một danh sách mới:");
            string newListName = Console.ReadLine();
            board.AddList(newListName);
            board.PrintBoard();
            Console.ReadKey();

            // ++Thêm thẻ
            Console.WriteLine("\nNhập tên thẻ bạn muốn thêm vào danh sách 'To Do':");
            string newTask = Console.ReadLine();
            board.AddCard(newTask, "To Do");

            Console.WriteLine("\nNhập tên thẻ bạn muốn thêm vào danh sách 'In Progress':");
            newTask = Console.ReadLine();
            board.AddCard(newTask, "In Progress");

            Console.WriteLine("\nNhập tên của thẻ bạn muốn thêm vào danh sách 'Done':");
            newTask = Console.ReadLine();
            board.AddCard(newTask, "Done");

            // ++Thêm thẻ vào danh sách mới
            Console.WriteLine($"\nNhập tên thẻ bạn muốn thêm vào danh sách '{newListName}':");
            string newTaskForNewList = Console.ReadLine();
            board.AddCardToNewList(newTaskForNewList, newListName);
            board.PrintBoard();

            // ++Xóa một thẻ
            Console.WriteLine("\nNhập tên thẻ bạn muốn xóa:");
            string title = Console.ReadLine();
            board.RemoveCard(title);
            board.PrintBoard();
            Console.ReadKey();

        }
    }
}

