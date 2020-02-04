using ConsoleAppTest.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleAppTest
{
    
    /// <summary>
    /// Класс с логикой
    /// </summary>
   public  class Bl

    {
        string pathXml = @"companies.xml";
        string pathXmlCloseInfo = @"closeinfo.xml";
        string pathXmlEntrepreneurs = @"entrepreneurs.xml";

        //действующие компании
        int companesTrue;

        //лист для хранения обьектов Company
        List<Company> companys = new List<Company>();

        List<CloseInfo> closeinfoList = new List<CloseInfo>();
        List<Entrepreneurs> entrepreneursList = new List<Entrepreneurs>();

        /// <summary>
        /// Чтение файла  xml
        ///
        /// </summary>
        public void ReadingXml()
        {
            
            XmlDocument xDoc = new XmlDocument();

            try
            {
             //загружаем хмл
             xDoc.Load(pathXml);

            // получим корневой элемент
            XmlElement xRoot = xDoc.DocumentElement;

                // обход всех узлов в корневом элементе
                foreach (XmlNode xnode in xRoot)
                {//обьект для заполнения данными из листа
                        Company сompany = new Company();

                    // получаем атрибут name
                    if (xnode.Attributes.Count > 0)

                    { 

                        XmlNode attr = xnode.Attributes.GetNamedItem("Name");
                        if (attr != null)
                        {
                            //заполняем обьект 
                            //  сompany.Name = attr.Value; 
                            //  Console.WriteLine(attr.Value);
                        }
                    }


                    // обходим все дочерние узлы элемента user
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        // если узел Id
                        if (childnode.Name == "Id")
                        {
                            сompany.Id = int.Parse(childnode.InnerText);
                            Console.WriteLine($"ID {childnode.InnerText}");
                        }

                        // если узел Name
                        if (childnode.Name == "Name")
                        {
                            сompany.Name = childnode.InnerText;
                            Console.WriteLine($"Название компании: {childnode.InnerText}");
                        }

                        // если узел - доход
                        if (childnode.Name == "Profit")
                        {
                            сompany.Profit = double.Parse(childnode.InnerText);
                            Console.WriteLine($"Доход компании: {childnode.InnerText}");
                        }

                        // если узел Name
                        if (childnode.Name == "CreationDate")
                        {
                            
                            Console.WriteLine($"Открытие компании произошло: {childnode.InnerText}");
                            сompany.CreationDate = DateTime.Parse(childnode.InnerText);
                        }

                        // если узел Name
                        if (childnode.Name == "ReportDate")
                        {
                            сompany.ReportDate = DateTime.Parse(childnode.InnerText);
                            Console.WriteLine($"Дата текущего отчета: {childnode.InnerText}\t\n");
                        }

                    }
                   companys.Add(сompany); // при каждой итераци добавляем в список новый обьект
                }
                
              Console.WriteLine();
               
            foreach (Company com in companys)
            {
                Console.WriteLine($"{com.Id} ({com.Name}) - {com.Profit}  - {com.CreationDate}  - {com.ReportDate}");
            }
               
                companesTrue = companys.Count(); // получение действующих компаний
                Console.WriteLine($"Количество действующих компаний = {companesTrue}");

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Что то пошло не так...Надо разбиратся...\t\n{ex}");
            }

            Console.Read();
        }

        //выборка из xml
        /// <summary>
        /// чтение хml файла Закрытых фирм
        /// </summary>
        public void ReadCloseXmil()
        {
            XmlDocument xDoc = new XmlDocument();

            DateTime temData = DateTime.Now ;

            try
            {
                //загружаем хмл
                xDoc.Load(pathXmlCloseInfo);

                // получим корневой элемент
                XmlElement xRoot = xDoc.DocumentElement;

                // обход всех узлов в корневом элементе
                foreach (XmlNode xnode in xRoot)
                {
                    //обьект для заполнения данными из листа
                    CloseInfo сloseInfo = new CloseInfo();

                    // получаем атрибут name
                    if (xnode.Attributes.Count > 0)

                    {

                        XmlNode attr = xnode.Attributes.GetNamedItem("Name");
                        if (attr != null)
                        {
                            //заполняем обьект 
                            //  сompany.Name = attr.Value; 
                            //  Console.WriteLine(attr.Value);
                        }
                    }


                    // обходим все дочерние узлы элемента user
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        // если узел Id
                        if (childnode.Name == "Id")
                        {
                            сloseInfo.Id = int.Parse(childnode.InnerText);
                            Console.WriteLine($"ID {childnode.InnerText}");
                        }

                        // если узел закрытия организации
                        if (childnode.Name == "CloseDate")
                        {
                            сloseInfo.CloseDate = DateTime.Parse(childnode.InnerText);
                            temData = DateTime.Parse(childnode.InnerText);
                            Console.WriteLine($"дата закрытия  компании: {childnode.InnerText}");
                        }

                        сloseInfo.TimeDayClose = JobDateTTime(temData); //вычитаем даты
                        Console.WriteLine($"Дата закрытия организации: {сloseInfo.TimeDayClose}\t\n");
                     

                    }
                    closeinfoList.Add(сloseInfo); // при каждой итераци добавляем в список новый обьект
                    SaveDanni(сloseInfo, "CloseInfoItems"); // запись в json
                }

                Console.WriteLine();

                foreach (CloseInfo com in closeinfoList)
                {
                    Console.WriteLine($"Номер ID :{com.Id} - Дата закрытия:{com.CloseDate}  - Количество дней существования фирмы {com.TimeDayClose}");
                }

                //Console.WriteLine($"Количество действующих компаний = {companesTrue}");

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Что то пошло не так...Надо разбиратся...\t\n{ex}");
            }

            Console.Read();
        }

        /// <summary>
        /// Чтение файла об ИП
        /// </summary>
        public void ReadEntrepreneurs()
        {
            XmlDocument xDoc = new XmlDocument();

            DateTime temData = DateTime.Now;

            try
            {
                //загружаем хмл
                xDoc.Load(pathXmlEntrepreneurs);

                // получим корневой элемент
                XmlElement xRoot = xDoc.DocumentElement;

                // обход всех узлов в корневом элементе
                foreach (XmlNode xnode in xRoot)
                {
                    //обьект для заполнения данными из листа
                    Entrepreneurs entrepreneurs = new Entrepreneurs();

                    // получаем атрибут name
                    if (xnode.Attributes.Count > 0)

                    {

                        XmlNode attr = xnode.Attributes.GetNamedItem("Name");
                        if (attr != null)
                        {
                            //заполняем обьект 
                            //  сompany.Name = attr.Value; 
                            //  Console.WriteLine(attr.Value);
                        }
                    }


                    // обходим все дочерние узлы элемента user
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        // если узел Id
                        if (childnode.Name == "Id")
                        {
                            entrepreneurs.Id = int.Parse(childnode.InnerText);
                            Console.WriteLine($"ID {childnode.InnerText}");
                        }

                        // если узел первое имя организации
                        if (childnode.Name == "FirstName")
                        {
                            entrepreneurs.FirstName = childnode.InnerText;
                            Console.WriteLine($"Название Ипешника: {childnode.InnerText}");
                        }

                        // если узел второе имя организации
                        if (childnode.Name == "SecondName")
                        {
                            entrepreneurs.SecondName = childnode.InnerText;
                            Console.WriteLine($"Втрое Название Ипешника: {childnode.InnerText}");
                        }

                        // если узел  доход ИП
                        if (childnode.Name == "Profit")
                        {
                            entrepreneurs.Profit = double.Parse(childnode.InnerText);
                            Console.WriteLine($"Доход организации: {childnode.InnerText}");
                        }

                        // если узел Открытия организации
                        if (childnode.Name == "CloseDate")
                        {
                            entrepreneurs.CreationDate = DateTime.Parse(childnode.InnerText);
                            temData = DateTime.Parse(childnode.InnerText);
                            Console.WriteLine($"Дата открытия  компании: {childnode.InnerText}");
                        }

                        // если узел Дата выгрузки отчета организации
                        if (childnode.Name == "ReportDate")
                        {
                            entrepreneurs.ReportDate = DateTime.Parse(childnode.InnerText);
                            temData = DateTime.Parse(childnode.InnerText);
                            Console.WriteLine($"Дата выгрузки отчета  компании: {childnode.InnerText}");
                        }
                        //entrepreneurs.TimeDayClose = JobDateTTime(temData); //вычитаем даты
                        //Console.WriteLine($"Дата закрытия организации: {сloseInfo.TimeDayClose}\t\n");


                    }
                    entrepreneursList.Add(entrepreneurs); // при каждой итераци добавляем в список новый обьект
                    SaveDanni(entrepreneurs, "entrepreneurs"); // запись в json
                }

                Console.WriteLine();

                foreach (CloseInfo com in closeinfoList)
                {
                    Console.WriteLine($"Номер ID :{com.Id} - Дата закрытия:{com.CloseDate}  - Количество дней существования фирмы {com.TimeDayClose}");
                }

                //Console.WriteLine($"Количество действующих компаний = {companesTrue}");

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Что то пошло не так...Надо разбиратся...\t\n{ex}");
            }

            Console.Read();
        }



     public  void TestMethod()
        {
            XmlDocument docXML = new XmlDocument(); // XML-документ
            docXML.Load(pathXml); // загрузить XML

            //получаем выборку по первому повавшему параметру
            string nameCompane = docXML.GetElementsByTagName("Name")[0].InnerText;
            // получаем значение тега "TransportName", так как тег "TransportName" единственный, то обращаемся к нему напрямую (указывая порядковый номер/индекс [0])

            Console.WriteLine(nameCompane);
            Console.ReadKey();
        }



        /// <summary>
        /// Получение временной разницы
        /// </summary>
        public int JobDateTTime(DateTime newDateTime)
        {
            //получаем текущие время
            DateTime currentDateTime = DateTime.Now;

            //указываем время которое надо вычесть
           // DateTime newDateTime = new DateTime(2020, 1, 1);

            // Период времени от 2000 до данного момента.
            TimeSpan interval = currentDateTime.Subtract(newDateTime);

            // количество дней
            int DataTimeLife = (int)interval.TotalDays;

            //Console.WriteLine(interval.ToString());
            //Console.WriteLine("Дни: " + interval.Days);
            //Console.WriteLine("Часы: " + interval.Hours);
            //Console.WriteLine("Минуты: " + interval.Minutes);
            //Console.WriteLine("Секунды: " + interval.Seconds);
            //Console.ReadKey();

            //TODO:  вывод полученной инфы 
            // 
            return DataTimeLife;
        }


        //запись в файл
        /// <summary>
        /// запись в текстовой файл. Журнал событий
        /// </summary>
        /// <param name="myText"></param>
        public void WrateText(string myText)
        {
            //  string tempPathDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string tempPathDir = @"Log\"; // Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //DirectoryInfo dirInfo = new DirectoryInfo(@"Log");

            FileInfo dirInfo = new FileInfo($"{tempPathDir}" + @"\Log.txt");
            DirectoryInfo directoryInfo = new DirectoryInfo(tempPathDir);


            try
            {
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create(); // Создание ктолога лога
                }

                if (!dirInfo.Exists)
                {
                    dirInfo.Create();// создание файла
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при записи лога \t\n " + ex);
            }

            using (StreamWriter sw = new StreamWriter($"{tempPathDir}" + @"Log.txt", true, System.Text.Encoding.Default))

            // using (StreamWriter sw = new StreamWriter(myPachDir + @"texLog.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine(DateTime.Now + "\t\n" + myText); // запись

            }
        }


        /// <summary>
        /// Запись полученных обьектов в json формат
        /// </summary>
        /// <param name="testSettingsJson"></param>
        public void SaveDanni(Object testSettingsJson, string nameFil)
        {
            try
            {
                string result = JsonConvert.SerializeObject(testSettingsJson);

                //using (StreamWriter sw = new StreamWriter("user.json", true, System.Text.Encoding.Default)) //перезапись файла.

                using (StreamWriter sw = new StreamWriter($"{nameFil}.json", true, System.Text.Encoding.Default))

                {
                    sw.WriteLine(result); // запись
                    WrateText("попытка записи файла user.json");
                }
            }
            catch (Exception ex)
            {

                WrateText("Ошибка при создании файла настроек user.json");
            }

        }


        public void ReadingJsonFile()
        {
            string pathJaog = @"entrepreneurs.json";

            var objJson = JsonConvert.DeserializeObject<Entrepreneurs>(File.ReadAllText(pathJaog));

            Console.WriteLine(objJson);
            Console.ReadKey();
        }


        //Тестовой метод десириализации json
        public void DesriJsonTest()
        {

           // string json = 
           // MyMusic newMusic = JsonConvert.DeserializeObject<MyMusic>(json);

           // JArray jsonArray = JArray.Parse(jsonText);

        //    int itemsCount = jsonArray.Count;

        //    string[] codes = new string[itemsCount];
        //    int[] indices = new int[itemsCount];

        //    for (int index = 0; index < itemsCount; ++index)
        //    {
        //        codes[index] = (string)jsonArray[index]["Kod"];
        //        indices[index] = index;
        //    }

        //    BubbleSort(codes, indices, StringComparer.OrdinalIgnoreCase);

        //    for (int index = 0; index < itemsCount; ++index)
        //    {
        //        Console.WriteLine("cycle index: {0}\tarray index: {1}", index, indices[index]);
        //        Console.WriteLine(jsonArray[indices[index]]);
        //    }
        }


    }
}
