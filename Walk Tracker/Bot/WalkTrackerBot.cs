using Viber.Bot;

namespace Walk_Tracker.Bot
{
    public class WalkTrackerBot
    {
        private readonly WalkTrackerBotClient _botClient;

        public WalkTrackerBot(WalkTrackerBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task Start()
        {
            await _botClient.SetMessageReceiverAsync(OnMessageReceived);
            await _botClient.SetWebhookAsync();
        }

        private async void OnMessageReceived(object sender, MessageEventArgs e)
        {
            // Handle user input
            if (e.Message.Type == MessageType.Text)
            {
                string input = e.Message.Text;

                // If user sends "start" command, ask for IMEI
                if (input == "/start")
                {
                    await _botClient.SendTextMessageAsync(e.Message.From.Id, "Please enter your IMEI:");
                }
                else
                {
                    // Otherwise, ask user to enter "/start" command
                    await _botClient.SendTextMessageAsync(e.Message.From.Id, "Please enter '/start' command to start the bot.");
                }
            }
        }

        private async void OnCallbackQueryReceived(object sender, CallbackQueryEventArgs e)
        {
            // Handle button clicks
            string data = e.CallbackQuery.Data;

            // If user clicks "Top 10" button
            if (data == "top10")
            {
                // Call API to get top 10 walks by user's IMEI
                string imei = GetImeiForUser(e.CallbackQuery.From.Id); // Implement this method to get the user's IMEI

                var client = new HttpClient();
                var response = await client.GetAsync($"https://example.com/api/{imei}/top10");
                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse the response and send the result to the user
                // ...

                await _botClient.SendTextMessageAsync(e.CallbackQuery.From.Id, "Here are your top 10 walks:");
            }
        }
    }
}
