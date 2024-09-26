using System;
using System.Collections.Generic;
using System.Numerics;
using Xunit;
using DataProcessor;
using Aspose.Cells;
using System.IO;
using HeroesLibrary;
using System.Reflection;

public class DataProcessorTests
{

    // ����� ��� ������ DataDecriptor

    // ���� �� �������� ����, ��� ����� ���������� ���������� �������� ������
    [Fact]
    public void DataDecryptor_GetDecryptedRecords_ShouldReturnNonEmptyList()
    {
        // ������� �������� ������ ������������� ������
        List<BigInteger> encryptedRecords = new List<BigInteger> { new BigInteger(3) };
        // ������� ������ ��� �������
        List<string> propertyNames = new List<string> { "Property1", "Property2" };

        // ������� ��������� ������ ����������� � ��������� �������
        DataDecryptor decryptor = new DataDecryptor(encryptedRecords, propertyNames);

        // �������� ����� ���������� ��� ��������� �������� ������
        List<List<string>> decryptedRecords = decryptor.GetDecryptedRecords();

        // Assert
        // ���������, ��� ������ �������������� ������ �� ������
        Assert.NotNull(decryptedRecords); // ������ �� ������ ���� null
        Assert.NotEmpty(decryptedRecords); // ������ �� ������ ���� ������
    }

    /// <summary>
    /// ���� ��� �������� ������������ ����������� ����� ������
    /// </summary>
    [Fact]
    public void GetDecryptedRecords_ShouldReturnCorrectNames_WhenSingleRecordIsEncrypted()
    {
        // ����������
        var encryptedRecords = new List<BigInteger> { new BigInteger(3) }; // 3 � �������� ������� - 11, ��� ������������� Property1 � Property2
        var propertyNames = new List<string> { "Property1", "Property2", "Property3" };

        var decryptor = new DataDecryptor(encryptedRecords, propertyNames);

        // ��������
        var result = decryptor.GetDecryptedRecords();

        // ���������
        Assert.NotNull(result);
        Assert.Single(result); // �������, ��� ����� ���� ������
        Assert.Equal(new List<string> { "Property1", "Property2" }, result[0]); // ���������, ��� ����������� �����
    }

    /// <summary>
    /// ���� ��� �������� ������������ ����������� ���������� �������
    /// </summary>
    [Fact]
    public void GetDecryptedRecords_ShouldReturnCorrectNames_WhenMultipleRecordsAreEncrypted()
    {
        // ����������
        var encryptedRecords = new List<BigInteger> { new BigInteger(3), new BigInteger(5) }; // 3 -> Property1, Property2; 5 -> Property1, Property3
        var propertyNames = new List<string> { "Property1", "Property2", "Property3" };

        var decryptor = new DataDecryptor(encryptedRecords, propertyNames);

        // ��������
        var result = decryptor.GetDecryptedRecords();

        // ���������
        Assert.NotNull(result);
        Assert.Equal(2, result.Count); // ������� ��� �������������� ������

        Assert.Equal(new List<string> { "Property1", "Property2" }, result[0]); // ������ ������
        Assert.Equal(new List<string> { "Property1", "Property3" }, result[1]); // ������ ������
    }

    /// <summary>
    /// ���� ��� �������� ������������ ����������� ������, ���������� ������ ���� ���
    /// </summary>
    [Fact]
    public void GetDecryptedRecords_ShouldReturnCorrectName_WhenSingleBitIsSet()
    {
        // ����������
        var encryptedRecords = new List<BigInteger> { new BigInteger(1) }; // 1 � �������� ������� - 01, ��� ������������� ������ Property1
        var propertyNames = new List<string> { "Property1", "Property2", "Property3" };

        var decryptor = new DataDecryptor(encryptedRecords, propertyNames);

        // ��������
        var result = decryptor.GetDecryptedRecords();

        // ���������
        Assert.NotNull(result);
        Assert.Single(result); // �������, ��� ����� ���� ������
        Assert.Equal(new List<string> { "Property1" }, result[0]); // ���������, ��� ����������� �����
    }




    // ����� ��� ������ DataWriter

    // ���� �� �������� ���������� ������ ������������� ������ � ����
    [Fact]
    public void DataWriter_WriteEncryptedDataToFile_ShouldCreateFile()
    {
        // ����������
        // ������� ������ ��� �������
        List<string> propertyNames = new List<string> { "Property1", "Property2" };
        // ������� ������ ������������� �������
        List<BigInteger> encryptedRecords = new List<BigInteger> { new BigInteger(10), new BigInteger(20) };
        // ��������� ���� � ��������� �����
        string filePath = "test_output.txt";

        // �������� ����� ������ ������ � ����
        DataWriter.WriteEncryptedDataToFile(propertyNames, encryptedRecords, filePath);

        // ���������, ��� ���� ��� ������
        Assert.True(File.Exists(filePath));

        // �������� ����� ����� ��������, ����� �� �������� �������
        File.Delete(filePath);
    }




    // ����� ��� ������ DataEncryptor

    [Fact]
    public void DataEncryptor_GetEncryptedRecords_ShouldReturnNonEmptyList()
    {
        // �������� workbook � ������� Aspose.Cells
        Workbook workbook = new Workbook();
        Worksheet worksheet = workbook.Worksheets[0];
        Cells cells = worksheet.Cells;

        // ���������� ������ � ������� ��� �����
        cells[0, 0].PutValue("��������");
        cells[0, 1].PutValue("����� ��������");
        cells[1, 0].PutValue("Item1");
        cells[1, 1].PutValue(5);
        cells[2, 0].PutValue("Item2");
        cells[2, 1].PutValue(10);

        // ������ ��� ����������
        List<List<string>> data = new List<List<string>>()
            {
                new List<string> { "Item1", "5" },
                new List<string> { "Item2", "10" }
            };

        // ����� �������
        List<string> columnNames = new List<string> { "��������", "����� ��������" };

        // ������������� �������
        Dictionary<string, string> mappings = new Dictionary<string, string>()
            {
                { "��������", "Name" },
                { "����� ��������", "ValueCount" }
            };

        // �������� ������� DataEncryptor
        DataEncryptor encryptor = new DataEncryptor(workbook, data, columnNames, mappings);

        // ���������� ������ ��� ����������
        var result = encryptor.GetEncryptedRecords();

        // ��������, ��� ��������� �� ������
        Assert.NotNull(result); // ���������, ��� ��������� �� null
        Assert.NotEmpty(result); // ���������, ��� ������ �� ������
    }

    /// <summary>
    /// ���� �� ��������, ���� �������� ������ ������ ���������� ����������� ��������.
    /// ������ ������� -1.
    /// </summary>
    [Fact]
    public void FindClassIndexForCellValue1()
    {
        // ����������
        var dataProcessor = new DataEncryptor(new Workbook(), new List<List<string>>(), new List<string>(), new Dictionary<string, string>());
        var appropriateValues = new List<List<string>>
        {
            new List<string> { "10", "20", "30" }  // ���������� �������� ��� ������� 0
        };

        // ��������� ���������� ���� _appropriateValues ����� ���������
        var fieldInfo = typeof(DataEncryptor).GetField("_appropriateValues", BindingFlags.NonPublic | BindingFlags.Instance);
        fieldInfo.SetValue(dataProcessor, appropriateValues);

        string cellValue = "5";  // �������� ������, ������ ������������ �����������
        int columnIndex = 0;

        // ���������� ��������� ��� ������ ��������� ������
        var methodInfo = typeof(DataEncryptor).GetMethod("FindClassIndexForCellValue", BindingFlags.NonPublic | BindingFlags.Instance);
        int result = (int)methodInfo.Invoke(dataProcessor, new object[] { cellValue, columnIndex });

        // ���������
        Assert.Equal(-1, result);  // �������, ��� ����� ����� -1
    }

    [Fact]
    public void FindClassIndexForCellValue2()
    {
        // ����������
        var dataProcessor = new DataEncryptor(new Workbook(), new List<List<string>>(), new List<string>(), new Dictionary<string, string>());
        var appropriateValues = new List<List<string>>
        {
            new List<string> { "10", "20", "30", "40", "50" }  // ���������� �������� ��� ������� 0
        };

        // ��������� ���������� ���� _appropriateValues ����� ���������
        var fieldInfo = typeof(DataEncryptor).GetField("_appropriateValues", BindingFlags.NonPublic | BindingFlags.Instance);
        fieldInfo.SetValue(dataProcessor, appropriateValues);

        string cellValue = "50";  // �������� ������
        int columnIndex = 0;

        // ���������� ��������� ��� ������ ��������� ������
        var methodInfo = typeof(DataEncryptor).GetMethod("FindClassIndexForCellValue", BindingFlags.NonPublic | BindingFlags.Instance);
        int result = (int)methodInfo.Invoke(dataProcessor, new object[] { cellValue, columnIndex });

        // ���������
        Assert.Equal(4, result);  // �������, ��� ����� ����� -1
    }

    [Fact]
    public void FindClassIndexForCellValue3()
    {
        // ����������
        var dataProcessor = new DataEncryptor(new Workbook(), new List<List<string>>(), new List<string>(), new Dictionary<string, string>());
        var appropriateValues = new List<List<string>>
        {
            new List<string> { "10", "20", "30", "40", "50" }  // ���������� �������� ��� ������� 0
        };

        // ��������� ���������� ���� _appropriateValues ����� ���������
        var fieldInfo = typeof(DataEncryptor).GetField("_appropriateValues", BindingFlags.NonPublic | BindingFlags.Instance);
        fieldInfo.SetValue(dataProcessor, appropriateValues);

        string cellValue = "45";  // �������� ������
        int columnIndex = 0;

        // ���������� ��������� ��� ������ ��������� ������
        var methodInfo = typeof(DataEncryptor).GetMethod("FindClassIndexForCellValue", BindingFlags.NonPublic | BindingFlags.Instance);
        int result = (int)methodInfo.Invoke(dataProcessor, new object[] { cellValue, columnIndex });

        // ���������
        Assert.Equal(3, result);  // �������, ��� ����� ����� -1
    }

    /// <summary>
    /// ���� ��� ��������, ��� ����� ���������� ���������� ������ ������, ����� ��� ������.
    /// </summary>
    [Fact]
    public void DataEncryptor_GetEncryptedRecords_ShouldReturnEmptyList_WhenNoData()
    {
        // ������� ������ ������� ����� Excel
        var workbook = new Workbook();

        // ������� ������ ������ ������
        var data = new List<List<string>>();

        // ������� ������ ������� ��� ����������
        var mappings = new Dictionary<string, string>();

        // ������� ��������� DataEncryptor � ������� �������
        var encryptor = new DataEncryptor(workbook, data, new List<string>(), mappings);

        // �������� ������������� ������
        var result = encryptor.GetEncryptedRecords();

        // ���������, ��� ��������� �� null � ������
        Assert.NotNull(result);
        Assert.Empty(result);  // ������� ������ ������, ��� ��� ������ ���
    }





    // ����� ��� ������ Hero

    /// <summary>
    /// ���� ��� ��������, ��� ����� ��������� ����������������.
    /// </summary>
    [Fact]
    public void Hero_ShouldInitializeCorrectly()
    {
        // ����������
        var hero = new Hero("John", "100", 10, "magical", 30, "Low");  // ������������� �����

        // �������� ������� �����
        Assert.Equal("John", hero.Name);  // ��������� ��� �����
        Assert.Equal("100", hero.Main_attribute);   // ��������� �������� �����
        Assert.Equal(10, hero.Damage);  // ��������� ����� �����
        Assert.Equal("magical", hero.Attack_type); // ��������� ��� �����
        Assert.Equal(30, hero.Move_speed); // ��������� �������� ��������
        Assert.Equal("Low", hero.Difficulty); // ��������� ������� ���������
    }

    /// <summary>
    /// ���� ��� ��������, ��� ����� ��������� ���������������� � ������� ����������.
    /// </summary>
    [Fact]
    public void Hero_ShouldInitializeCorrectly2()
    {
        // ���������� ������ ��� �����
        string name = "John";
        string mainAttribute = "Strength";
        int damage = 50;
        string attackType = "Melee";
        int moveSpeed = 300;
        string difficulty = "Medium";

        // ������������� �����
        var hero = new Hero(name, mainAttribute, damage, attackType, moveSpeed, difficulty);

        // �������� ������� �����
        Assert.Equal("John", hero.Name);                  // ��������� ��� �����
        Assert.Equal("Strength", hero.Main_attribute);    // ��������� �������� ������� �����
        Assert.Equal(50, hero.Damage);                    // ��������� ���� �����
        Assert.Equal("Melee", hero.Attack_type);          // ��������� ��� ����� �����
        Assert.Equal(300, hero.Move_speed);               // ��������� �������� ������������ �����
        Assert.Equal("Medium", hero.Difficulty);          // ��������� ��������� ���������� ������
    }

    /// <summary>
    /// ���� ��� ��������, ��� ���� ����� ��������� � ���������� ���������.
    /// </summary>
    [Fact]
    public void Hero_ShouldHaveValidDamage()
    {
        // ����������
        var hero = new Hero("John", "Strength", 50, "Melee", 300, "Medium");

        // ��������
        int damage = hero.Damage; // �������� �������� ����� �����

        // �������� ��������� �����
        Assert.InRange(damage, 0, 1000);  // �������, ��� ���� ��������� � ��������� �� 0 �� 1000
    }

    /// <summary>
    /// ���� ��� ��������, ��� �������� ������������ ����� ��������� � ���������� ���������.
    /// </summary>
    [Fact]
    public void Hero_ShouldHaveValidMoveSpeed()
    {
        // ����������
        var hero = new Hero("John", "Agility", 45, "Ranged", 500, "Hard");

        // ��������
        int moveSpeed = hero.Move_speed; // �������� �������� �������� ������������ �����

        // �������� ��������� �������� ������������
        Assert.InRange(moveSpeed, 100, 600);  // �������, ��� �������� ��������� � ��������� �� 100 �� 600
    }

    /// <summary>
    /// ���� ��� �������� ������ ��������� ����� ���� � ������.
    /// </summary>
    [Fact]
    public void Hero_ShouldHandleDifferentAttackTypes()
    {
        // ����������
        var meleeHero = new Hero("MeleeHero", "Strength", 60, "Melee", 350, "Easy");
        var rangedHero = new Hero("RangedHero", "Agility", 40, "Ranged", 400, "Hard");

        // �������� ���� ����� � ����� �������� ���
        Assert.Equal("Melee", meleeHero.Attack_type);   // ������� ��� ����� "Melee"

        // �������� ���� ����� � ����� �������� ���
        Assert.Equal("Ranged", rangedHero.Attack_type); // ������� ��� ����� "Ranged"
    }

    /// <summary>
    /// ���� ��� �������� ������ ��������� ���������� ������.
    /// </summary>
    [Fact]
    public void Hero_ShouldHaveDifficultyLevel()
    {
        // ����������
        var hero = new Hero("John", "Intelligence", 30, "Magic", 280, "Medium");

        // ��������
        string difficulty = hero.Difficulty; // �������� ������� ���������

        // ��������, ��� ������� ��������� �������� ����������
        Assert.True(difficulty == "Easy" || difficulty == "Medium" || difficulty == "Hard", "Difficulty should be valid");
    }
}