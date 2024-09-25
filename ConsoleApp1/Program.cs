using Aspose.Cells;
using DataProcessor;
using System.ComponentModel;
using System.Numerics;

ExcelFile excelFile1 = new ExcelFile("Database1_Data.xlsx");


// ENCRYPTION PROCCESS

Console.WriteLine("===========================================");
Console.WriteLine("|    ENCRYPTION PROCCESS HAS STARTED!     |");
Console.WriteLine("===========================================");

Workbook workbook = new Workbook("Database1.xlsx");
List<List<string>> AppropriateValues = excelFile1.AppropriateValues;
List<string> PropertyNames = excelFile1.PropertyNames;
Dictionary<string, string> NamesAndShortNames = excelFile1.NamesAndShortNames;


DataEncryptor dataEncryptor = new DataEncryptor(workbook, AppropriateValues, PropertyNames, NamesAndShortNames);


List<BigInteger> encryptedRecords = dataEncryptor.GetEncryptedRecords();
int recordCount = 1;
foreach (var encryptedRecord in encryptedRecords)
{
    Console.WriteLine($"Record_#{recordCount}: {encryptedRecord}");
    recordCount++;
}

Console.WriteLine("===========================================");
Console.WriteLine("|                 SUCCESS!                |");
Console.WriteLine("===========================================");

Console.WriteLine();



// DECRYPTION PROCCESS

Console.WriteLine("===========================================");
Console.WriteLine("|    DECRYPTION PROCCESS HAS STARTED!     |");
Console.WriteLine("===========================================");

DataDecryptor dataDecryptor = new DataDecryptor(encryptedRecords, PropertyNames);


List<List<string>> decryptedRecords = dataDecryptor.GetDecryptedRecords();
recordCount = 1;

foreach (var decryptedRecordList in decryptedRecords)
{
    List<string> Records = decryptedRecordList;

    Console.Write($"Record_#{recordCount}");
    foreach (var record in Records)
    {
        Console.Write($" {record}");
    }
    recordCount++;
    Console.WriteLine();
}

Console.WriteLine("===========================================");
Console.WriteLine("|                 SUCCESS!                |");
Console.WriteLine("===========================================");




// Получаем зашифрованные записи
DataEncryptor encryptor = new DataEncryptor(workbook, excelFile1.AppropriateValues, excelFile1.PropertyNames, excelFile1.NamesAndShortNames);
List<BigInteger> encryptedRecords2 = encryptor.GetEncryptedRecords();

// Запись бинарных классов и зашифрованных записей в файл
DataWriter.WriteEncryptedDataToFile(excelFile1.PropertyNames, encryptedRecords2, "output.txt");

