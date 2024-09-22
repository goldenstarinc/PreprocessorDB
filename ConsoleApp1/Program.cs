using Aspose.Cells;
using DataProcessor;
using System.ComponentModel;
using System.Numerics;

ExcelFile excelFile1 = new ExcelFile("Database1_Data.xlsx");


Workbook workbook = new Workbook("Database1.xlsx");
List<List<string>> AppropriateValues = excelFile1.AppropriateValues;
List<string> PropertyNames = excelFile1.PropertyNames;
Dictionary<string, string> NamesAndShortNames = excelFile1.NamesAndShortNames;


DataEncryptor dataEncryptor = new DataEncryptor(workbook, AppropriateValues, PropertyNames, NamesAndShortNames);


List<BigInteger> encryptedRecords = dataEncryptor.GetEctryptedRecords();
foreach (var encryptedRecord in encryptedRecords)
{
    Console.WriteLine($"{encryptedRecord}");
}


