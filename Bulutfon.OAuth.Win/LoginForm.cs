using System.Linq;
using System.Windows.Forms;

namespace Bulutfon.OAuth.Win {

    public partial class LoginForm : Form {

        public static bool Login(string clientId, string clientSecret, IWin32Window owner = null) {
            using (var frm = new LoginForm(clientId, clientSecret)) {
                frm.webBrowser.Navigate(frm.client.GetServiceLoginUrl());
                return frm.ShowDialog(owner) == DialogResult.OK;
            }
        }

        private BulutfonWinClient client;

        protected LoginForm(string clientId, string clientSecret) {
            InitializeComponent();
            client = new BulutfonWinClient(clientId, clientSecret);
        }

        private void webBrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
            if (e.Url.Segments.Reverse().Skip(1).FirstOrDefault() == "authorize/") {
                var code = e.Url.Segments.Last();
                var authenticated = client.VerifyAuthentication(code);
                if (authenticated) {
                    DialogResult = DialogResult.OK;
                }
                else {
                    MessageBox.Show("Giriş başarısız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                }
                Close();
            }
        }
    }
}