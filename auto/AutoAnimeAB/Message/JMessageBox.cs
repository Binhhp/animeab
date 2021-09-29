using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoAnimeAB.Message
{
   public static class JMessageBox
    {
        public static void ErrorMessage(string message)
        {
            var dialog = MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        public static void SuccessMessage(string message)
        {
            var dialog = MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
    }
}
