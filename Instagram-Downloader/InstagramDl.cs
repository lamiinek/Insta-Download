using System;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace InstagramDownloader
{
	public class InstagramDl : Form
	{

		// gui elements
		Label label = new Label();
		TextBox inputLink = new TextBox();
		Button dlBtn = new Button ();
		PictureBox loading = new PictureBox ();
		PictureBox picBox = new PictureBox ();


		public static void Main(String[] args){
			
			Application.Run (new InstagramDl());
				
		}

		public InstagramDl (){
			Text = "Instagram Download - by @lamiinek";
			SetBounds (0, 0, 600, 600);
			BackColor = Color.White;

			label.Text = "URL:";
			inputLink.BorderStyle = BorderStyle.None;
			dlBtn.Text = "Download";
			picBox.BackColor = Color.Bisque;
			picBox.BorderStyle = BorderStyle.Fixed3D;

			label.BackColor = Color.AliceBlue;
			label.Font = new Font ("Arial", 15, FontStyle.Regular);
			inputLink.Font = new Font ("Arial", 12, FontStyle.Regular);
			dlBtn.Font = new Font ("Arial", 13, FontStyle.Regular);
			inputLink.BackColor = Color.AntiqueWhite;
			label.SetBounds (50, 40, 50, 20);
			inputLink.SetBounds (150, 40, 380, 40);
			dlBtn.SetBounds (425, 90, 100, 40);
			loading.SetBounds (50, 90, 40, 40);
			loading.ImageLocation = "gears.gif";
			loading.SizeMode = PictureBoxSizeMode.StretchImage; // resize the img to the size of the pbox
			picBox.SetBounds (50, 150, 480, 360);
			// hide loading till the dlbtn is clicked
			loading.Visible = false;
			dlBtn.Click += new EventHandler (download);

			Controls.Add (label);
			Controls.Add (inputLink);
			Controls.Add (dlBtn);
			Controls.Add (loading);
			Controls.Add (picBox);


		}
			

		public void download(object sender, EventArgs e){

			loading.Visible = true;
			Thread.Sleep (2000);

			String url = inputLink.Text;
			if (url.Length > 10) {
				
				//https://www.instagram.com/p/BJ3HuuCjeK1/?taken-by=natgeo
				try {

					Process p = new Process{
						StartInfo = new ProcessStartInfo{
							FileName = "instagram_dl.exe",
							Arguments = url,
							UseShellExecute = false,
							CreateNoWindow = true,
							RedirectStandardOutput = true
						}
					};
					p.Start ();

					String output = p.StandardOutput.ReadLine ();
					picBox.ImageLocation = output;
					picBox.SizeMode = PictureBoxSizeMode.StretchImage;
					
				} catch (Exception ex) {
					MessageBox.Show (ex.Message);
					Console.WriteLine (ex.Message);
				}

			} else {
				MessageBox.Show ("URL not valid!");
			}

			loading.Visible = false;
		}
	}
}

