namespace SaberTestCS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ListNode> before = CreateSerializeList();
            List<ListNode> after = CreateDeserializeList();
            ListsCheck(before, after);
        }

        static List<ListNode> CreateSerializeList()
        {
            List<ListNode> serializedList = new List<ListNode>();

            ListNode node1 = new() { Data = "node1" };
            ListNode node2 = new() { Data = "node2" };
            ListNode node3 = new() { Data = "node3" };
            ListNode node4 = new() { Data = "node4" };
            ListNode node5 = new() { Data = "node5" };

            node1.Next = node2;
            node1.Previous = null;
            node1.Random = node5;

            node2.Next = node3;
            node2.Previous = node1;
            node2.Random = node3;


            node3.Next = node4;
            node3.Previous = node2;
            node3.Random = node1;

            node4.Next = node5;
            node4.Previous = node3;
            node4.Random = node2;

            node5.Next = null;
            node5.Previous = node4;
            node5.Random = node4;

            ListRandom listRandom = new ListRandom();

            listRandom.Head = node1;
            listRandom.Tail = node5;
            listRandom.Count = 5;

            using (FileStream fileStream = new FileStream
                ("for_Test.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                listRandom.Serialize(fileStream);
            }

            ListNode current = listRandom.Head;

            for (int i = 0; i < 5; i++)
            {
                serializedList.Add(current);
                current = current.Next;
            }

            return serializedList;
        }

        static List<ListNode> CreateDeserializeList()
        {
            List<ListNode> deserializedList = new List<ListNode>();
            ListRandom loadedList = new ListRandom();

            using (FileStream fs = new FileStream("for_Test.txt", FileMode.Open, FileAccess.Read))
            {
                loadedList.Deserialize(fs);
            }
            ListNode current = loadedList.Head;

            for (int i = 0; i < loadedList.Count; i++)
            {
                deserializedList.Add(current);
                current = current.Next;
            }

            return deserializedList;
        }

        static void ListsCheck(List<ListNode> listBefore, List<ListNode> listAfter)
        {
            if(listBefore.Count != listAfter.Count)
            {
                Console.WriteLine("Error: Lists have different lengths.");
                return;
            }
            for (int i = 0; i < listBefore.Count; i++)
            {
                ListNode beforeNode = listBefore[i];
                ListNode afterNode = listAfter[i];
                if (beforeNode.Data != afterNode.Data)
                {
                    Console.WriteLine($"Error: Data mismatch at index {i}.");
                    return;
                }
                int beforeRandomIndex = listBefore.IndexOf(beforeNode.Random);
                int afterRandomIndex = listAfter.IndexOf(afterNode.Random);
                if (beforeRandomIndex != afterRandomIndex)
                {
                    Console.WriteLine($"Error: Random pointer mismatch at index {i}.");
                    return;
                }
            }
            Console.WriteLine("Success: Lists are identical.");
        }

    }
}
