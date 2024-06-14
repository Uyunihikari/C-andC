using System;
using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;


namespace WinFormsApp4
{

 public partial class Form1 : Form
 {
     [DllImport("Dll4",CallingConvention = CallingConvention.Cdecl)]

     private  static extern void Memory(int pid, IntPtr ptr, int newvalue, out int byteswritten);

     private TextBox id;
     private TextBox address;
     private TextBox newvalue;
     private Button button;
     
     public Form1()
     {
         InitializeComponent();
         InitializeMyComponents();
     }

     private void InitializeMyComponents()
     {
         id = new TextBox();
         id.Location = new Point(10, 20);
         id.Size = new Size(100, 20);
         
        address = new TextBox();
         address.Location = new Point(10, 50);
         address.Size = new Size(100, 20);

         newvalue = new TextBox();
         newvalue.Location = new Point(10, 80);
         newvalue.Size = new Size(100, 20);

         button = new Button();
         button.Text = "Write Memory";
         button.Location = new Point(10, 110);
         button.Size = new Size(100, 20);


         button.Click += click;
         
         this.Controls.Add(id);
         this.Controls.Add(address);
         this.Controls.Add(newvalue);
         this.Controls.Add(button);

         

     }

     private void click(object sender, EventArgs e)
     {
         int pid = int.Parse(id.Text);

         if (CheckId(pid))
         {

             try
             {

                 IntPtr ptr = new IntPtr(Convert.ToInt64(address.Text, 16));
                 int bytes;
                 int newvalue = int.Parse(this.newvalue.Text);
                 
                 Memory(pid,ptr,newvalue,out bytes);


             }
             catch (Exception exception)
             {
                 MessageBox.Show($"Error:{exception.Message}", "Error");
             }
             
         }


     }

     private bool CheckId(int pid)
     {
         Process[] process = Process.GetProcesses();

         foreach (var _pid in process)
         {
             if (pid == _pid.Id)
             {
               //  MessageBox.Show("未找到pid", "Error");
                 return true;
             }
         }
         MessageBox.Show("未找到pid", "Error");
         return false;
     }
     
 }

}
