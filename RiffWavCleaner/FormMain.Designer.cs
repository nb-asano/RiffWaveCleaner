/**
 * Copyright (c) 2012-2013 Sakura-Zen soft All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR(S) ``AS IS'' AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 * IN NO EVENT SHALL THE AUTHOR(S) BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
namespace RiffWavCleaner
{
	partial class FormMain
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.btnRefIn = new System.Windows.Forms.Button();
			this.tBFileIn = new System.Windows.Forms.TextBox();
			this.tBFileOut = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnRefOut = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.btnStart = new System.Windows.Forms.Button();
			this.progressBarMain = new System.Windows.Forms.ProgressBar();
			this.openFD = new System.Windows.Forms.OpenFileDialog();
			this.saveFD = new System.Windows.Forms.SaveFileDialog();
			this.bgWorker = new System.ComponentModel.BackgroundWorker();
			this.SuspendLayout();
			// 
			// btnRefIn
			// 
			this.btnRefIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRefIn.Location = new System.Drawing.Point(299, 12);
			this.btnRefIn.Name = "btnRefIn";
			this.btnRefIn.Size = new System.Drawing.Size(33, 23);
			this.btnRefIn.TabIndex = 2;
			this.btnRefIn.Text = "...";
			this.btnRefIn.UseVisualStyleBackColor = true;
			this.btnRefIn.Click += new System.EventHandler(this.btnRefIn_Click);
			// 
			// tBFileIn
			// 
			this.tBFileIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tBFileIn.Location = new System.Drawing.Point(47, 14);
			this.tBFileIn.Name = "tBFileIn";
			this.tBFileIn.Size = new System.Drawing.Size(246, 19);
			this.tBFileIn.TabIndex = 1;
			// 
			// tBFileOut
			// 
			this.tBFileOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tBFileOut.Location = new System.Drawing.Point(47, 43);
			this.tBFileOut.Name = "tBFileOut";
			this.tBFileOut.Size = new System.Drawing.Size(246, 19);
			this.tBFileOut.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "入力";
			// 
			// btnRefOut
			// 
			this.btnRefOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRefOut.Location = new System.Drawing.Point(299, 41);
			this.btnRefOut.Name = "btnRefOut";
			this.btnRefOut.Size = new System.Drawing.Size(33, 23);
			this.btnRefOut.TabIndex = 5;
			this.btnRefOut.Text = "...";
			this.btnRefOut.UseVisualStyleBackColor = true;
			this.btnRefOut.Click += new System.EventHandler(this.btnRefOut_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "出力";
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStart.Location = new System.Drawing.Point(257, 71);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 7;
			this.btnStart.Text = "開始";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// progressBarMain
			// 
			this.progressBarMain.Location = new System.Drawing.Point(14, 71);
			this.progressBarMain.Name = "progressBarMain";
			this.progressBarMain.Size = new System.Drawing.Size(237, 23);
			this.progressBarMain.TabIndex = 6;
			// 
			// openFD
			// 
			this.openFD.Filter = "Wavファイル|*.wav|すべてのファイル|*.*";
			// 
			// saveFD
			// 
			this.saveFD.Filter = "Wavファイル|*.wav|すべてのファイル|*.*";
			// 
			// bgWorker
			// 
			this.bgWorker.WorkerReportsProgress = true;
			this.bgWorker.WorkerSupportsCancellation = true;
			this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
			this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			// 
			// FormMain
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(344, 106);
			this.Controls.Add(this.progressBarMain);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnRefOut);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tBFileOut);
			this.Controls.Add(this.tBFileIn);
			this.Controls.Add(this.btnRefIn);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMain";
			this.Text = "RiffWavCleaner v0.1.0";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnRefIn;
		private System.Windows.Forms.TextBox tBFileIn;
		private System.Windows.Forms.TextBox tBFileOut;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnRefOut;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.ProgressBar progressBarMain;
		private System.Windows.Forms.OpenFileDialog openFD;
		private System.Windows.Forms.SaveFileDialog saveFD;
		private System.ComponentModel.BackgroundWorker bgWorker;
	}
}

