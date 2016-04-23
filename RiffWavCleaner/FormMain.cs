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
using System;
using System.ComponentModel;
using System.Windows.Forms;
using PcmLib;

namespace RiffWavCleaner
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		#region Form EventHandler
		
		private void FormMain_Load(object sender, EventArgs e)
		{
			// 引数で受けた場合（優先）
			string[] cmd = System.Environment.GetCommandLineArgs();
			if (cmd.Length > 1) {
				tBFileIn.Text = cmd[1];
				if (tBFileOut.Text == "") {
					tBFileOut.Text = createOutputFileName(tBFileIn.Text);
				}
			}
		}

		private void FormMain_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				e.Effect = DragDropEffects.Copy;
			} else {
				e.Effect = DragDropEffects.None;
			}
		}

		private void FormMain_DragDrop(object sender, DragEventArgs e)
		{
			string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			if (fileNames.Length > 0) {
				tBFileIn.Text = fileNames[0];
				if (tBFileOut.Text == "") {
					tBFileOut.Text = createOutputFileName(tBFileIn.Text);
				}
			}
		}

		#endregion

		#region Control EventHandler

		private void btnRefIn_Click(object sender, EventArgs e)
		{
			DialogResult d = openFD.ShowDialog();
			if (d == DialogResult.OK) {
				tBFileIn.Text = openFD.FileName;
			}
		}

		private void btnRefOut_Click(object sender, EventArgs e)
		{
			DialogResult d = saveFD.ShowDialog();
			if (d == DialogResult.OK) {
				tBFileOut.Text = saveFD.FileName;
			}
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			if (tBFileIn.Text == "" || tBFileOut.Text == "") {
				return;
			}

			bgWorker.RunWorkerAsync(new string[]{ tBFileIn.Text, tBFileOut.Text });
		}

		#endregion

		#region BackGround Worker

		private void bgWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			const int COPY_LEN = 50 * 1024;
			BackgroundWorker worker = sender as BackgroundWorker;
			string[] files = (string[])e.Argument;

			try {
				using (RiffReader rr = new RiffReader(files[0]))
				using (RiffWriter rw = new RiffWriter(files[1])) {
					// 読み取りファイル解析
					bool b = rr.Parse();
					if (!b) {
						e.Result = -2;
						return;
					}

					WaveFormatEx wf = rr.WaveFormat;

					// 拡張ヘッダーを無効化できる条件
					if (wf.Channels <= 2 && wf.Extensible && wf.BitsPerSample != 32) {
						wf.Extensible = false;
						wf.FormatTag = WaveFormatTag.PCM;
					}
					// ヘッダーの書き出し
					b = rw.WriteHeader(wf);
					if (!b) {
						e.Result = -1;
						return;
					}

					// ループ回数を計算。進捗にも使う
					long max = rr.Length / COPY_LEN;
					if ((rr.Length % COPY_LEN) > 0) {
						max++;
					}

					for (long i = 0; i < max; i++) {
						byte[] arr = rr.Read8(COPY_LEN);
						if (!rw.WriteStream8(arr)) {
							e.Result = -1;
							return;
						}

						int percentage = (int)((i + 1) * 100 / max);
						worker.ReportProgress(percentage);
					}

					if (!rw.WriteFinalize()) {
						return;
					}
				}
			} catch {
				// エラー
				e.Result = -3;
				return;
			}
			e.Result = 0;
		}

		private void bgWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			progressBarMain.Value = e.ProgressPercentage;
		}

		private void bgWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			switch ((int)e.Result) {
				case 0:
					MessageBox.Show("正常終了");
					break;
				case -1:
					MessageBox.Show("ファイルの書き出しに失敗");
					break;
				case -2:
					MessageBox.Show("ファイルの読み出しに失敗");
					break;
				case -3:
					MessageBox.Show("ファイルの読み出し、もしくは書き出しに失敗");
					break;
				default:
					MessageBox.Show("不明なエラー");
					break;
			}
		}

		#endregion

		#region その他

		private string createOutputFileName(string s)
		{
			if (s == null || s == "") {
				return "";
			}
			int n = s.LastIndexOf(".");
			if (n > 0) {
				string name = s.Substring(0, n);
				string ext = s.Substring(n + 1);
				return name + "_out." + ext;
			}
			return "";
		}

		#endregion
	}
}
