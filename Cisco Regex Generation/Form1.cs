using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace Cisco_Regex_Generation 
{
    public partial class Form1 : Form
    {
        private List<string> cisco_vers;
        private string el_regex;
        private List<string> num_range;

        public Form1()
        {
            InitializeComponent();
            num_range = new List<string>();
            cisco_vers = new List<string>();
            this.el_regex = "";
            textBox1.Text = @"C:\Users\waverill\Documents\Cisco\4.3.2013\Test";
         //   CompareStrings("12.4(9e)", "12.4(9)");
        }

        private void parseOVAL(string file)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(file);
            XmlNodeList vers = xdoc.GetElementsByTagName("version_string");
            foreach (XmlNode node in vers)
            {
                string v = node.InnerText;
                if (!this.cisco_vers.Contains(v))
                    this.cisco_vers.Add(v);
            }
        }

        public static int CompareStrings(string s1, string s2)
        {
            Regex v = new Regex(@"^([0-9]+)\.([0-9])(\(([0-9]+)([a-z])?\))(([A-Z]+)([0-9]+)?([a-z]+)?)?");
            Match v1 = v.Match(s1);
            Match v2 = v.Match(s2);
            int major_v1 = Convert.ToInt32(v1.Groups[1].Captures[0].ToString());
            int major_v2 = Convert.ToInt32(v2.Groups[1].Captures[0].ToString());
            if (major_v1 > major_v2)
                return 1;
            else if (major_v2 > major_v1)
                return -1;
            else //they ==
            {
                int minor_v1 = Convert.ToInt32(v1.Groups[2].Captures[0].ToString());
                int minor_v2 = Convert.ToInt32(v2.Groups[2].Captures[0].ToString());
                if (minor_v1 > minor_v2)
                    return 1;
                else if (minor_v2 > minor_v1)
                    return -1;
                else
                {
                    int minor2_v1 = Convert.ToInt32(v1.Groups[4].Captures[0].ToString());
                    int minor2_v2 = Convert.ToInt32(v2.Groups[4].Captures[0].ToString());
                    if (minor2_v1 > minor2_v2)
                        return 1;
                    else if (minor2_v2 > minor2_v1)
                        return -1;
                    else 
                    {
                        if (v1.Groups[5].Success && !v2.Groups[5].Success)
                            return 1;
                        else if (!v1.Groups[5].Success && v2.Groups[5].Success)
                            return -1;
                        else if (v1.Groups[5].Success && v2.Groups[5].Success)
                        {
                            string tmp1 = v1.Groups[5].Captures[0].ToString();
                            string tmp2 = v2.Groups[5].Captures[0].ToString();
                            int res = tmp1.CompareTo(tmp2);
                            if (res != 0)
                                return res;
                            else if (v1.Groups[7].Success && v2.Groups[7].Success)
                            {
                                tmp1 = v1.Groups[7].Captures[0].ToString();
                                tmp2 = v2.Groups[7].Captures[0].ToString();
                                res = tmp1.CompareTo(tmp2);
                                if (res != 0)
                                    return res;
                                else if (v1.Groups[8].Success && v2.Groups[8].Success)
                                {
                                    int n1 = Convert.ToInt32(v1.Groups[8].Captures[0].ToString());
                                    int n2 = Convert.ToInt32(v2.Groups[8].Captures[0].ToString());
                                    if (n1 > n2)
                                        return 1;
                                    else if (n2 > n1)
                                        return -1;
                                    else if (n1 == n2)
                                    {
                                        if (v1.Groups[9].Success && v2.Groups[9].Success)
                                            return v1.Groups[9].Captures[0].ToString().CompareTo(v2.Groups[9].Captures[0].ToString());
                                        else if (!v1.Groups[9].Success && v2.Groups[9].Success)
                                        {
                                            return -1;
                                        }

                                        else if (v1.Groups[9].Success && !v2.Groups[9].Success)
                                        {
                                            return 1;
                                        }

                                        else if (!v1.Groups[9].Success && !v2.Groups[9].Success)
                                        {
                                            return 0;
                                        }
                                        else
                                            return 0;
                                    }
                                    else
                                        return 0;
                                }

                                else if (!v1.Groups[8].Success && v2.Groups[8].Success)
                                {
                                    return -1;
                                }

                                else if (v1.Groups[8].Success && !v2.Groups[8].Success)
                                {
                                    return 1;
                                }

                                else if (!v1.Groups[8].Success && !v2.Groups[8].Success)
                                {
                                    return 0;
                                }
                                else
                                    return 0;
                            }

                            else if (!v1.Groups[7].Success && v2.Groups[7].Success)
                            {
                                return -1;
                            }
                            else if (v1.Groups[7].Success && !v2.Groups[7].Success)
                            {
                                return 1;
                            }
                            else if (!v1.Groups[7].Success && !v2.Groups[7].Success)
                            {
                                return 0;
                            }
                            else
                                return 0;
                        }
                        else if (!v1.Groups[5].Success && !v2.Groups[5].Success)
                        {
                            if (v1.Groups[7].Success && v2.Groups[7].Success)
                            {
                               string tmp1 = v1.Groups[7].Captures[0].ToString();
                               string tmp2 = v2.Groups[7].Captures[0].ToString();
                               int res = tmp1.CompareTo(tmp2);
                                if (res != 0)
                                    return res;
                                else if (v1.Groups[8].Success && v2.Groups[8].Success)
                                {
                                    int n1 = Convert.ToInt32(v1.Groups[8].Captures[0].ToString());
                                    int n2 = Convert.ToInt32(v2.Groups[8].Captures[0].ToString());
                                    if (n1 > n2)
                                        return 1;
                                    else if (n2 > n1)
                                        return -1;
                                    else if (n1 == n2)
                                    {
                                        if (v1.Groups[9].Success && v2.Groups[9].Success)
                                            return v1.Groups[9].Captures[0].ToString().CompareTo(v2.Groups[9].Captures[0].ToString());
                                        else if (!v1.Groups[9].Success && v2.Groups[9].Success)
                                        {
                                            return -1;
                                        }

                                        else if (v1.Groups[9].Success && !v2.Groups[9].Success)
                                        {
                                            return 1;
                                        }

                                        else if (!v1.Groups[9].Success && !v2.Groups[9].Success)
                                        {
                                            return 0;
                                        }
                                        else
                                            return 0;
                                    }
                                    else
                                        return 0;
                                }

                                else if (!v1.Groups[8].Success && v2.Groups[8].Success)
                                {
                                    return -1;
                                }

                                else if (v1.Groups[8].Success && !v2.Groups[8].Success)
                                {
                                    return 1;
                                }

                                else if (!v1.Groups[8].Success && !v2.Groups[8].Success)
                                {
                                    return 0;
                                }
                                else
                                    return 0;
                            }

                            else if (!v1.Groups[7].Success && v2.Groups[7].Success)
                            {
                                return -1;
                            }
                            else if (v1.Groups[7].Success && !v2.Groups[7].Success)
                            {
                                return 1;
                            }
                            else if (!v1.Groups[7].Success && !v2.Groups[7].Success)
                            {
                                return 0;
                            }
                            else
                                return 0;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // int num = readTextIn();
          //  MessageBox.Show("Found " + num + " unique vulnerable IOS versions.");
           // this.cisco_vers.Sort();
           // writeText();
           // MessageBox.Show("DONE");
            var files = Directory.GetFiles(textBox1.Text);
            foreach (string f in files)
            {
                parseOVAL(f);
            }
            this.cisco_vers.Sort(CompareStrings);
            Trie<char, object> my_trie = new Trie<char, object>();
            foreach (string s in this.cisco_vers)
            {
                my_trie.Add(s, true);
            }
           // writeText();
           // int test= my_trie.Suffixes("15.0(1)SE").Count;
           // Node rte =  my_trie.root;
          /* Trie<char, object> my_trie = new Trie<char, object>();
            my_trie.Add("12.2(55)EX", true);
            my_trie.Add("12.2(55)EX1", true);
            my_trie.Add("12.2(55)EX2", true);
            my_trie.Add("12.2(55)EX3", true);
            my_trie.Add("12.2(55)EY", true);
            my_trie.Add("12.2(55)EZ", true);
            my_trie.Add("12.2(55)SE", true);
            my_trie.Add("12.2(55)SE1", true);*/
            Trie<char, object>.Node<char, object> n = my_trie.root;
            ghettoTraverse(n);
            replaceRanges();
            textBox2.Text = this.el_regex;
            string ver_string = "";
            foreach (string vers in this.cisco_vers)
            {
                ver_string += vers + "\r\n";
            }
            textBox3.Text = ver_string;
            string non_match = "";
            Regex r = new Regex(this.el_regex);
            int bad_count = 0;
            foreach (string s in this.cisco_vers)
            {
                if (!r.Match(s).Success)
                {
                    non_match += s + "\r\n";
                    bad_count++;
                }
            }
            textBox4.Text = non_match;
            textBox5.Text = "Found " + bad_count + " non-matching version strings out of " + this.cisco_vers.Count + " unique version strings.";
          //  MessageBox.Show(this.el_regex);
        }

        private void ghettoTraverse(Trie<char, object>.Node<char, object> node)
        {
            Boolean open = false;
            List<Trie<char, object>.Node<char, object>> n_kids = node.Children;
            string val = node.Key.ToString();
            if (val == "." || val == @"\" || val == "(" || val == ")")
                val = @"\" + val;
            if (n_kids.Count > 1)
            {
                val = val + @"(";
                open = true;
            }
            if (val != "\0")
                this.el_regex += val;
       /*     foreach (Trie<char, object>.Node<char, object> n_node in n_kids)
            {
                ghettoTraverse(n_node);
                if (open && n_node.Children.Count>0)
                    this.el_regex += "|";
            }*/

            for (int x = 0; x < n_kids.Count; x++)
            {
                ghettoTraverse(n_kids[x]);
                if (open && x != n_kids.Count - 1)
                    this.el_regex += "|";
            }
            if (node.Parent != null)
                if (node.Parent.Children.Count < 2 && this.cisco_vers.Contains(strAtNode(node.Parent)))
                    this.el_regex += "?";
            //    else if (this.cisco_vers.Contains(strAtInnerNode(node.Parent)))
            //        this.el_regex += "?";
            if (open)
            {
                this.el_regex += ")";
                if(this.cisco_vers.Contains(strAtNode(node)))
                        this.el_regex +="?";
            }        
        }

        private string strAtNode(Trie<char, object>.Node<char, object> n)
        {
            if (n == null)
                return "";
            string str ="";
            IEnumerable<char> keys = n.Keys;
            foreach (char c in keys)
            {
                if (c != '\0')
                    str += c;
            }
            return str;
        }

        private string strAtInnerNode(Trie<char, object>.Node<char, object> n)
        {
            if (n == null)
                return "";
            string str = "";
            IEnumerable<char> keys = n.Keys;
            foreach (char c in keys)
            {
                if (c != '\0')
                    str += c;
            }
            return str + ")";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private int readTextIn()
        {
            int total = 0;
            int found = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader(textBox1.Text);
            while ((line = file.ReadLine()) != null)
            {
                total++;
                Console.WriteLine(line);
                if (!this.cisco_vers.Contains(line))
                {
                    this.cisco_vers.Add(line);
                    found++;
                }
            }

            file.Close();
            // Suspend the screen.
            Console.ReadLine();
            return found;
        }

        private void writeText()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\waverill\orderedIOS2.txt");
            file.AutoFlush = true;
            foreach (string line in this.cisco_vers)
            {
                file.WriteLine(line);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            TextBox l = (TextBox)sender;
            Clipboard.SetText(l.Text);
        }
        private void textBox3_Click(object sender, EventArgs e)
        {
            TextBox l = (TextBox)sender;
            Clipboard.SetText(l.Text);
        }

        private void replaceRanges()
        {
            string range = "";
            for (int x = 0; x < 10; x++)
            {
                for (int y = 9; y > x + 1; y--)
                {
                    range = genRange(y, x);
                    this.num_range.Add(range);
                    string regex_range = "[" + x.ToString() + "-" + y.ToString() + "]";
                 //   this.el_regex = this.el_regex.Replace(range, regex_range);
                    this.el_regex = Regex.Replace(this.el_regex, range + "([^A-Za-z0-9)(|])?", regex_range);
                }
            }
        }

        private string genRange(int num, int limit)
        {
            int tmp = limit;
            string s = "";
            while (tmp <= num)
            {
                s += tmp.ToString();
                tmp++;
                if (tmp<= num)
                    s += @"\|";
            }
            return s;
        }
    }
}
