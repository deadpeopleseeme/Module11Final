namespace Module11Final.Services
{
    public static class TwoNumbersCounter
    {
        public static string TwoNumbersCount(string clientMessage)
        {
            string[] numbers = clientMessage.Split(' ');
            string result = "";

            // проверяем, разбивается ли строка на 2, и если да, то числа ли введены в этой строке. числа пусть будут int, в задаче иного не указано
            if (numbers.Length != 2 || !int.TryParse(numbers[0], out int number1) || !int.TryParse(numbers[01], out int number2))
            {
                result = "Ошибка ввода! Только 2 целых числа и только через пробел!";
            }
            else
            {
                result = $"Сумма чисел: {number1 + number2}";
            }
            return result;
        }
    }
}
