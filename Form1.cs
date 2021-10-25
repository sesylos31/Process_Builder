using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnvDTE;
using TCatSysManagerLib;
using EnvDTE100;
using System.IO;
using System.Xml;





namespace Process_Builder
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {

        }
        // Create a new ArrayList to hold the Customer objects.
        private ArrayList customerArray = new ArrayList();
        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
        //Abre o projeto buscando o arquivo *.sln como referencia
        string plcName = " ";
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Twcat files (*.sln)|*.sln|All files (*.*)|*.*";
                openFileDialog1.ShowDialog();
                string Caminho = openFileDialog1.FileName;                
                string sourceFolderPath = Path.GetDirectoryName(Caminho);
                string projectNameFull = Path.GetFileName(Caminho);
                int found = 0;
                found = projectNameFull.IndexOf(".");
                string projectName = projectNameFull.Substring(0,found );
                this.Text = this.Text + " - " + projectName;

            //lê o arquivo TSPROJ para saber o conteudo do projeto
                string projFileName = sourceFolderPath + @"\" + projectName + @"\"+ projectName + @".tsproj" ;
                XmlTextReader reader = new XmlTextReader(projFileName);
            //string[] tsrpoj = File.ReadAllLines(projFileName);
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element   :   // The node is an element.
                        if (reader.Name == "Project")
                        {
                            //reader.MoveToNextAttribute();
                            //reader.MoveToNextAttribute();
                            int numAtrib = reader.AttributeCount;
                            reader.MoveToAttribute (1);
                            plcName = reader.Value;
                            //while (reader.MoveToNextAttribute()) // Read the attributes.
                            //    if (reader.Name == "Name")
                            //    {
                            //        String plcName = reader.Name;
                            //        TsprojLst.Items.Add(plcName);
                            //    }
                        }
                        
                        //while (reader.MoveToNextAttribute()) // Read the attributes.
                        //TsprojLst.Items.Add(reader.AttributeCount + " " + reader.Name + "=" + reader.Value);
                        //TsprojLst.Items.Add(reader.Name);
                        break;
                }
                
            }
            this.Text = this.Text + " - " + plcName;

            string plcDir = sourceFolderPath + @"\" + projectName + @"\" + plcName;
            string[] plcTree = Directory.GetDirectories(plcDir);

            int plcDirIndx = 0;

            projectTre.BeginUpdate();
            projectTre.Nodes.Clear();
            projectTre.Nodes.Add(projectName);
            projectTre.Nodes[0].Nodes.Add("PLC");
            projectTre.Nodes[0].Nodes[0].Nodes.Add(plcName);
            //projectTre.Nodes[0].Nodes[1].Nodes.Add("Grandchild");
            //projectTre.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Great Grandchild");
            projectTre.EndUpdate();

            int i = 0;

            foreach (string line in plcTree)
            {
                //TsprojLst.Items.Add(line);
                projectTre.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(line.Substring(plcDir.Length+1, line.Length - (plcDir.Length+1)));
            }
            foreach (string line in plcTree)
            {
                string[] plcFolder = Directory.GetDirectories(line);
                if (plcFolder.Length == 0)
                {
                    string[] fileName = Directory.GetFiles(line);
                    foreach (string line2 in fileName)
                    {
                        projectTre.Nodes[0].Nodes[0].Nodes[0].Nodes[i].Nodes.Add(line2.Substring(line.Length + 1, line2.Length - (line.Length + 1)));
                    }
                }
                //i = 0;
                foreach (string line1 in plcFolder)
                {
                    projectTre.Nodes[0].Nodes[0].Nodes[0].Nodes[i].Nodes.Add(line1.Substring(line.Length + 1, line1.Length - (line.Length + 1)));
                    
                }
                i = i + 1;
            }
 

            //string[] Tsproj = File.ReadAllLines(sourceFolderPath + @"\")

            //Type t = System.Type.GetTypeFromProgID("VisualStudio.DTE.16.0", true);
            //EnvDTE.DTE dte = (EnvDTE.DTE)System.Activator.CreateInstance(t);
            //var settings = dte.GetObject("TcAutomationSettings");
            //settings.SilentMode = true;
            //dte.SuppressUI = false;
            //dte.MainWindow.Visible = true;

            //EnvDTE.Solution sol = dte.Solution;

            //sol.Open(Caminho);                                       //   @"Z:\Simpaper\SIMPAPER NOVO\SIMPAPER NOVO.sln");
            //EnvDTE.Project pro = sol.Projects.Item(1);
            //ITcSysManager sysMan = pro.Object;
            //string ProjetoRota = "DESKTOP-GDPOINR";
            //string ProjetoCaminho = @"Z:\Simpaper\SIMPAPER NOVO";
            //string ProjetoNome = @"SIMPAPER NOVO";

            //dte.ExecuteCommand("File.OpenProjectFromTarget", @"DESKTOP-GDPOINR Z:\Simpaper\SIMPAPER NOVO SIMPAPER NOVO");

            //ITcSmTreeItem plc = TCatSysManager.LookupTreeItem("TIPC");
            //ITcSmTreeItem newProject = plc.CreateChild("NameOfProject", 1, "", pathToProjectOrTpzipFile);
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Add customers to the ArrayList of Customer objects.
            for (int x = 0; x < 1000; x++)
            {
                customerArray.Add(new Customer("Customer" + x.ToString()));
            }

            // Add orders to each Customer object in the ArrayList.
            foreach (Customer customer1 in customerArray)
            {
                for (int y = 0; y < 15; y++)
                {
                    customer1.CustomerOrders.Add(new Order("Order" + y.ToString()));
                }
            }

            // Display a wait cursor while the TreeNodes are being created.
            //Cursor.Current = new Cursor("MyWait.cur");

            // Suppress repainting the TreeView until all the objects have been created.
            treeView1.BeginUpdate();

            // Clear the TreeView each time the method is called.
            treeView1.Nodes.Clear();

            // Add a root TreeNode for each Customer object in the ArrayList.
            foreach (Customer customer2 in customerArray)
            {
                treeView1.Nodes.Add(new TreeNode(customer2.CustomerName));

                // Add a child treenode for each Order object in the current Customer object.
                foreach (Order order1 in customer2.CustomerOrders)
                {
                    treeView1.Nodes[customerArray.IndexOf(customer2)].Nodes.Add(
                      new TreeNode(customer2.CustomerName + "." + order1.OrderID));
                }
            }

            // Reset the cursor to the default for all controls.
            Cursor.Current = Cursors.Default;

            // Begin repainting the TreeView.
            treeView1.EndUpdate();
        }
    }
}
