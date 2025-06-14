using System;
using System.Collections.Generic;
using UsersLoaderPlugin;

class Program
{
    static void Main()
    {
        var plugin = new Plugin();
        var emptyList = new List<PhoneApp.Domain.DTO.DataTransferObject>();
        var result = plugin.Run(emptyList);

        Console.WriteLine("Результат плагина:");
        foreach (var item in result)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("Готово.");
    }
}
