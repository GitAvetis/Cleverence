using System.Text;

namespace SaberTestCS
{
    internal class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(Stream s)
        {
            s.Write(Encoding.UTF8.GetBytes($"{Count}\n"));//первым токеном запишу длину списка

            byte[] data;

            Dictionary<ListNode, int> listDictionary = new();//на этом этапе раздал id для всех узлов,
                                                             //чтобы обращаться к ним черз этот словарь по ListNode.Random
            ListNode node = Head;

            for (int i = 0; i < Count; i++)
            {
                if (node != null)
                {
                    listDictionary[node] = i;
                    node = node.Next;
                }
            }

            node = Head;

            for (int i = 0; i < Count; i++)
            {
                int randIndex = listDictionary[node.Random];
                string listInfo = $"{i}|{node.Data}|{randIndex}\n";
                data = Encoding.UTF8.GetBytes(listInfo);
                s.Write(data, 0, data.Length);
                node = node.Next;
            }

        }

        public void Deserialize(Stream s)
        {
            s.Position = 0;
            byte[] stringFromBase = new byte[s.Length];
            s.Read(stringFromBase, 0, stringFromBase.Length);

            if (stringFromBase == null)
                throw new Exception("Поток не содержит данных");

            Dictionary<int, ListNode> listDictionary = new();

            string[] allTokens = Encoding.UTF8.GetString(stringFromBase).Split('\n');

            if (allTokens.Length < 2)
                throw new Exception("Поток не содержит данных для десериализации");

            Count = Convert.ToInt32(allTokens[0]);
            if (Count == 0)
                throw new Exception("Поток не содержит данных для десериализации");


            for (int i = 0; i < Count; i++)
            {
                listDictionary[i] = new ListNode();
            }

            ListNode node;

            Head = listDictionary[0];
            Tail = listDictionary[Count - 1];

            int nodeDataInd = 1;
            int randInd = 2;
            //allTokens[0] содержит Count, поэтому первое значение начинаем с первого индекса
            for (int i = 0; i < Count; i++)
            {
                string[] info = allTokens[i + 1].Split('|');
                node = listDictionary[i];
                node.Data = info[nodeDataInd];

                if (int.TryParse(info[randInd], out int random))
                    node.Random = listDictionary[random];
                else
                    node.Random = null;

                if (i == 0)
                    node.Previous = null;
                else
                    node.Previous = listDictionary[i - 1];

                if (i < Count - 1)
                    node.Next = listDictionary[i + 1];
                else node.Next = null;
            }
        }
    }
}
