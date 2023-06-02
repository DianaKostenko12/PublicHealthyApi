using HealthyApi.Data.Entities;

namespace HealthyApi.Services.CalorieServices
{
    public interface ICalorieService
    {
        Task<string> GetCaloriesByFoodAsync(string food);
        Task<string> GetAdviceAboutRationAsync(string rationOfPerson);
        Task<string> GetCaloriesADayAsync(User user);
        Task<string> GetDietForAWeekAsync(User user);
    }
}
