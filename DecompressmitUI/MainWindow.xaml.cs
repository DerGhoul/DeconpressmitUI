using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DecompressmitUI 
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
       

        private void decompress_Click(object sender, RoutedEventArgs e)
        {
            proofOfExist();
        }

        private void proofOfExist()
        {

            if (sourcePath.Text != "" && desPath.Text != "")
            {
                if (Directory.Exists(sourcePath.Text))
                {
                    if (Directory.Exists(desPath.Text))
                    {
                        OrdnerExtrahieren(sourcePath.Text, desPath.Text);
                    }
                    else
                    {
                        ausgabe.Text = "Der Zielpfad existiert nicht";
                    }

                }
                else
                {
                    ausgabe.Text = "Die Quellpfad existiert nicht";
                }

            }
            else
            {
                ausgabe.Text = "Geben sie einen Pfad an";
            }
        }


        

        public void OrdnerExtrahieren(string sourcefile, string destfile)
        {
            string fileName;

            if (System.IO.Directory.Exists(sourcefile))
            {
                string d = @"\";
                char splitter = d[0];
                string[] filewithoutZip = new string[2];
                string[] sourcefilewithoutZip = new string[2];

                string[] files = System.IO.Directory.GetFiles(sourcefile);

                
                 

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    fileName = System.IO.Path.GetFileName(s);
                    string directorypath = destfile + d + fileName;
                    string sourcedirectorypath = sourcefile + d + fileName;


                    sourcefilewithoutZip = sourcedirectorypath.Split('.');
                    filewithoutZip = directorypath.Split('.');

                    directorypath = filewithoutZip[0];
                    sourcedirectorypath = sourcefilewithoutZip[0];


                    Directory.CreateDirectory(directorypath);
                    if (Directory.Exists(directorypath))
                    {

                        ausgabe.Text += ($"{fileName} wurde erfolgreich erstellt\n");
                    }


                    ZipFile.ExtractToDirectory(s, directorypath);

                    directorypath = "";
                    sourcedirectorypath = "";
                }
            }
        
            else
            {
                ausgabe.Text = ("Source path does not exist!\n");
            }
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                proofOfExist();
            }
        }
    }
}
