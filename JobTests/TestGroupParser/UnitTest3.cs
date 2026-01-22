using Task3;
using static Task3.FormatedLog;

namespace TestGroupParser
{
    public class UnitTest3
    {
        [Fact]
        public void ParseFormat1_ValidLog_ShouldReturnCorrectFormatedLog()
        {
            var formatedLog = new List<string>();
            string log = "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'";
            var result = Task3Prog.ParseLog(log,formatedLog);

            Assert.NotNull(result);
            Assert.Equal("2025-03-10", result!.Date); // после ChangeDateFormat
            Assert.Equal("15:14:49.523", result.Time);
            Assert.Equal(LogLevel.INFO, result.Level);
            Assert.Null(result.Method);
            Assert.Equal("Версия программы: '3.4.0.48729'", result.Message);
        }

        [Fact]
        public void ParseFormat2_ValidLog_ShouldReturnCorrectFormatedLog()
        {
            var formatedLog = new List<string>();
            string log = "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'";
            var result = Task3Prog.ParseLog(log, formatedLog);

            Assert.NotNull(result);
            Assert.Equal("2025-03-10", result!.Date);
            Assert.Equal("15:14:51.5882", result.Time);
            Assert.Equal(LogLevel.INFO, result.Level);
            Assert.Equal("MobileComputer.GetDeviceId", result.Method);
            Assert.Equal("Код устройства: '@MINDEO-M40-D-410244015546'", result.Message);
        }

        // ===================== NEGATIVE TESTS =====================

        [Fact]
        public void ParseFormat1_InvalidLogLevel_ShouldReturnNull()
        {
            var formatedLog = new List<string>();
            string log = "10.03.2025 15:14:49.523 BADLEVEL Сообщение ошибки";
            var result = Task3Prog.ParseLog(log, formatedLog);
            Assert.Null(result);
        }

        [Fact]
        public void ParseFormat2_InvalidTokensCount_ShouldReturnNull()
        {
            var formatedLog = new List<string>();
            string log = "2025-03-10 15:14:51.5882| INFO|11"; // слишком мало токенов
            var result = Task3Prog.ParseLog(log, formatedLog);
            Assert.Null(result);
        }

        [Fact]
        public void ParseFormat1_NotEnoughTokens_ShouldReturnNull()
        {
            var formatedLog = new List<string>();
            string log = "10.03.2025 15:14:49.523"; // нет сообщения
            var result = Task3Prog.ParseLog(log, formatedLog);
            Assert.Null(result);
        }

        [Fact]
        public void ParseFormat2_InvalidLogLevel_ShouldReturnNull()
        {
            var formatedLog = new List<string>();
            string log = "2025-03-10 15:14:51.5882| BADLEVEL|11|MobileComputer.GetDeviceId| Сообщение";
            var result = Task3Prog.ParseLog(log, formatedLog);
            Assert.Null(result);
        }

        [Fact]
        public void ParseLog_InvalidFormat_ShouldWriteToProblemFile()
        {
            string log = "THIS IS A TOTALLY INVALID LOG LINE";
            string root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,"..", "..", ".."));
            string problemFile = Path.Combine(root, "problems.txt");

            // Очистим файл перед тестом
            if (File.Exists(problemFile))
                File.Delete(problemFile);

            Task3Prog.ParseLog(log, new List<string>());

            string content = File.ReadAllText(problemFile);
            Assert.Contains(log, content);
        }
    }
}
