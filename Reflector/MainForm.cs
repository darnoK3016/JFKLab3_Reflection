using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reflector
{
    public partial class MainForm : Form
    {
        private Assembly assembly;
        public MainForm()
        {
            InitializeComponent();
        }

        private static void AddModule(Module module, TreeNode parent)
        {
            var newNode = new TreeNode(module.Name) { Tag = module };
            parent.Nodes.Add(newNode);


            foreach (var type in module.GetTypes())
            {
                AddType(type, newNode);
            }
        }

        private static void AddType(Type type, TreeNode parent)
        {
            var newNode = new TreeNode(type.Name) { Tag = type };
            TreeNode memberNode;

            var memberTypeNode = new TreeNode("Methods");
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                var count = method.GetParameters().Length;
                var stringBuilder = new StringBuilder(method.Name).Append('(');
                foreach (var param in method.GetParameters())
                {
                    stringBuilder.Append(param.ParameterType);
                    if (param.Position < count - 1)
                    {
                        stringBuilder.Append(", ");
                    }
                }
                stringBuilder.Append(')');
                memberNode = new TreeNode(stringBuilder.ToString()) { Tag = method };
                memberTypeNode.Nodes.Add(memberNode);
            }
            newNode.Nodes.Add(memberTypeNode);
            parent.Nodes.Add(newNode);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            using (var openAssemblyDialog = new FolderBrowserDialog())
            {
                if (openAssemblyDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                else
                {
                    string[] files = Directory.GetFiles(openAssemblyDialog.SelectedPath);
                    foreach (string file in files)
                    {
                        try
                        {
                            this.assembly = Assembly.LoadFrom(file.ToString());
                            this.PopulateTree();
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }

            }
        }

        private void PopulateTree()
        {
            var newNode = new TreeNode(this.assembly.GetName().Name) { Tag = this.assembly };
            this.treeView1.Nodes.Add(newNode);

            foreach (var module in this.assembly.GetModules())
            {
                AddModule(module, newNode);
            }
        }

        private void memberNode_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBox1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            textBox1.Text = "e.g. \"Argument1 ; Argument2\" etc.";
            textBox2.Clear();

            if (treeView1.SelectedNode.Tag is MethodInfo selected)
            { 
                if (selected.GetCustomAttribute<Prism.DescriptionAttribute>(true) != null)
                {
                    var descriptionAttribute = selected.GetCustomAttribute<Prism.DescriptionAttribute>();
                     
                    textBox3.Text = descriptionAttribute.Description;
                    
                     textBox3.Text += string.IsNullOrEmpty(descriptionAttribute.Copyright) ? string.Empty : Environment.NewLine + $" Copyright: '{descriptionAttribute.Copyright}'";
                }
                else
                {
                    textBox3.Text = "Description empty";
                }
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.ForeColor = textBox1.ForeColor = System.Drawing.SystemColors.WindowText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var input = textBox1.Text;
            string[] inputElement = input.Split(';', ':');

            try
            {
                MethodInfo methodInfo = (MethodInfo)treeView1.SelectedNode.Tag;
                ParameterInfo[] parameters = methodInfo.GetParameters();

                dynamic classIstance = Activator.CreateInstance((Type)treeView1.SelectedNode.Parent.Parent.Tag);
                int count = parameters.Length;

                if (count > 0 && count == inputElement.Length)
                {
                    object[] parametersArray = new object[count];
                    int i = 0;
                    foreach (var param in parameters)
                    {
                        parametersArray[i] = TypeDescriptor.GetConverter(parameters[0].ParameterType).ConvertFrom(inputElement[i]);
                        i++;
                    }
                    object result = methodInfo.Invoke(classIstance, parametersArray);
                    textBox2.Text = result.ToString();
                    textBox2.Show();
                }
                else if (count > 0 && count != inputElement.Length)
                {
                    MessageBox.Show("Invalid argument count.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("This method cannot be run.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.ToString()} Invalid input argument.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
