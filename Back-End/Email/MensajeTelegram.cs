using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Email
{
    public class MensajeTelegram : IMensajeTelegram
    {
        private readonly TelegramBotClient _bot;
        private readonly long _chatId;

        public MensajeTelegram(IConfiguration config)
        {
            _bot = new TelegramBotClient(config["Telegram:BotToken"]!);

            if (!long.TryParse(config["Telegram:ChatId"], out _chatId))
            {
                throw new ArgumentException("ChatId inválido en la configuración.");
            }
        }

        public async Task EnviarTelegram(string texto)
        {
            await _bot.SendTextMessageAsync(
                chatId: _chatId,
                text: texto,
                parseMode: ParseMode.Markdown
            );
        }
    }
}
