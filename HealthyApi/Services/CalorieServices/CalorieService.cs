using Microsoft.VisualBasic;
using OpenAI_API.Completions;
using OpenAI_API;
using HealthyApi.Data.Entities;
using HealthyApi.Data;

namespace HealthyApi.Services.CalorieServices
{
    public class CalorieService : ICalorieService
    {
        private readonly DataContext _context;
        public CalorieService(DataContext context)
        {
            _context = context;
        }

        public async Task<string> GetCaloriesByFoodAsync(string food)
        {
            string OutPutResult = "";
            var openAI = new OpenAIAPI(Constants.GPTkey);
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = $"The number of kilocalories of {food} ( as short as possible, only a number)";
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;

            var completions = await openAI.Completions.CreateCompletionAsync(completionRequest);
            foreach (var completion in completions.Completions)
            {
                OutPutResult += completion.Text;
            }

            return OutPutResult;
        }

        public async Task<string> GetAdviceAboutRationAsync(string rationOfPerson)
        {
            string result = "";
            var openAI = new OpenAIAPI(Constants.GPTkey);
            CompletionRequest completionRequest = new CompletionRequest();
            if (!(rationOfPerson.Contains("Breakfast")
                && rationOfPerson.Contains("Dinner")
                && rationOfPerson.Contains("Lunch")))
            {
                return "Not corect info";
            }

            completionRequest.Prompt = $"Give advice on this person's diet:{rationOfPerson} (Answer as briefly as possible)";
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 1000;

            var completions = await openAI.Completions.CreateCompletionAsync(completionRequest);

            foreach (var completion in completions.Completions)
            {
                result += completion.Text;
            }

            return result;
        }
        public async Task<string> GetCaloriesADayAsync(User user)
        {
            string OutPutResult = "";
            var openAI = new OpenAIAPI(Constants.GPTkey);
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = $"Write count of calories to eat for a day for a person with the following data: {user.Sex}, {user.Height} cm tall, {user.Weight} kg, age {user.Age}. " +
                $" Answer as briefly as possible, without additions, without spaces, with unit of measurement";

            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 1000;

            var completions = await openAI.Completions.CreateCompletionAsync(completionRequest);

            foreach (var completion in completions.Completions)
            {
                OutPutResult += completion.Text;
            }

            return OutPutResult;
        }

        public async Task<string> GetDietForAWeekAsync(User user)
        {
            string OutPutResult = "";

            var openAI = new OpenAIAPI(Constants.GPTkey);
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = $"Write a diet for a week (Mon, Wed, Thu, Ch, Fri, Sat, Sun) for a person with the following data: woman, 178 cm tall, 67 kg, age 18. Write the diet according to the following pattern: Monday\r\n•\tBreakfast [Placeholder]\r\n•\tLunch: [Placeholder]\r\n•\tDinner: [Placeholder]\r\nTuesday\r\n•\tBreakfast: [Placeholder]\r\n•\tLunch: [Placeholder]\r\n•\tDinner: [Placeholder]\r\nWednesday\r\n•\tBreakfast: [Placeholder]\r\n•\tLunch: [Placeholder]\r\n•\tDinner: [Placeholder]\r\nThursday\r\n•\tBreakfast: [Placeholder]\r\n•\tLunch: [Placeholder]\r\n•\tDinner: [Placeholder]\r\nFriday\r\n•\tBreakfast: [Placeholder]\r\n•\tLunch: [Placeholder]\r\n•\tDinner: [Placeholder]\r\nSaturday\r\n•\tBreakfast: [Placeholder]\r\n•\tLunch: [Placeholder]\r\n•\tDinner: [Placeholder]\r\nSunday\r\n•\tBreakfast: [Placeholder]\r\n•\tLunch: [Placeholder]\r\n•\tDinner: [Placeholder]\r\nthe appropriate text instead of the placeholder\r\n. Answer as briefly as possible, without additions, " +
                $"without spaces ( and please stand instead of the symbol of transition to a new line a symbol \n";
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 1000;
            var completions = await openAI.Completions.CreateCompletionAsync(completionRequest);

            foreach (var completion in completions.Completions)
            {
                OutPutResult += completion.Text;
            }

            return (OutPutResult);
        }
    }
}
