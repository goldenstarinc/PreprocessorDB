using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesLibrary;
using Aspose.Cells;

namespace DataProcessor
{
    public class DataProcessor
    {
        private Encoding _alternateEncoding = Encoding.GetEncoding("IBM437");
        // Метод для чтения базы данных из Excel-файла
        public static List<Hero> ReadHeroesFromExcel(string filePath)
        {
            List<Hero> heroes = new List<Hero>();

            // Открываем Excel-файл
            Workbook workbook = new Workbook(filePath);
            Worksheet worksheet = workbook.Worksheets[0];

            // Получаем диапазон данных
            Cells cells = worksheet.Cells;

            for (int i = 1; i <= cells.MaxDataRow; i++)
            {
                // Читаем значения из каждой ячейки строки
                string name = cells[i, 0].StringValue;
                string mainAttribute = cells[i, 1].StringValue;
                int damage = cells[i, 2].IntValue;
                string attackType = cells[i, 3].StringValue;
                int moveSpeed = cells[i, 4].IntValue;
                string difficulty = cells[i, 5].StringValue;

                // Создаем объект Hero и добавляем его в список
                var hero = new Hero(name, mainAttribute, damage, attackType, moveSpeed, difficulty);
                heroes.Add(hero);
            }

            return heroes;
        }
    }
}
