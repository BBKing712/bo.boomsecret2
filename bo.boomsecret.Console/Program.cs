using bo.boomsecret.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bo.boomsecret.Console
{
    class Program
    {
        private static string _key = ConfigurationManager.AppSettings["BoomEncryptionPassword"];

        static void Main(string[] args)
        {

            System.Console.WriteLine("WpfBoomSecret.ConsoleRunner");
            //string email1 = "b.korn@gmx.de";
            string email1 = "s.mueller@bo-wohnungswirtschaft.de";
            //            "email"; "NU Name"
            //"test1@test1.de"; "S. Bauer Elektrotechnik GmbH & Co. KG"
            //"test2@test2.de"; "MANDATA Service Linden"
            //"s.li@boservice.de"; "Herr Lehmann Gasgeräte-Kundendienst"
            //"p.schweitzer@bo-wohnungswirtschaft.de"; "Elektro Gerhardt GmbH"
            //"info@Mietenkorte-gmbh.de"; "Mietenkorte GmbH"
            //"kokulinsky@bo-wohnungswirtschaft.de"; "Turbo Fix"
            //"h.kokulinsky@bo-wohnungswirtschaft.de"; "Turbo Fix"
            //"p.schweitzer@getcloud.de"; "Turbo Fix"
            //"schmeisser@adesso-mobile.de"; "Turbo Fix"
            //"s.mueller@bo-wohnungswirtschaft.de"; "Turbo Fix"
            //"marschner@adesso-mobile.de"; "SAL-Haustechnik GmbH"
            //"syn0nym@hotmail.de"; "Turbo Fix"
            //"hendrik.kokulinsky@gmx.de"; "Turbo Fix"
            //"sascha.sigges@outlook.com"; "Turbo Fix"
            //"l@w.de"; "Herr Lehmann Gasgeräte-Kundendienst"
            //"user01@mail.de"; "RKR-Wenger Rohr- und Kanalreinigung"
            //"n@w.de"; "N & W Gebäudetechnik GmbH"
            //"k.hoefeler@boservice.de"; "Turbo Fix"
            //"miloja6130@4tmail.net"; "Ernst Augustin Wasseranlagen"
            //"stammdaten@boservice.de"; "PBR Bauservice UG"



            string encrypted = BoomEncryptor.GetEncryptedString(email1, _key);
            string encrypted2 = BoomEncryptor.GetEncryptedStringNEW(email1, _key);
            string boomServer = "https://boom.dev.enapt.de";
            string boomserverLogin = (boomServer + "/login?s=");
            string completeLink = boomserverLogin + encrypted;
            //bool exists1 = BoomLoginHelper.CheckExistenceAsync(encrypted).Result;
            bool exists2 = BoomLoginHelper.CheckExistence(encrypted);
            System.Diagnostics.Process.Start(completeLink);
            System.Console.WriteLine("Press any key to exit the program.");
            System.Console.ReadKey();


        }
    }
}
