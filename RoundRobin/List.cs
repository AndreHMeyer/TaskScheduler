using System;

namespace RoundRobin
{
    public class List
    {
        /// <summary>
        /// Nó da lista encadeada.
        /// </summary>
        public class Node
        {
            public Task Task { get; set; }
            public Node Next { get; set; }
        }

        /// <summary>
        /// Insere uma tarefa no início da lista.
        /// </summary>
        /// <param name="head">O primeiro nó da lista.</param>
        /// <param name="task">A tarefa a ser inserida.</param>
        public static void Insert(ref Node head, Task task)
        {
            Node newNode = new Node();
            newNode.Task = task;
            newNode.Next = head;
            head = newNode;
        }

        /// <summary>
        /// Remove uma tarefa da lista.
        /// </summary>
        /// <param name="head">O primeiro nó da lista.</param>
        /// <param name="task">A tarefa a ser removida.</param>
        public static void Delete(ref Node head, Task task)
        {
            Node current = head;
            Node previous = null;

            //Verifica se a lista está vazia
            if (current == null)
            {
                throw new InvalidOperationException("A lista está vazia.");
            }

            //A tarefa a ser removida é a primeira da lista
            if (current.Task == task)
            {
                head = current.Next;
                return;
            }

            //Percorre a lista procurando a tarefa
            while (current != null && current.Task != task)
            {
                previous = current;
                current = current.Next;
            }

            //Se a tarefa não foi encontrada
            if (current == null)
            {
                throw new ArgumentException("A tarefa especificada não foi encontrada na lista.");
            }

            //Remove a tarefa da lista
            previous.Next = current.Next;
        }

        /// <summary>
        /// Percorre e exibe o conteúdo da lista.
        /// </summary>
        /// <param name="head">O primeiro nó da lista.</param>
        public static void Traverse(Node head)
        {
            if (head == null)
            {
                Console.WriteLine("A lista está vazia.");
                return;
            }

            Node current = head;
            while (current != null)
            {
                Console.WriteLine($"[{current.Task.Name}] [{current.Task.Priority}] [{current.Task.Burst}]");
                current = current.Next;
            }
        }

        public static bool Contains(Node head, Task task)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Task == task)
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

    }
}
