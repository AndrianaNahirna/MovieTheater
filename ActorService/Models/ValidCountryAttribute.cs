using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ActorService.Validation
{
    public class ValidCountryAttribute : ValidationAttribute
    {
        private readonly string[] _allowedCountries = new[]
        {
            "Австралія", "Австрія", "Азербайджан", "Албанія", "Алжир", "Ангола", "Аргентина", "Афганістан", "Бельгія", "Болгарія", "Бразилія", "Ватикан", "Великобританія", "В'єтнам", "Вірменія", "Греція", "Грузія", "Данія", "Естонія", "Єгипет", "Ізраїль", "Індія", "Індонезія", "Ірак", "Іран", "Ірландія", "Ісландія", "Іспанія", "Італія", "Казахстан", "Канада", "Китай", "Кіпр", "Колумбія", "Куба", "Латвія", "Литва", "Люксембург", "Мексика", "Молдова", "Монако", "Нідерланди", "Німеччина", "Норвегія", "ОАЕ", "Пакистан", "Південна Корея", "Польща", "Португалія", "Румунія", "Словаччина", "Словенія", "США", "Таїланд", "Туреччина", "Угорщина", "Узбекистан", "Україна", "Фінляндія", "Франція", "Хорватія", "Чехія", "Чилі", "Швейцарія", "Швеція", "Японія"
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string inputCountry = value.ToString();

            if (!_allowedCountries.Contains(inputCountry))
            {
                return new ValidationResult("Оберіть існуючу країну із запропонованого списку.");
            }

            return ValidationResult.Success;
        }
    }
}