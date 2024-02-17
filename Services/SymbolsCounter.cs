namespace Module11Final.Services
{
    public static class SymbolsCounter
    {
        /* в данной реализации кажется, будто почти нет смысла выносить это в отдельный класс, и можно посчитать прямо в хэндлере,
        но это явно имеет смысл для соблюдения архитектуры + в будущем функционал сервиса может расширяться*/

        public static string SymbolsCount(string clientMessage)
        {
            return $"Длина сообщения,  знаков: {clientMessage.Length}";
        }
    }
}
