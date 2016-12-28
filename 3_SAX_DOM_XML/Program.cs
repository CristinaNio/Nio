using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace _3_SAX_DOM_XML
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlReaderSettings settings = new XmlReaderSettings();//функциональные возможности для поддержки XmlReader
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.IgnoreWhitespace = true; // игнорироваться незначимые символы-разделители
            settings.IgnoreComments = true;//Игнорируем комм
            settings.CheckCharacters = true; //осуществляется проверка символов

            XmlReader reader = XmlReader.Create("My.xml", settings);
            Console.WriteLine("Я читаю этот файл методом SAX, я прочитал его так:");
         
            while (reader.Read())
            {
                switch (reader.NodeType)//возвращает тип текущего узла
                {
                    case XmlNodeType.Element:
                        Console.Write("<" + reader.Name);
                        while (reader.MoveToNextAttribute())
                        {
                            Console.Write("  " + reader.Name + "=\"" + reader.Value + "\"");
                        }
                        Console.WriteLine(">");
                        break;
                    case XmlNodeType.Text:
                        Console.WriteLine(reader.Value);
                        break;
                    case XmlNodeType.EndElement:
                        Console.Write("</" + reader.Name);
                        Console.WriteLine(">");
                        break;

                }

            }
            reader = XmlReader.Create("My.xml", settings);//создание втрого потока для XmlDocument , чтобы проверить корректроность чтения
              XmlDocument document = new XmlDocument();
              document.Load(reader); //второй поток ушёл на запись
           
              Console.WriteLine("\n\nЯ записал этот файл методом DOM, я записал его так:");
              document.Save(Console.Out);

        //создаём файл
           Console.Write("Новый файл будет записан на какой диск? (укажите имя диска)");
           string name_d = Console.ReadLine();
           Console.Write("Пожалуйста введите имя файла: ");
           string name_file = Console.ReadLine();
           string y = name_d + ":\\" + name_file + ".xml";

           try
           {
               if (File.Exists(y)) { File.Delete(y); }
               FileStream fs = File.Create(y);
               fs.Close();//обязательно
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.ToString());
           }
            //Записываем
           document.Save(y);
           Console.WriteLine("Успешная запись по адресу: " + y);
            
            Console.ReadKey();
        }
    }
}
