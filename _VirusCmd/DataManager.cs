using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _VirusCmd.Classes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace _VirusCmd
{
    static class DataManager
    {
        [Serializable]
        public class SerializableContainer 
        {
            public List<Country>? countries;
            public SeasonSingleton? season;
        }

        public static void Save(List<Country> countries, SeasonSingleton season, string name = "VirusDB.auf") 
        {
            SerializableContainer container = new() 
            {
                countries = countries,
                season = season
            };
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(name, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, container);
            stream.Close();
        }
        public static dynamic Read(string name = "VirusDB.auf") 
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.Read);
            SerializableContainer tempContainer = (SerializableContainer)formatter.Deserialize(stream);
            stream.Close();
            return tempContainer;
        }
    }
}
