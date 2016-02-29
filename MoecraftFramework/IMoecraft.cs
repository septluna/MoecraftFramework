using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;


//using System.EnterpriseServices;//引用COM+的类

namespace MoecraftFramework
{
    [Guid("EC1DECD9-927F-4D33-9939-DB2080311AF7")]
    public interface IMoecraft
    {
        string main(string info);
        bool show();
    }
    public interface IShow
    {
        bool Show();
    }

    [Guid("3898EEB5-D51B-449A-BB7C-DDD7921E7227")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Entry : IMoecraft
    {
        bool FormExist = true;
        public string text;
        public string main(string info)
        {
            MessageBox.Show("123456");
            return info;
        }
        public bool show()
        {                
            CTransfShow ct = new CTransfShow();
            FormExist = ct.aaa();
            return FormExist;
        }
    }
    public class CTransfShow
    {
        bool FormExist = true;
        public bool aaa()
        {
            IShow ish = new CShow();
            FormExist = ish.Show();
            return FormExist;
        }
    }
    public class CShow : IShow
    {
        bool FormExist = true;
        public bool Show()
        {
            Form frm = new Form();
            frm.Text = "测试窗口";
            frm.FormClosed += FormClose;
            frm.ShowDialog();
            return FormExist;
        }
        public void FormClose(object sender,EventArgs e)
        {
            FormExist = false;
        } 
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    }
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "打开脚本";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 352);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
