using BikeCommunitySite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BikeCommunitySite.Services
{
    public class TypeConversions
    {
        public static DataTable jsonStringToTable(string jsonContent)
        {
            var places = JsonConvert.DeserializeObject<DestinationModel.RootObject>(jsonContent);
            var list = places.places;
            PropertyDescriptorCollection props =
            TypeDescriptor.GetProperties(typeof(DestinationModel.Place));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (var item in list)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static string jsonToCSV(string jsonContent, string delimiter)
        {
            StringWriter csvString = new StringWriter();
            using (var csv = new CsvHelper.CsvWriter(csvString))
            {
                //csv.Configuration.SkipEmptyRecords = true;
                //csv.Configuration.WillThrowOnMissingField = false;
                csv.Configuration.Delimiter = delimiter;

                using (var dt = jsonStringToTable(jsonContent))
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        csv.WriteField(column.ColumnName);
                    }
                    csv.NextRecord();

                    foreach (DataRow row in dt.Rows)
                    {
                        for (var i = 0; i < dt.Columns.Count; i++)
                        {
                            csv.WriteField(row[i]);
                        }
                        csv.NextRecord();
                    }
                }
            }
            return csvString.ToString();
        }

        public static async Task<List<GooglePlaceModel.Result>> ToGooglePlaceFormat(List<DestinationModel.Place> lst)
        {
            var rv = new List<GooglePlaceModel.Result>();
            var textsearch = "https://maps.googleapis.com/maps/api/place/textsearch/json?query={0}&key=AIzaSyBgSD6hkVYX4myK7K7Od7UUMPA3i0ijS6E";
            foreach (var place in lst)
            {

                var request = (HttpWebRequest)WebRequest.Create(string.Format(textsearch, place.name));

                WebResponse response = await request.GetResponseAsync();

                var raw = String.Empty;
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true, 1024, true))
                {
                    raw = reader.ReadToEnd();
                }
                var result = JsonConvert.DeserializeObject<GooglePlaceModel.Result>(raw);
                var p = new GooglePlaceModel.Result
                {
                    place_id = result.place_id,
                    description=place.description.ToString()
                };

                rv.Add(p);
            }
            return rv;
        }
    }
}
