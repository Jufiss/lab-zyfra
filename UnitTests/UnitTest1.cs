using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace UnitTests
{
    public class UnitTest1
    {
        private const string TestFilePath = "state.txt";
        public UnitTest1()
        {
            File.WriteAllLines(TestFilePath, new[]
            {
                "1 = value1",
                "2 = value2"
            });
        }

        // проверка отсутствия файла
        [Fact]
        public void FileDoesNotExist()
        {
            // Arrange
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }

            // Act
            var result = Program.ReadState(TestFilePath);

            // Assert
            Assert.Empty(result);
        }

        // проверка корректности чтения данных из файла
        [Fact]
        public void CorrectReadDictionary()
        {
            // Arrange
            var expected = new Dictionary<string, string>
        {
            { "1", "value1" },
            { "2", "value2" }
        };

            // Act
            var result = Program.ReadState(TestFilePath);

            // Assert
            Assert.Equal(expected, result);
        }

        // проверка обновления значения в словаре
        [Fact]
        public void UpdateValueInDictionary()
        {
            // Arrange
            var initialState = Program.ReadState(TestFilePath);
            string idToUpdate = "1";
            string newValue = "updated_value";

            // Act
            Program.UpdateState(TestFilePath, initialState, idToUpdate, newValue);

            // Assert
            Assert.Equal(newValue, initialState[idToUpdate]);
        }

        // проверка обновления значения в файле
        [Fact]
        public void UpdateFile()
        {
            // Arrange
            var initialState = Program.ReadState(TestFilePath);
            string idToUpdate = "1";
            string newValue = "updated_value";

            // Act
            Program.UpdateState(TestFilePath, initialState, idToUpdate, newValue);

            // Assert
            var updatedState = Program.ReadState(TestFilePath);
            Assert.Equal(newValue, updatedState[idToUpdate]);
        }
    }
}