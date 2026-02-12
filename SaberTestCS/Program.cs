namespace SaberTestCS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ListRandom loadedList = new ListRandom();
            ListRandom loadedListForFile = new ListRandom();
            string path = "../../../Deserialize.txt";
            string emptyFilePath = "for_Test.txt";

            List<ListNode> before = CreateSerializeList(emptyFilePath);
            List<ListNode> beforeFromFile = CreateSerializeList(path);
            List<ListNode> after = CreateDeserializeList(loadedList);
            List<ListNode> afterFromFile = CreateDeserializeList(loadedListForFile, path);

            ListsCheck(before, after);
            ListsCheck(beforeFromFile, afterFromFile);
        }

        static List<ListNode> CreateSerializeList(string path)
        {
            List<ListNode> serializedList = new List<ListNode>();
            ListRandom listRandom = new ListRandom();

            if (path == "for_Test.txt")
            {
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

                listRandom.Head = node1;
                listRandom.Tail = node5;
                listRandom.Count = 5;

            }
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    listRandom.Deserialize(fs);
                }
            }

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                listRandom.Serialize(fs);
            }

            ListNode current = listRandom.Head;

            for (int i = 0; i < listRandom.Count; i++)
            {
                serializedList.Add(current);
                current = current.Next;
            }

            return serializedList;
        }

        static List<ListNode> CreateDeserializeList(ListRandom loadedList, string path = "for_Test.txt")
        {
            List<ListNode> deserializedList = new List<ListNode>();

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
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
            if (listBefore.Count != listAfter.Count)
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
