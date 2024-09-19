using DataProcessor;

// Выводим на экран всех героев
var heroes = DataProcessor.DataProcessor.ReadHeroesFromExcel("Database1.xlsx");
foreach (var hero in heroes)
{
    Console.WriteLine(hero);
}
