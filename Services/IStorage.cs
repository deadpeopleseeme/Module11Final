using Module11Final.Models;


namespace Module11Final.Services
{
    public interface IStorage
    {
        // Получение сессии пользователя по идентификатору
        Session GetSession(long chatId);
    }
}