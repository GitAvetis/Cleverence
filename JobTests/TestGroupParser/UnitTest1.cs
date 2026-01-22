namespace TestGroupParser
{
    public class UnitTest1
    {
        [Fact]
        public void TestEmptyInput()
        {
            string input = "";
            string output = CleverenceSoftJuniorTest.Task1.GroupParser(input);
            Assert.True(string.IsNullOrEmpty(output));

        }

        [Fact]
        public void TestFromTask()
        {
            string inputLine = "aaabbcccdde";
            string trueResult = "a3b2c3d2e";
            string output = CleverenceSoftJuniorTest.Task1.GroupParser(inputLine);
            Assert.Equal(trueResult, output);
        }

        [Fact]
        public void Test3()
        {
            string inputLine = "abcde";
            string trueResult = "abcde";
            string output = CleverenceSoftJuniorTest.Task1.GroupParser(inputLine);
            Assert.Equal(trueResult, output);
        }
    }
}