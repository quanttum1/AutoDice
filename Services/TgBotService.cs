// using Telegram.Bot;
// using Telegram.Bot.Args;
// using System.Threading.Tasks;
//
// public class TgBotService
// {
//     private readonly TelegramBotClient _botClient;
//     private readonly string _token = "your-bot-token";
//
//     public TelegramBotService()
//     {
//         _botClient = new TelegramBotClient(_token);
//         _botClient.OnMessage += Bot_OnMessage;
//         _botClient.StartReceiving();
//     }
//
//     private async void Bot_OnMessage(object sender, MessageEventArgs e)
//     {
//         if (e.Message.Text != null)
//         {
//             // Handle messages here
//             await _botClient.SendTextMessageAsync(e.Message.Chat.Id, "Hello from ASP.NET Bot!");
//         }
//     }
//
//     public void Stop()
//     {
//         _botClient.StopReceiving();
//     }
// }

