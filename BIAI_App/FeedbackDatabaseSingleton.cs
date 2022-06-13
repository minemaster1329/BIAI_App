using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace BIAI_App
{
    internal sealed class FeedbackDatabaseSingleton
    {
        private XmlTextWriter DataWriter => new XmlTextWriter("database.xml", null){Formatting = Formatting.Indented};
        private XmlTextReader DataReader => new XmlTextReader("database.xml");

        private static readonly string[] _labels =
        {
            "Corn___Common_Rust",
            "Corn___Gray_Leaf_Spot",
            "Corn___Healthy",
            "Corn___Leaf_Blight",
            "Invalid",
            "Potato___Early_Blight",
            "Potato___Healthy",
            "Potato___Late_Blight",
            "Rice___Brown_Spot",
            "Rice___Healthy",
            "Rice___Hispa",
            "Rice___Leaf_Blast",
            "Wheat___Brown_Rust",
            "Wheat___Healthy",
            "Wheat___Yellow_Rust"
        };

        public ObservableCollection<InvalidImageRecord> InvalidImagesRecords { get; private set; }

        private static FeedbackDatabaseSingleton _instanceDatabaseSingleton;
        private XmlSerializer _serializer;

        public static FeedbackDatabaseSingleton Instance
        {
            get { return _instanceDatabaseSingleton ??= new FeedbackDatabaseSingleton(); }
        }

        private FeedbackDatabaseSingleton()
        {
            _serializer = new XmlSerializer(typeof(ObservableCollection<InvalidImageRecord>));

            if (!Directory.Exists("InvalidPhotos")) Directory.CreateDirectory("InvalidPhotos");

            foreach (string label in _labels)
            {
                if (!Directory.Exists($"InvalidPhotos/{label}")) Directory.CreateDirectory($"InvalidPhotos/{label}");
            }

            if (!File.Exists("database.xml"))
            {
                XmlTextWriter textWriter = DataWriter;
                textWriter.Formatting = Formatting.Indented;
                textWriter.WriteStartDocument();
                InvalidImagesRecords = new ObservableCollection<InvalidImageRecord>();
                _serializer.Serialize(textWriter, InvalidImagesRecords);
                textWriter.WriteEndDocument();
                textWriter.Close();
            }

            else
            {
                XmlTextReader reader = DataReader;
                if (_serializer.CanDeserialize(reader))
                {
                    InvalidImagesRecords = _serializer.Deserialize(reader) as ObservableCollection<InvalidImageRecord>;
                }
                else
                {
                    MessageBox.Show("Failed to read data from file");
                }
                reader.Close();
            }
        }

        public void Initialize(){}

        public void AddNewImage(string imagePath, string label)
        {
            if (!_labels.Contains(label)) throw new ArgumentException("Label not found!");
            if (imagePath is null || !File.Exists(imagePath)) throw new ArgumentException("Invalid image");

            InvalidImageRecord invalidImageRecord = new InvalidImageRecord() {Label = label};
            int imagesWithSpecifiedLabelCount = Directory.EnumerateFiles($"InvalidPhotos/{label}").Count();
            invalidImageRecord.ImagePath = $"InvalidPhotos/{label}/invalid_{imagesWithSpecifiedLabelCount}";
            File.Copy(imagePath, $"InvalidPhotos/{label}/invalid_{imagesWithSpecifiedLabelCount}{Path.GetExtension(imagePath)}");
            InvalidImagesRecords.Add(invalidImageRecord);
            Save();
        }

        public void Save()
        {
            File.Delete("database.xml");
            XmlTextWriter xmlTextWriter = DataWriter;
            xmlTextWriter.WriteStartDocument();
            _serializer.Serialize(xmlTextWriter, InvalidImagesRecords);
            xmlTextWriter.WriteEndDocument();
        }

        //public ICollection<InvalidImageRecord> GetAllImages()
        //{

        //}
    }
}
